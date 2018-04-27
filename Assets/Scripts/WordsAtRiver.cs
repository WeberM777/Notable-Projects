using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class WordsAtRiver : MonoBehaviour
{
    private SpeechRecognizerManager speechManager = null;
    private GameObject destination;
    private GameObject player;

    private int wordCount;
    private int matchedCount = 0;
    private Vector3 startPosition;
    private Vector3 destPosition;

    public Text gameText;

    private string currWord = "";
    List<string> words = new List<string> { "green", "yellow", "blue", "purple" };
    private bool isListening = false;

    //public static System.Random rnd = new System.Random();

    private IEnumerator StartLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene("Main");
    }

    void Awake()
    {
        destination = GameObject.FindWithTag("Destination");
        player = GameObject.FindWithTag("Player");
        startPosition = player.transform.position;
        destPosition = destination.transform.position;
        gameText.text = "Get Ready!";
        wordCount = words.Count;
    }

    void Start()
    {
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

        player.transform.position = Vector2.Lerp(startPosition, destPosition, (float)matchedCount / wordCount);

        if (SpeechRecognizerManager.IsAvailable() && isListening)
        {
            speechManager.StartListening(3, "en-US");
        }

        if (player.transform.position.Equals(destPosition))
        {
            Debug.Log("Made it to the river");
        }

    }

    void OnDestroy()
    {
        if (speechManager != null)
            speechManager.Release();
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

        // Need to parse
        string[] texts = results.Split(new string[] { SpeechRecognizerManager.RESULT_SEPARATOR }, System.StringSplitOptions.None);


        Debug.Log("Speech results:\n   " + string.Join("\n   ", texts));

        validateSpeech(texts);
    }

    private void validateSpeech(string[] texts)
    {
        foreach (string text in texts)
        {
            if (text.ToLower().Equals(currWord.ToLower()))
            {
                matchedCount++;
                return;
            }
        }
        matchedCount--;
        StartCoroutine(NextWord());
    }

    private IEnumerator NextWord()
    {
        Debug.Log("Made it to the NextWord()");
        yield return new WaitForSeconds(3);
        currWord = "";
        int index = Random.Range(0, words.Count);
        currWord = words[index];
        gameText.text = currWord;
        if (SpeechRecognizerManager.IsAvailable())
        {
            if (!isListening)
            {
                isListening = true;
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

    #endregion

    #region DEBUG

    private void DebugLog(string message)
    {
        Debug.Log(message);
    }

    #endregion
}
