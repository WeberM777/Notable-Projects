using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1GameManager : MonoBehaviour {

    public GameObject instructionPanel;
    public List<TMPro.TextMeshProUGUI> SentenceTextMesh;
    public GameObject rudee;
    public GameObject lamon;
    public GameObject gameOverPanel;
    public Text status;
    public GameObject nextButton;
    public GameObject tryAgainButton;
    public GameObject listenButton;
    public GameObject endSpeechButton;
    private SpeechRecognizerManager speech;
    private List<string> sentences;
    private List<AudioClip> sentenceAudio;
    private bool listening = false;
    private int progress = 0; // what progress they are in the game
    private int movement;
    private bool voiceOpen = false; // value that if true tells the speech recognizer it should be listening for a response
    private Vector2 origRudee;
    private Vector2 origLamon;
    private int misses; // how many missed pronounced phrases

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.Log("Speech recognition is only available on Android platform.");
        }

        if (!SpeechRecognizerManager.IsAvailable())
        {
            Debug.Log("Speech recognition is not available on this device.");
        }

        // We pass the game object's name that will receive the callback messages.
        speech = new SpeechRecognizerManager(gameObject.name);
        sentenceAudio = new List<AudioClip>();

        sentenceAudio.Add(Resources.Load<AudioClip>("Sentences/phrase 08_eng"));
        sentenceAudio.Add(Resources.Load<AudioClip>("Sentences/phrase 15_eng"));
        sentenceAudio.Add(Resources.Load<AudioClip>("Sentences/phrase 31_eng"));
        sentenceAudio.Add(Resources.Load<AudioClip>("Sentences/phrase 35_eng"));

        // Adds sentences to the game
        sentences = new List<string>();
        sentences.Add("Peach is sad");
        sentences.Add("She will make a wish at the river.");
        sentences.Add("I wish I had a friend");
        sentences.Add("I like pink.");
        movement = (Screen.width - 200) / 2 / sentences.Count; // calculates the movement of the characters based on the screen size
        origLamon = lamon.transform.position;
        origRudee = rudee.transform.position;
    }
    void OnDestroy()
    {
        if (speech != null)
            speech.Release();
    }

    // Update is called once per frame
    void Update () {
        // allows for a reprompt of the phrase if the speech recognizer times out
        if (SpeechRecognizerManager.IsAvailable() && voiceOpen)
        {
            if (!listening)
            {
                listening = true;
                speech.StartListening(3, "en-US");
            }
        }
    }

    /// <summary>
    /// Event handler, used for debugging purposes
    /// </summary>
    /// <param name="e"></param>
    void OnSpeechEvent(string e)
    {
        switch (int.Parse(e))
        {
            case SpeechRecognizerManager.EVENT_SPEECH_READY:
                Debug.Log("Ready for speech");
                break;
            case SpeechRecognizerManager.EVENT_SPEECH_BEGINNING:
                Debug.Log("User started speaking");
                break;
            case SpeechRecognizerManager.EVENT_SPEECH_END:
                Debug.Log("User stopped speaking");
                break;
        }
    }

    /// <summary>
    /// Event handler for when the speech recognizer returns results
    /// </summary>
    /// <param name="results"></param>
    void OnSpeechResults(string results)
    {
        listening = false;
        endSpeechButton.SetActive(false);
        // Need to parse
        string[] texts = results.Split(new string[] { SpeechRecognizerManager.RESULT_SEPARATOR }, System.StringSplitOptions.None);
        

        Debug.Log("Speech results:\n   " + string.Join("\n   ", texts));

        validateSpeech(texts);
    }

    /// <summary>
    /// Event handler for errors, mainly for debugging
    /// </summary>
    /// <param name="error"></param>
    void OnSpeechError(string error)
    {
        switch (int.Parse(error))
        {
            case SpeechRecognizerManager.ERROR_AUDIO:
                Debug.Log("Error during recording the audio.");
                break;
            case SpeechRecognizerManager.ERROR_CLIENT:
                Debug.Log("Error on the client side.");
                break;
            case SpeechRecognizerManager.ERROR_INSUFFICIENT_PERMISSIONS:
                Debug.Log("Insufficient permissions. Do the RECORD_AUDIO and INTERNET permissions have been added to the manifest?");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK:
                Debug.Log("A network error occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK_TIMEOUT:
                Debug.Log("A network timeout occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NO_MATCH:
                Debug.Log("No recognition result matched.");
                break;
            case SpeechRecognizerManager.ERROR_NOT_INITIALIZED:
                Debug.Log("Speech recognizer is not initialized.");
                break;
            case SpeechRecognizerManager.ERROR_RECOGNIZER_BUSY:
                Debug.Log("Speech recognizer service is busy.");
                break;
            case SpeechRecognizerManager.ERROR_SERVER:
                Debug.Log("Server sends error status.");
                break;
            case SpeechRecognizerManager.ERROR_SPEECH_TIMEOUT:
                Debug.Log("No speech input.");
                break;
            default:
                break;
        }

        listening = false;
    }

    /// <summary>
    /// Enables all Game objects and prepares them for the game
    /// </summary>
    public void StartGame()
    {
        instructionPanel.SetActive(false);
        status.text = "Get ready!";
        StartCoroutine(NextSentence());
    }

    /// <summary>
    /// Moves on the the next sentence in the list.  This is called at the beginning of the game and after each correct sentence
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextSentence()
    {

        listenButton.SetActive(false);
        nextButton.SetActive(false);
        tryAgainButton.SetActive(false);
        audioSource.Stop();

        resetTMs();
        yield return new WaitForSeconds(1);
        misses = 0;
        status.text = "";
        voiceOpen = true;
        int index = Random.Range(0, 3);
        TextMeshProUGUI TMP = SentenceTextMesh[index];
        TMP.text = sentences[progress];
        audioSource.clip = sentenceAudio[progress];
        if (SpeechRecognizerManager.IsAvailable()) {
            if(!listening)
            {
                listening = true;
                endSpeechButton.GetComponentInChildren<Text>().text = "Touch here when finished speaking.";
                endSpeechButton.SetActive(true);
                speech.StartListening(3, "en-US");
            }
        }
    }


    /// <summary>
    /// Validates if what the user said matches the sentence given
    /// </summary>
    /// <param name="texts"></param>
    private void validateSpeech(string[] texts)
    {

        foreach (string text in texts)
        {
            if (text.ToLower().Equals(sentences[progress].ToLower()) || misses > 1)
            {
                progress++;
                if (progress < sentences.Count)
                {

                    voiceOpen = false;
                    movePeopleForward();
                    if (misses > 1)
                    {
                        status.text = "Incorrect ไม่ถูกต้อง";
                    }
                    else
                    {
                        status.text = "Good Job งานที่ดี";
                    }
                    listenButton.SetActive(true);
                    nextButton.SetActive(true);
                }
                else
                {
                    movePeopleForward();
                    endGame();
                }

                return;
            }
        }
        //movePeopleBackward();
        TryAgain();
    }

    /// <summary>
    /// Moves the characters on the screen
    /// </summary>
    private void movePeopleForward()
    {
        Vector2 pos = new Vector2(rudee.transform.position.x - movement, rudee.transform.position.y);
        rudee.transform.position = pos;
        pos = new Vector2(lamon.transform.position.x + movement, lamon.transform.position.y);
        lamon.transform.position = pos;
    }

    /// <summary>
    /// Moves characters backward
    /// </summary>
    private void movePeopleBackward() // this is not working right now
    {
        Vector2 pos;
        if (rudee.transform.position.x + movement < -50 || lamon.transform.position.x - movement < 50)
        {
            rudee.transform.position = origRudee;
            lamon.transform.position = origLamon;
        }
        else
        {
            pos = new Vector2(rudee.transform.position.x + movement, rudee.transform.position.y);
            rudee.transform.position = pos;
            pos = new Vector2(lamon.transform.position.x - movement, lamon.transform.position.y);
            lamon.transform.position = pos;
        }
    }

    /// <summary>
    /// Resets all the text meshes before each new round
    /// </summary>
    private void resetTMs()
    {
        foreach (var TM in SentenceTextMesh)
        {
            TM.text = "";
        }
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    private void endGame()
    {
        status.text = "";
        voiceOpen = false;
        resetTMs();
        gameOverPanel.SetActive(true);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneDelay(2));
    }
    private IEnumerator LoadNextSceneDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (GameObject.FindObjectOfType<GameManager>().progress <= SceneManager.GetActiveScene().buildIndex)
        {
            GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
        }
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// Called when users input is not validated correctly
    /// </summary>
    /// <returns></returns>
    private void TryAgain()
    {
        misses++;
        voiceOpen = false;
        status.text = "Try Again ลองอีกครั้ง";
        tryAgainButton.SetActive(true);
        listenButton.SetActive(true);
    }
    public void SentenceAudio()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StartNextSentence()
    {
        StartCoroutine(NextSentence());
        audioSource.Stop();
    }

    public void StartTryAgain()
    {
        status.text = "";
        audioSource.Stop();
        listenButton.SetActive(false);
        nextButton.SetActive(false);
        tryAgainButton.SetActive(false);
        endSpeechButton.GetComponentInChildren<Text>().text = "Touch here when finished speaking.";
        endSpeechButton.SetActive(true);
        voiceOpen = true;
        audioSource.Stop();
    }

    public void StopSeech()
    {
        endSpeechButton.GetComponentInChildren<Text>().text = "Processing . . .";
        speech.StopListening();
    }
}
