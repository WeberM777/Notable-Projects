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
    private bool audioLock = false;
	private Word currWord;


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
        currWord = null;
		// level = 0;

		LoadStory();
		//LoadSounds(); // Load the sounds from file
		StartStory();

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartStory()
	{
		UpdateText(1);
        if (SceneManager.GetActiveScene().name.Equals("Story - Level 1"))
        {
            StartPhraseAudio();
        }
	}

	/// <summary>
	/// Plays the sound of a word
	/// </summary>
	/// <param name="word">The word to be spoken</param>
	public void PlayWord(Word word)
	{
		// finds the appropriate audioclip for the given word
		if (word == null) return; // if recording doesnt exist return

        if(slowAudio)
        {
            audioSource.clip = word.audioSlow;
        }
        else
        {
            audioSource.clip = word.audioEng;
        }

		if (audioSource != null && audioSource.clip != null)
		{
			if (!audioSource.isPlaying)
			{
                audioSource.Play();
                slowAudio = false;
			}
		}
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
            int num = 0;
            foreach (Sentence sentence in sentences)
            {
                sentence.sentenceNum = num;
                story.Sentences.Add(sentence);
                num++;
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
        if (audioLock)
        {
            return;
        }
        if (storyProgress < story.Phrases.Count)
        {
            if (!locked)
            {
                HideThaiHelp();
                UpdateText(1);
                StartCoroutine(DestroyPopUp(0));
                audioSource.Stop();
                if (SceneManager.GetActiveScene().name.Equals("Story - Level 1"))
                {
                    StartPhraseAudio();
                }
            }
        }
        else
        {
            if (GameObject.FindObjectOfType<GameManager>().progress <= SceneManager.GetActiveScene().buildIndex)
            {
                GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
            }
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneLoader.Instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneLoader.Instance.LoadNextScene("Menu");
            }
        }

	}

	/// <summary>
	/// Changes to the previous event in the application
	/// </summary>
	public void PreviousStoryEvent()
	{
        if (audioLock)
        {
            return;
        }
		if (storyProgress > 1)
		{
            HideThaiHelp();
            UpdateText(-1);
            audioSource.Stop();
            if (SceneManager.GetActiveScene().name.Equals("Story - Level 1"))
            {
                StartPhraseAudio();
            }
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
        if (audioLock)
        {
            return;
        }

        HideThaiHelp();
		Vector2 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        Word tmp = words.Find(w => w.word.ToLower().Equals(word.ToLower()));

		GameObject popUp;
        if (tmp != null)
        {
            tmp.audioEng = Resources.Load("Words/" + tmp.word + "_eng") as AudioClip;
            tmp.audioSlow = Resources.Load("Words/" + tmp.word + "_slow") as AudioClip;
            PlayWord(tmp);
            if (!SceneManager.GetActiveScene().name.Equals("Story - Level 4"))
            {
                if (firstLine)
                {
                    pos.y += 1f;
                    popUp = Instantiate(popUpTopPrefab, pos, popUpTopPrefab.transform.rotation);
                    if (tmp.thai.Length > 6)
                    {
                        popUp.GetComponentInChildren<TextMesh>().fontSize = 200;
                    }
                    else
                    {
                        popUp.GetComponentInChildren<TextMesh>().fontSize = 350;
                    }
                    popUp.GetComponentInChildren<TextMesh>().text = tmp.thai;
                }
                else
                {
                    pos.y -= 1f;
                    popUp = Instantiate(popUpBottomPrefab, pos, popUpTopPrefab.transform.rotation);
                    if (tmp.thai.Length > 6)
                    {
                        popUp.GetComponentInChildren<TextMesh>().fontSize = 200;
                    }
                    else
                    {
                        popUp.GetComponentInChildren<TextMesh>().fontSize = 350;
                    }
                    popUp.GetComponentInChildren<TextMesh>().text = tmp.thai;
                }
                popUp.transform.SetParent(transform);
            }
            currWord = tmp;
        }
	}

    /// <summary>
    /// Destroys any pop up on the screen
    /// </summary>
    /// <param name="wait"></param>
    /// <returns></returns>
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
	private IEnumerator PlayPhrase()
	{
        if (audioLock)
        {
            yield break;
        }
        if (!audioSource.isPlaying)
        {
            audioLock = true;
            foreach (Sentence sentence in story.Phrases[storyProgress - 1].sentences)
            {
                if (sentence.sentenceNum+1 < 10)
                {
                    sentence.AudioEng = Resources.Load("Sentences/Phrase 0" + (sentence.sentenceNum+1) + "_eng") as AudioClip;
                }
                else
                {
                    sentence.AudioEng = Resources.Load("Sentences/Phrase " + (sentence.sentenceNum+1) + "_eng") as AudioClip;
                }

                if (sentence.AudioEng != null)
                {
                    audioSource.clip = sentence.AudioEng;

                    if (audioSource != null && audioSource.clip != null)
                    {

                        audioSource.Play();
                        yield return new WaitForSeconds(audioSource.clip.length + .5f);
                    }
                }
            }
            audioLock = false;
        }
    }

    /// <summary>
    /// Plays the phrase audioclip
    /// </summary>
    public void StartPhraseAudio()
    {
        StartCoroutine(PlayPhrase());
    }

    /// <summary>
    /// Shows a summary of the phrase in thai
    /// </summary>
    private IEnumerator ShowThaiHelpText()
	{
        // Eventually will add a mapper to the words from english to thai and access the translation that way
        if (thaiHelp.text == "")
		{
            if (audioLock)
            {
                yield break;
            }

            string tmp = "";
			StartCoroutine(DestroyPopUp(0));
            foreach (Sentence sentence in story.Phrases[storyProgress-1].sentences)
            {
                tmp += sentence.sentThai;
            }
			thaiHelp.text = tmp;
            ThaiHelpPanel.SetActive(true);

            if (!audioSource.isPlaying)
            {
                audioLock = true;
                foreach (Sentence sentence in story.Phrases[storyProgress - 1].sentences)
                {
                    if (sentence.sentenceNum < 10)
                    {
                        sentence.AudioEng = Resources.Load("Sentences/Phrase 0" + (sentence.sentenceNum + 1) + "_thai") as AudioClip;
                    }
                    else
                    {
                        sentence.AudioEng = Resources.Load("Sentences/Phrase " + (sentence.sentenceNum + 1) + "_thai") as AudioClip;
                    }

                    if (sentence.AudioEng != null)
                    {
                        audioSource.clip = sentence.AudioEng;

                        if (audioSource != null && audioSource.clip != null)
                        {

                            audioSource.Play();
                            yield return new WaitForSeconds(audioSource.clip.length + .4f);
                        }
                    }
                }
                audioLock = false;
            }
        }
		else
		{
            HideThaiHelp();

        }

    }

    public void ThaiHelp()
    {
        StartCoroutine(ShowThaiHelpText());
    }

    /// <summary>
    /// Hides the thai help panel
    /// </summary>
	private void HideThaiHelp()
	{
		thaiHelp.text = "";
        ThaiHelpPanel.SetActive(false);
    }

}
