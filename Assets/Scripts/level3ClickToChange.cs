using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class level3ClickToChange : MonoBehaviour
{

    public int currentIndex; //keeps track of the index for words

    //list of english vocab words
    List<string> vocabwords = new List<string>(){"banana", "leaves", "like", "bread", "wish", "river",
    "dancing", "friend", "bright", "moon", "candle", "music"};

    //list of thai vocab words
    List<string> thaivocabwords = new List<string>(){"กล้วย", "ใบไม้", "ชอบ", "ขนมปัง", "ประสงค์", "แม่น้ำ",
        "การเต้นรำ", "เพื่อน", "สดใส", "ดวงจันทร์", "เทียน", "เพลง"};

    //english text on screen
    public Text textshowed = null;
    //thai text on screen
    public Text thai_textshowed = null;
    //vocab placeholder for images on screen
    public Image VocabImage;
    //english audio button
    public AudioSource Eng_RegularAudio;
    //previous button (left)
    public Button NextButton;


    //next button push
    public void nextMouseClick()
    {

        if (currentIndex <= 12)
        {
            currentIndex++;
        }
        //loads everything for that screen
        allTheStuff();
    }

    //previous button push
    public void previousMouseClick()
    {
        if (currentIndex <= 12)
        {
            currentIndex--;
        }
        //loads everthing for that screen
        //ShowText();
        allTheStuff();
    }

    //all the components for each word
    public void allTheStuff()
    {
        if (currentIndex >= 0 && currentIndex != 12)
        {
            
            //Control whether the next button is interactable or not
            //NextButton.interactable = false;

            //textshowed.text = vocabwords[currentIndex];
            textshowed.text = vocabwords[currentIndex];

            //textshowed.text = vocabwords[currentIndex];
            thai_textshowed.text = thaivocabwords[currentIndex];

            Sprite aTempSprite = Resources.Load(("VocabSprites/" + vocabwords[currentIndex]), typeof(Sprite)) as Sprite;//string converted to sprite
            VocabImage.GetComponent<Image>().sprite = aTempSprite;

            string temp1 = "";
            string temp2 = "";
            temp1 = vocabwords[currentIndex];
            temp2 = temp1 + "_eng";

            AudioClip aTempAudioClip = Resources.Load(("Words/" + temp2), typeof(AudioClip)) as AudioClip;
            Eng_RegularAudio.GetComponent<AudioSource>().clip = aTempAudioClip;

        }

        if (currentIndex == 12)
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
    }
}
