using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Oversees all aspects of the story
/// </summary>
public class StoryManager : MonoBehaviour
{

	public static StoryManager instance;
	public TextMeshControl phrase;
	public GameObject popUpBottomPrefab;
	public GameObject popUpTopPrefab;
	public Text thaiHelp;
    public GameObject ThaiHelpPanel;

	public bool locked = false;
	private string currWord;


	private AudioSource audioSource; // plays the words audiofile
	private List<Word> words; // Contains all words in the story
	private Story story; //  all sentences and phrases in the story
	private int storyProgress; // how far along they are in the story
	private bool slowAudio; // toggle switch control boolean
							// private int level; // eventually will be for different levels

	private void Awake()
	{

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
	void Start()
	{

		words = new List<Word>();
		story = new Story();
		storyProgress = 0;
		slowAudio = false;
		thaiHelp.text = "";
		currWord = "";
		// level = 0;

		LoadStory();
		LoadSounds(); // Load the sounds from file
		StartStory();

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartStory()
	{
		UpdateText(1);
	}

	/// <summary>
	/// Plays the sound of a word
	/// </summary>
	/// <param name="word">The word to be spoken</param>
	public void PlayWord(string word)
	{
		// finds the appropriate audioclip for the given word
		Word tmp = words.Find(w => w.word.ToLower().Equals(word.ToLower()));
		if (tmp == null) return; // if recording doesnt exist return
		audioSource.clip = tmp.audioEng;

		if (audioSource != null && audioSource.clip != null)
		{
			if (!audioSource.isPlaying)
			{
				if (slowAudio)
				{
					audioSource.pitch = .7f;
					audioSource.Play();
					slowAudio = false;
				}
				else
				{
					audioSource.pitch = 1;
					audioSource.Play();
                    slowAudio = false;
				}
			}
		}
	}

    /// <summary>
    /// Finds all audio files and calls LoadFile() to load all the sound files
    /// </summary>
    void LoadSounds()
    {
        AudioClip[] wordFiles = Resources.LoadAll<AudioClip>("Words/");
        AudioClip[] sentenceFiles = Resources.LoadAll<AudioClip>("Sentences/");
        Word tmp;
        foreach (var clip in wordFiles)
        {
            tmp = words.Find(w => w.word.ToLower().Equals(clip.name.Substring(0, clip.name.IndexOf("_eng")).ToLower()));
            if(tmp != null)
            {
                tmp.audioEng = clip;
            }
        }
        foreach (var clip in sentenceFiles)
        {
            foreach (var sentence in story.Sentences)
            {
                // if _eng add to english
                // if _thai add to thai
            }
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
		words.Add(new Word(clip.name.Substring(0, clip.name.IndexOf("_")).ToLower(), clip));
	}

	/// <summary>
	/// Loads the entire story from a file
	/// </summary>
	void LoadStory()
	{

		string tmp;
        try
        {
            TextAsset file = Resources.Load("story") as TextAsset;
            tmp = file.text;

            Sentence[] sentences = JsonHelper.FromJson<Sentence>(tmp);
            foreach (Sentence sentence in sentences)
            {
                story.Sentences.Add(sentence);
            }

            file = Resources.Load("ThaiWords") as TextAsset;
            tmp = file.text;
            Word[] jsonWords = JsonHelper.FromJson<Word>(tmp);
            foreach (Word word in jsonWords)
            {
                words.Add(word);
            }

        }
        catch (FileNotFoundException ex)
        {
            Debug.Log("story.json is not found in the Assets Folder. Exception: " + ex.Message);
			return;
		}
		catch (NullReferenceException ex)
		{
			Debug.Log("story.json is not found in the Assets Folder. Exception: " + ex.Message);
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

		if (dir > 0)
		{
			if (storyProgress < story.Phrases.Count)
			{
				phrase.SetPhrase(story.Phrases[storyProgress++].ToString());
			}
		}
		else
		{
			if (storyProgress > 1)
			{
				phrase.SetPhrase(story.Phrases[--storyProgress - 1].ToString());
			}

		}

	}

	/// <summary>
	/// Changes to the next event in the application
	/// </summary>
	public void NextStoryEvent()
	{
        if (storyProgress < story.Phrases.Count)
		{
            if (story.Phrases[storyProgress].ToString().Contains("What color do you like?"))
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Story - Level 1":
                        SceneManager.LoadScene("Round1MidActivity", LoadSceneMode.Additive);
                        break;
                    case "Story - Level 2":
                        break;
                    case "Story - Level 3":
                        break;
                    case "Story - Level 4":
                        break;
                }
                storyProgress++;
            }
            if (!locked)
            {
                HideThaiHelp();
                UpdateText(1);
                StartCoroutine(DestroyPopUp(0));
                audioSource.Stop();
            }
		}
		else
		{
            if (GameObject.FindObjectOfType<GameManager>().progress <= SceneManager.GetActiveScene().buildIndex)
            {
                if (SceneManager.GetActiveScene().name.Equals("Story - Level 1"))
                    GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex+1);
                GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
            }
            if (GameObject.FindObjectOfType<GameManager>().progress < SceneManager.sceneCountInBuildSettings-1)
            {
                if(SceneManager.GetActiveScene().name.Equals("Story - Level 1"))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
	}

	/// <summary>
	/// Changes to the previous event in the application
	/// </summary>
	public void PreviousStoryEvent()
	{
		if (storyProgress > 1)
		{
            HideThaiHelp();
            UpdateText(-1);
            audioSource.Stop();
        }
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
		}

	}

	/// <summary>
	/// instantiates a popUp prefab at the words location
	/// </summary>
	/// <param name="wordInfo">WordInfo of the word to be shown in Thai</param>
	public void ShowThaiPopUp(string word, bool firstLine, PointerEventData eventData)
	{
		HideThaiHelp();
		Vector2 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        Word tmp;

		GameObject popUp;
		if (firstLine)
		{
			pos.y += 1f;
			popUp = Instantiate(popUpTopPrefab, pos, popUpTopPrefab.transform.rotation);
			if ((tmp = words.Find(w => w.word.ToLower().Equals(word.ToLower()))) != null)
			{
                if (tmp.thai.Length > 5)
                {
                    popUp.GetComponentInChildren<TextMesh>().fontSize = 250;
                }
                else
                {
                    popUp.GetComponentInChildren<TextMesh>().fontSize = 350;
                }
                popUp.GetComponentInChildren<TextMesh>().text = tmp.thai;
			}
		}
		else
		{
			pos.y -= 1f;
			popUp = Instantiate(popUpBottomPrefab, pos, popUpTopPrefab.transform.rotation);
			if ((tmp = words.Find(w => w.word.ToLower().Equals(word.ToLower()))) != null)
			{
                if(tmp.thai.Length > 5)
                {
                    popUp.GetComponentInChildren<TextMesh>().fontSize = 250;
                }
                else
                {
                    popUp.GetComponentInChildren<TextMesh>().fontSize = 350;
                }
				popUp.GetComponentInChildren<TextMesh>().text = tmp.thai;
			}
		}
		popUp.transform.SetParent(transform);
		currWord = word;
	}

	public IEnumerator DestroyPopUp(float wait)
	{
		yield return new WaitForSeconds(wait);
		Destroy(GameObject.FindGameObjectWithTag("PopUp"));

	}

	/// <summary>
	/// toggles the slowAudio bool
	/// </summary>
	public void PlaySlowAudio()
	{
		slowAudio = true;
		PlayWord(currWord);
        slowAudio = false;
	}

	/// <summary>
	/// Plays the Audio of the sentences, of the current phrase
	/// </summary>
	public void PlayPhrase()
	{
        float length = 0;
        foreach (Sentence sentence in story.Phrases[storyProgress - 1].sentences)
        {

            if (sentence.AudioEng != null)
            {
                audioSource.clip = sentence.AudioEng;

                if (audioSource != null && audioSource.clip != null)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayDelayed(length);
                        length += audioSource.clip.length + 1; // this may need to change
                    }
                }
            }
        }
    }

	/// <summary>
	/// Shows a summary of the phrase in thai
	/// </summary>
	public void ShowThaiHelpText()
	{
		// Eventually will add a mapper to the words from english to thai and access the translation that way
		if (thaiHelp.text == "")
		{
            string tmp = "";
			StartCoroutine(DestroyPopUp(0));
            foreach (Sentence sentence in story.Phrases[storyProgress-1].sentences)
            {
                tmp += sentence.sentThai + " ";
            }
			thaiHelp.text = tmp;
            ThaiHelpPanel.SetActive(true);
		}
		else
		{
            HideThaiHelp();

        }

	}

	private void HideThaiHelp()
	{
		thaiHelp.text = "";
        ThaiHelpPanel.SetActive(false);
    }

}
