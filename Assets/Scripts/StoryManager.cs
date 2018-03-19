using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Oversees all aspects of the story
/// </summary>
public class StoryManager : MonoBehaviour {

	public static StoryManager instance;
	public TextMeshControl phrase;
    public GameObject popUpBottomPrefab;
    public GameObject popUpTopPrefab;


	private AudioSource audioSource; // plays the words audiofile
	private List<Word> words; // Contains all words in the story
	private Story story; //  all sentences and phrases in the story
	private int storyProgress; // how far along they are in the story
	private bool slowAudio; // toggle switch control boolean
	// private int level; // eventually will be for different levels

	private void Awake() { 
	
		audioSource = GetComponent<AudioSource>();
		if (instance == null) // singleton pattern, instance of StoryManager
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		words = new List<Word>();
		story = new Story();
		storyProgress = 0;
		slowAudio = false;
		//level = 0;

		LoadStory();
		LoadSounds(); // Load the sounds from file
		UpdateText(1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Plays the sound of a word
	/// </summary>
	/// <param name="word">The word to be spoken</param>
	public void PlayWord(string word)
	{
		// finds the appropriate audioclip for the given word
		Word tmp = words.Find(w => w.sWord.Equals(word.ToLower()));
		if (tmp == null) return; // if recording doesnt exist return
		audioSource.clip = tmp.audioClip;

		if (audioSource != null && audioSource.clip != null)
		{
			if (!audioSource.isPlaying)
			{
				if(slowAudio)
				{
					audioSource.pitch = .7f;
					audioSource.Play();
				}
				else
				{
					audioSource.pitch = 1;
					audioSource.Play();
				}
			}
		}
	}

	/// <summary>
	/// Finds all audio files and calls LoadFile() to load all the sound files
	/// </summary>
	void LoadSounds()
	{

		FileInfo[] files;
		string fileName = Application.dataPath + "/" + "Sounds/Words/";
		var info = new DirectoryInfo(fileName);

		files = info.GetFiles()
			.Where(f => Path.GetExtension(f.Name) == ".wav" || Path.GetExtension(f.Name) == ".ogg" || Path.GetExtension(f.Name) == ".mp3")
			.ToArray();


		foreach (var file in files)
		{
			StartCoroutine(LoadFile(file.FullName));
		}


	}

	/// <summary>
	/// Loads a single audio clip from file into the List words
	/// </summary>
	/// <param name="path">path of audiofile</param>
	/// <returns></returns>
	IEnumerator LoadFile(string path)
	{
		WWW www = new WWW("file://" + path);

		AudioClip clip = www.GetAudioClip(false);
		while (clip.loadState != AudioDataLoadState.Loaded)
			yield return www;

		clip.name = Path.GetFileName(path);
		words.Add(new Word(clip.name.Substring(0, clip.name.IndexOf("_")).ToLower() ,clip, true));
	}

	/// <summary>
	/// Loads the entire story from a file
	/// </summary>
	void LoadStory()
	{

		string tmp;
		try
		{
			StreamReader file = new StreamReader(Application.dataPath + "/story.txt");
			while ((tmp = file.ReadLine()) != null)
			{
				story.Sentences.Add(tmp.Trim());
			}
		}
		catch (FileNotFoundException ex)
		{
			Debug.Log("story.txt is not found in the Assets Folder. Exception: " + ex.Message);
			return;
		}

		story.setPhrases();

	}

	/// <summary>
	/// Updates the text on screen
	/// </summary>
	/// <param name="dir">Which direction to update, -1 for previous, 1 for next</param>
	void UpdateText(int dir)
	{
		
		if(dir > 0)
		{
			if (storyProgress < story.Phrases.Count)
			{
				phrase.SetPhrase(story.Phrases[storyProgress++]);
			}
		}
		else
		{
			if(storyProgress > 1)
			{
				phrase.SetPhrase(story.Phrases[--storyProgress-1]);
			}

		}
		
	}

	/// <summary>
	/// Changes to the next event in the application
	/// </summary>
	public void NextStoryEvent()
	{
		// could change depending on how many sentences we want before an activity
		// could possibly put some delimiter or something in the story to show it should be the end of a story block
		if(storyProgress < story.Sentences.Count)
		{
			UpdateText(1);
		}
		return;
	}

	/// <summary>
	/// Changes to the previous event in the application
	/// </summary>
	public void PreviousStoryEvent()
	{
		if (storyProgress > 0)
		{
			UpdateText(-1);
		}
		return;
	}

    /// <summary>
    /// instantiates a popUp prefab at the words location
    /// </summary>
    /// <param name="wordInfo">WordInfo of the word to be shown in Thai</param>
    public void ShowThaiPopUp(string word, bool firstLine, Vector2 position)
    {
        // find location of word on screen, move up and instantiate a pop up
        Vector2 pos = Camera.main.ScreenToWorldPoint(position);
        GameObject popUp;
        if (firstLine)
        {
            pos.y += .75f;
            popUp = Instantiate(popUpTopPrefab, pos, popUpTopPrefab.transform.rotation);
            // this is where it would look up the english word to the thai word
            popUp.GetComponentInChildren<TextMeshPro>().text = "ดีใจ";
           }
        else
        {
            pos.y -= .75f;
            popUp = Instantiate(popUpBottomPrefab, pos, popUpTopPrefab.transform.rotation);
        }
        popUp.transform.parent = transform;
    }   

    public IEnumerator DestroyPopUp(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(GameObject.FindGameObjectWithTag("PopUp"));

    }

    /// <summary>
    /// toggles the slowAudio bool
    /// </summary>
	public void UpdateSlowAudio()
	{
		slowAudio = !slowAudio;
	}

    /// <summary>
    /// Plays the Audio of the sentences, of the current phrase
    /// </summary>
	public void PlayPhrase()
	{

	}

}
