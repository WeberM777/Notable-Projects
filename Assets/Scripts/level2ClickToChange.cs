using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class level2ClickToChange : MonoBehaviour {

	//list of english vocab words
	List <string> vocabwords = new List <string> (){"banana", "leaves", "like", "bread", "wish", "river",
	"dancing", "friend", "bright", "moon", "candle", "music"};

	//list of thai vocab words
	List <string> thaivocabwords = new List <string> (){"กล้วย", "ใบไม้", "ชอบ", "ขนมปัง", "ประสงค์", "แม่น้ำ",
		"การเต้นรำ", "เพื่อน", "สดใส", "ดวงจันทร์", "เทียน", "เพลง"};

    //keeps track of the index for words
    public int currentIndex = 0;
    public int buttonPosition = 0;

    //randomizes the word
    public static int randomWord1;
    //randomizes the word
    public static int randomWord2;
    //randomizes the word
    public static int randomWord3;
    //randomizes the word
    public static int randomWord4;

    //randomizes the correct answer button
    public static int randomButton;
   
	//english button options
	public Text BTNText1;
	public Text BTNText2;
	public Text BTNText3;
	public Text BTNText4;

	//symbol correct or wrong image
	public GameObject Symbol1;
	public GameObject Symbol2;
	public GameObject Symbol3;
	public GameObject Symbol4;

	//symbol image
	public Sprite checkmark;
	public Sprite xmark;

	//thai text on screen
	public Text thai_textshowed = null;
	//vocab placeholder for images on screen
	public Image VocabImage;
	//english audio button
	public AudioSource Eng_RegularAudio;
    //english slow audio button
    public AudioSource answerSound;
    //add audio clip for correct answer
    public AudioClip checkmarkSound;
    //add audio clip for wrong answer
    public AudioClip xmarkSound;
	//next button (left)
    public Button NextButton;

    // Use this for initialization
    void Start()
    {
        //deactivate all symbols
        Symbol1.gameObject.SetActive(false);
        Symbol2.gameObject.SetActive(false);
        Symbol3.gameObject.SetActive(false);
        Symbol4.gameObject.SetActive(false);
        NextButton.interactable = false;
      
    }

	//next button push
	public void nextMouseClick(){
		
        //deactivate all symbols
        Symbol1.gameObject.SetActive(false);
        Symbol2.gameObject.SetActive(false);
        Symbol3.gameObject.SetActive(false);
        Symbol4.gameObject.SetActive(false);
		

        //create count
		if (currentIndex <= 12) {
			currentIndex++;	
            buttonPosition++;
		}
		//loads everything for that screen
		allTheStuff ();
        //after all stuff loads make next button inactive
        NextButton.interactable = false;
	}

    //IMPLMENT GAME LOGIC
    //all the components for each word
    public void allTheStuff()
    {
        //initializes randon variables
        randomWord1 = Random.Range(0, 12);
        randomWord2 = Random.Range(0, 12);
        randomWord3 = Random.Range(0, 12);
        randomWord4 = Random.Range(0, 12);
        randomButton = Random.Range(0, 4);

        if (currentIndex >= 0 && currentIndex != 12)
        {

            if(buttonPosition == 0){
                BTNText3.text = vocabwords[currentIndex];
                BTNText2.text = vocabwords[randomWord2];
                BTNText1.text = vocabwords[randomWord1];
                BTNText4.text = vocabwords[randomWord4];

                if (BTNText1.text == BTNText2.text)
                {
                    randomWord2++;
                    BTNText2.text = vocabwords[randomWord2];
                }
                if (BTNText1.text == BTNText3.text)
                {
                    randomWord1++;
                    BTNText1.text = vocabwords[randomWord1];
                }
                if (BTNText1.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText2.text == BTNText3.text)
                {
                    randomWord2++;
                    BTNText2.text = vocabwords[randomWord2];
                }
                if (BTNText2.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText3.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
            }
            if(buttonPosition == 1){
                BTNText1.text = vocabwords[currentIndex];
                BTNText2.text = vocabwords[randomWord2];
                BTNText3.text = vocabwords[randomWord3];
                BTNText4.text = vocabwords[randomWord4];

                if (BTNText1.text == BTNText2.text)
                {
                    randomWord2++;
                    BTNText2.text = vocabwords[randomWord2];
                }
                if (BTNText1.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText1.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText2.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText2.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText3.text == BTNText4.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
            }
            if(buttonPosition == 2){
                BTNText4.text = vocabwords[currentIndex];
                BTNText2.text = vocabwords[randomWord2];
                BTNText3.text = vocabwords[randomWord3];
                BTNText1.text = vocabwords[randomWord1];

                if (BTNText1.text == BTNText2.text)
                {
                    randomWord2++;
                    BTNText2.text = vocabwords[randomWord2];
                }
                if (BTNText1.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText1.text == BTNText4.text)
                {
                    randomWord1++;
                    BTNText1.text = vocabwords[randomWord1];
                }
                if (BTNText2.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText2.text == BTNText4.text)
                {
                    randomWord2++;
                    BTNText2.text = vocabwords[randomWord2];
                }
                if (BTNText3.text == BTNText4.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
            }
            if(buttonPosition == 3){
                BTNText2.text = vocabwords[currentIndex];
                BTNText4.text = vocabwords[randomWord4];
                BTNText3.text = vocabwords[randomWord3];
                BTNText1.text = vocabwords[randomWord1];

                if (BTNText1.text == BTNText2.text)
                {
                    randomWord1++;
                    BTNText1.text = vocabwords[randomWord1];
                }
                if (BTNText1.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText1.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText2.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText2.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText3.text == BTNText4.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
            }
            if(buttonPosition >= 4){
                BTNText1.text = vocabwords[currentIndex];
                BTNText4.text = vocabwords[randomWord4];
                BTNText3.text = vocabwords[randomWord3];
                BTNText2.text = vocabwords[randomWord2];

                if (BTNText1.text == BTNText2.text)
                {
                    randomWord2++;
                    BTNText2.text = vocabwords[randomWord2];
                }
                if (BTNText1.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText1.text = vocabwords[randomWord3];
                }
                if (BTNText1.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText2.text == BTNText3.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }
                if (BTNText2.text == BTNText4.text)
                {
                    randomWord4++;
                    BTNText4.text = vocabwords[randomWord4];
                }
                if (BTNText3.text == BTNText4.text)
                {
                    randomWord3++;
                    BTNText3.text = vocabwords[randomWord3];
                }

                buttonPosition = 0;
            }
                
            //places in the correct thai word
            thai_textshowed.text = thaivocabwords[currentIndex];

            //use temp string and convert to sprite
            Sprite aTempSprite = Resources.Load(("VocabSprites/" + vocabwords[currentIndex]), typeof(Sprite)) as Sprite;
            //use temp converted sprite to call the correct spirit
            VocabImage.GetComponent<Image>().sprite = aTempSprite;

            //create temp strings to get correct audio names
            string temp1 = "";
            string temp2 = "";
            temp1 = vocabwords[currentIndex];
            temp2 = temp1 + "_eng";

            //use temp string and convert to an audio clip
            AudioClip aTempAudioClip = Resources.Load(("Words/" + temp2), typeof(AudioClip)) as AudioClip;
            //use temp coverted audio clip to retrieve the correct audioclip
            Eng_RegularAudio.GetComponent<AudioSource>().clip = aTempAudioClip;
        }

        //change scenes
        if (currentIndex == 12)
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

    //button1 push
    public void BTN1MouseClick()
    {

        Symbol1.gameObject.SetActive(true);
        //if it equals the correct button
        if (BTNText1.text == vocabwords[currentIndex])
        {
            Symbol1.GetComponent<Image>().sprite = checkmark;
            answerSound.GetComponent<AudioSource>().clip = checkmarkSound;
            NextButton.interactable = true;
        }
        else
        {
            Symbol1.GetComponent<Image>().sprite = xmark;
            answerSound.GetComponent<AudioSource>().clip = xmarkSound;
        }
    }

    //button2 push
    public void BTN2MouseClick()
    {
        Symbol2.gameObject.SetActive(true);
        //if it equals the correct button
        if (BTNText2.text == vocabwords[currentIndex])
        {
            Symbol2.GetComponent<Image>().sprite = checkmark;
            answerSound.GetComponent<AudioSource>().clip = checkmarkSound;
            NextButton.interactable = true;
           

        }else
        {
            Symbol2.GetComponent<Image>().sprite = xmark;
            answerSound.GetComponent<AudioSource>().clip = xmarkSound;
         
        }
     

    }

    //button3 push
    public void BTN3MouseClick()
    {
        Symbol3.gameObject.SetActive(true);
        //if it equals the correct button
        if (BTNText3.text == vocabwords[currentIndex])
        {
            Symbol3.GetComponent<Image>().sprite = checkmark;
            answerSound.GetComponent<AudioSource>().clip = checkmarkSound;
            NextButton.interactable = true;
        }
        else
        {
            Symbol3.GetComponent<Image>().sprite = xmark;
            answerSound.GetComponent<AudioSource>().clip = xmarkSound;
        }
    }

    //button4 push
    public void BTN4MouseClick()
    {
        Symbol4.gameObject.SetActive(true);
        //if it equals the correct button
        if (BTNText4.text == vocabwords[currentIndex])
        {
            Symbol4.GetComponent<Image>().sprite = checkmark;
            answerSound.GetComponent<AudioSource>().clip = checkmarkSound;
            NextButton.interactable = true;
           
        }else
        {
            Symbol4.GetComponent<Image>().sprite = xmark;
            answerSound.GetComponent<AudioSource>().clip = xmarkSound;
        }

    }
}
