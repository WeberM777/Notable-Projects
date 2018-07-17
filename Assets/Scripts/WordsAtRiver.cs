using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class WordsAtRiver : MonoBehaviour
{
    private SpeechRecognizerManager speechManager = null;
    private GameObject destination;
    private GameObject player;

    private AudioSource audioSource;
    public GameObject listenButton;
    public GameObject nextButton;
    public GameObject endSpeechButton;
    public Text status;
    public GameObject gameOverPanel;
    public GameObject instructionPanel;
    public GameObject gameOverMessage;

    private int wordCount;
    private int matchedCount = 0;
    private int attempts = 0;
    private Vector3 startPosition;
    private Vector3 destPosition;

    public Text gameText;

    private string currWord = "";
    List<string> words = new List<string> { "banana", "like", "peach", "flower", "music", "dance" };
    private bool isListening = false;

    //public static System.Random rnd = new System.Random();

    private IEnumerator StartLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        destination = GameObject.FindWithTag("Destination");
        player = GameObject.FindWithTag("Player");
        startPosition = player.transform.position;
        destPosition = destination.transform.position;
        gameText.text = "Get Ready!";
        wordCount = words.Count;
    }

    void Start()
    {
        if (Application.isEditor)
        {
            endGame();
        }
        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.Log("Speech recognition is only available on Android platform.");
            return;
        }

        if (!SpeechRecognizerManager.IsAvailable())
        {
            Debug.Log("Speech recognition is not available on this device.");
            return;
        }

        // We pass the game object's name that will receive the callback messages.
        speechManager = new SpeechRecognizerManager(gameObject.name);


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            matchedCount--;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            matchedCount++;
        }

        if (SpeechRecognizerManager.IsAvailable() && isListening)
        {
            speechManager.StartListening(3, "en-US");
        }

    }

    void OnDestroy()
    {
        if (speechManager != null)
            speechManager.Release();
    }

    private void ChangeScene()
    {
        if (GameObject.FindObjectOfType<GameManager>().progress <= SceneManager.GetActiveScene().buildIndex)
        {
            GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
        }
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    #region SPEECH_CALLBACKS

    void OnSpeechEvent(string e)
    {
        switch (int.Parse(e))
        {
            case SpeechRecognizerManager.EVENT_SPEECH_READY:
                DebugLog("Ready for speech");
                break;
            case SpeechRecognizerManager.EVENT_SPEECH_BEGINNING:
                DebugLog("User started speaking");
                break;
            case SpeechRecognizerManager.EVENT_SPEECH_END:
                DebugLog("User stopped speaking");
                break;
        }
    }

    void OnSpeechResults(string results)
    {
        isListening = false;
        endSpeechButton.SetActive(false);

        // Need to parse
        string[] texts = results.Split(new string[] { SpeechRecognizerManager.RESULT_SEPARATOR }, System.StringSplitOptions.None);


        Debug.Log("Speech results:\n   " + string.Join("\n   ", texts));

        validateSpeech(texts);
    }

    public void StartGame()
    {
        instructionPanel.SetActive(false);
        status.text = "Get ready!";
        NextWord();
    }

    private void validateSpeech(string[] texts)
    {
        attempts++;
        foreach (string text in texts)
        {
            if (text.ToLower().Equals(currWord.ToLower()))
            {
                matchedCount++;
                player.transform.position = Vector2.Lerp(startPosition, destPosition, (float)matchedCount / wordCount);
                status.text = "Good Job งานที่ดี";
                listenButton.SetActive(true);
                nextButton.SetActive(true);
                if (player.transform.position.Equals(destPosition))
                {
                    endGame();
                }
                return;
            }
        }
        if(attempts > 8)
        {
            endLosingGame();
        }
        status.text = "Incorrect ไม่ถูกต้อง";
        listenButton.SetActive(true);
        nextButton.SetActive(true);
        matchedCount--;
        player.transform.position = Vector2.Lerp(startPosition, destPosition, (float)matchedCount / wordCount);
    }

    private void NextWord()
    {

        listenButton.SetActive(false);
        nextButton.SetActive(false);
        audioSource.Stop();

        Debug.Log("Made it to the NextWord()");
        currWord = "";
        status.text = "";
        int index = Random.Range(0, words.Count);
        currWord = words[index];
        gameText.text = currWord;
        audioSource.clip = Resources.Load(currWord + "_eng") as AudioClip;
        if (SpeechRecognizerManager.IsAvailable())
        {
            if (!isListening)
            {
                isListening = true;
                endSpeechButton.GetComponentInChildren<Text>().text = "Touch here when finished speaking.";
                endSpeechButton.SetActive(true);
                speechManager.StartListening(3, "en-US");
            }
        }
    }

    void OnSpeechError(string error)
    {
        switch (int.Parse(error))
        {
            case SpeechRecognizerManager.ERROR_AUDIO:
                DebugLog("Error during recording the audio.");
                break;
            case SpeechRecognizerManager.ERROR_CLIENT:
                DebugLog("Error on the client side.");
                break;
            case SpeechRecognizerManager.ERROR_INSUFFICIENT_PERMISSIONS:
                DebugLog("Insufficient permissions. Do the RECORD_AUDIO and INTERNET permissions have been added to the manifest?");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK:
                DebugLog("A network error occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK_TIMEOUT:
                DebugLog("A network timeout occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NO_MATCH:
                DebugLog("No recognition result matched.");
                break;
            case SpeechRecognizerManager.ERROR_NOT_INITIALIZED:
                DebugLog("Speech recognizer is not initialized.");
                break;
            case SpeechRecognizerManager.ERROR_RECOGNIZER_BUSY:
                DebugLog("Speech recognizer service is busy.");
                break;
            case SpeechRecognizerManager.ERROR_SERVER:
                DebugLog("Server sends error status.");
                break;
            case SpeechRecognizerManager.ERROR_SPEECH_TIMEOUT:
                DebugLog("No speech input.");
                break;
            default:
                break;
        }

        isListening = false;
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    private void endGame()
    {
        status.text = "";
        nextButton.SetActive(false);
        listenButton.SetActive(false);
        gameOverPanel.SetActive(true);
        LoadNextScene();
    }

    private void endLosingGame()
    {
        status.text = "";
        nextButton.SetActive(false);
        listenButton.SetActive(false);
        gameOverMessage.GetComponent<Text>().text = "Game Over\n|nจบเกม";
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
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneLoader.Instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneLoader.Instance.LoadNextScene("Menu");
        }
    }

    public void WordAudio()
    {
        // Add sentence audio here
        if (!audioSource.isPlaying && audioSource != null)
            audioSource.Play();
    }

    public void StartNextWord()
    {
        NextWord();
        audioSource.Stop();
    }

    public void StopSpeech()
    {
        endSpeechButton.GetComponentInChildren<Text>().text = "Processing . . .";
        speechManager.StopListening();
    }

    #endregion

    #region DEBUG

    private void DebugLog(string message)
    {
        Debug.Log(message);
    }

    #endregion
}
