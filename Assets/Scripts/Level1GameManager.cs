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
    private SpeechRecognizerManager speech;
    private List<string> sentences;
    private bool listening = false;
    private string message = "";
    private int progress = 0;
    private int movement;
    private bool voiceOpen = false;
    private Vector2 origRudee;
    private Vector2 origLamon;

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
        sentences = new List<string>();
        sentences.Add("She is glad");
        sentences.Add("He is sad");
        sentences.Add("She likes pink");
        sentences.Add("His brother helped him");
        sentences.Add("He will make a wish");
        sentences.Add("The Moon is bright");
        sentences.Add("I wish I had a friend");
        sentences.Add("He likes pink too");
        sentences.Add("His wish came true");
        movement = (Screen.width - 500) / 2 / sentences.Count;
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
        if (SpeechRecognizerManager.IsAvailable() && voiceOpen)
        {
            if (!listening)
            {
                listening = true;
                speech.StartListening(3, "en-US");
            }
        }
    }
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

    void OnSpeechResults(string results)
    {
        listening = false;

        // Need to parse
        string[] texts = results.Split(new string[] { SpeechRecognizerManager.RESULT_SEPARATOR }, System.StringSplitOptions.None);
        

        Debug.Log("Speech results:\n   " + string.Join("\n   ", texts));

        validateSpeech(texts);
    }

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

    public void StartGame()
    {
        instructionPanel.SetActive(false);
        status.text = "Get ready!";
        StartCoroutine(NextSentence());
    }

    private IEnumerator NextSentence()
    {
        resetTMs();
        yield return new WaitForSeconds(1);
        status.text = "";
        voiceOpen = true;
        int index = Random.Range(0, 3);
        TextMeshProUGUI TMP = SentenceTextMesh[index];
        TMP.text = sentences[progress];
        if (SpeechRecognizerManager.IsAvailable()) {
            if(!listening)
            {
                listening = true;
                speech.StartListening(3, "en-US");
            }
        }
    }

    private void validateSpeech(string[] texts)
    {
        foreach (string text in texts) 
        {
            if(text.ToLower().Equals(sentences[progress].ToLower()))
            {
                progress++;
                if(progress < sentences.Count)
                {
                    voiceOpen = false;
                    movePeopleForward();
                    status.text = "Good Job งานที่ดี";
                    StartCoroutine(NextSentence());
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
        StartCoroutine(tryAgain());
    }

    private void movePeopleForward()
    {
        Vector2 pos = new Vector2(rudee.transform.position.x - movement, rudee.transform.position.y);
        rudee.transform.position = pos;
        pos = new Vector2(lamon.transform.position.x + movement, lamon.transform.position.y);
        lamon.transform.position = pos;
    }

    private void movePeopleBackward()
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

    private void resetTMs()
    {
        foreach (var TM in SentenceTextMesh)
        {
            TM.text = "";
        }
    }

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
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator tryAgain()
    {
        voiceOpen = false;
        status.text = "Try Again ลองอีกครั้ง";
        yield return new WaitForSeconds(1);
        status.text = "";
        voiceOpen = true;
    }
}
