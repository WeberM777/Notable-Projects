using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class clickToChange : MonoBehaviour {

	/* Not Used -Text list
	public static List<string> vocabTextArr;
	private string[] vocabReadRows;
	public string vocabTextFile = "vocab";
	private TextAsset vocabTextAsset;
	*/

	public int currentIndex; //keeps track of the index for words

    ////list of english vocab words
    //List<string> vocabwords = new List<string>(){"banana", "leaves", "like", "bread", "wish", "river",
    //"dancing", "friend", "bright", "moon", "candle", "music"};

    ////list of thai vocab words
    //List<string> thaivocabwords = new List<string>(){"กล้วย", "ใบไม้", "ชอบ", "ขนมปัง", "ประสงค์", "แม่น้ำ",
        //"การเต้นรำ", "เพื่อน", "สดใส", "ดวงจันทร์", "เทียน", "เพลง"};

	//english text on screen
	public Text textshowed = null;
	//thai text on screen
	public Text thai_textshowed = null;
	//vocab placeholder for images on screen
	public Image VocabImage;
	//english audio button
	public AudioSource Eng_RegularAudio;
	//english slow audio button
	public AudioSource Eng_SlowAudio;
	//previous button (left)
	public Button PreviousButton;

	//banana
	public AudioClip banana_eng;
	public Sprite banana;
	//leaves
	public AudioClip leaves_eng;
	public Sprite leaves;
	//like
	public AudioClip like_eng;
	public Sprite like;
	//bread
	public AudioClip bread_eng;
	public Sprite bread;
	//wish
	public AudioClip wish_eng;
	public Sprite wish;
	//river
	public AudioClip river_eng;
	public Sprite river;

	//string m_textshowed;
	//public string currentText = "";
	//public float delay = 2.1f;

	/*The rest of the vocab words*/
	//moon
	//bright
	//candles
	//music
	//dancing
	//friend

	/*
	// Use this for initialization
	void Start () {
		StartCoroutine(ShowText());
	}*/

	//next button push
	public void nextMouseClick(){
		//vocabTextAsset = (TextAsset)Resources.Load(vocabTextFile);
		//vocabTextArr = vocabTextAsset.text.Split ('\n').ToList ();

		if (currentIndex <= 6) {
			currentIndex++;	
		}
		//loads everything for that screen
		allTheStuff ();
	}

	//previous button push
	public void previousMouseClick(){
		if (currentIndex <= 6) {
			currentIndex--;
		}
		//loads everthing for that screen
		//ShowText();
		allTheStuff ();
	}

	//all the components for each word
	public void allTheStuff(){

        //while (currentIndex >= 0){
            
        //    textshowed.text = vocabwords[currentIndex];
        //    thai_textshowed.text = thaivocabwords[currentIndex];
        //    VocabImage.GetComponent<Image>().sprite = Resources.Load < Sprite > (vocabwords[currentIndex]);
        //    Eng_RegularAudio.GetComponent<AudioSource>().clip = banana_eng;


        //}





		//pull in images and audio for word
		if (currentIndex == 0) {

            textshowed.text = "banana";
			thai_textshowed.text = "กล้วย";
			VocabImage.GetComponent<Image> ().sprite = banana;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = banana_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = banana_eng;
			PreviousButton.interactable = false;
		}

		if (currentIndex == 1) {

			//ShowText ();
			textshowed.text = "leaves";
			thai_textshowed.text = "ใบไม้";
			VocabImage.GetComponent<Image> ().sprite = leaves;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = leaves_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = leaves_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 2) {

			textshowed.text = "like";
			thai_textshowed.text = "ชอบ";
			VocabImage.GetComponent<Image> ().sprite = like;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = like_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = like_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 3) {
            
			textshowed.text = "bread";
			thai_textshowed.text = "ขนมปัง";
			VocabImage.GetComponent<Image> ().sprite = bread;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = bread_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = bread_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 4) {

			textshowed.text = "wish";
			thai_textshowed.text = "ประสงค์";
			VocabImage.GetComponent<Image> ().sprite = wish;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = wish_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = wish_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 5) {

			textshowed.text = "river";
			thai_textshowed.text = "แม่น้ำ";
			VocabImage.GetComponent<Image> ().sprite = river;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = river_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = river_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 6) {

			changeScenes2 ();

		}
			
	}
		

	//change the scene to Color Activity (then that scene is programed to go to story
	public void changeScenes2() {

        FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene("Level1Round1Activity");
	}

	/*public IEnumerator ShowText(){

		m_textshowed = textshowed.text;

		for(int i = 0; i < m_textshowed.Length; i++){
			currentText = m_textshowed.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}

			
	}*/
}
