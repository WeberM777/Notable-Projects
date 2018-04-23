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
	public Sprite BananaVisual;
	//leaves
	public AudioClip leaves_eng;
	public Sprite LeavesVisual;
	//like
	public AudioClip like_eng;
	public Sprite LikeVisual;
	//bread
	public AudioClip bread_eng;
	public Sprite BreadVisual;
	//wish
	public AudioClip wish_eng;
	public Sprite WishVisual;
	//river
	public AudioClip river_eng;
	public Sprite RiverVisual;

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

		//pull in images and audio for word
		if (currentIndex == 0) {

			textshowed.text = "banana";
			thai_textshowed.text = "กล้วย";
			VocabImage.GetComponent<Image> ().sprite = BananaVisual;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = banana_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = banana_eng;
			PreviousButton.interactable = false;
		}

		if (currentIndex == 1) {

			//ShowText ();
			textshowed.text = "leaves";
			thai_textshowed.text = "ใบไม้";
			VocabImage.GetComponent<Image> ().sprite = LeavesVisual;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = leaves_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = leaves_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 2) {

			textshowed.text = "like";
			thai_textshowed.text = "ชอบ";
			VocabImage.GetComponent<Image> ().sprite = LikeVisual;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = like_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = like_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 3) {

			textshowed.text = "bread";
			thai_textshowed.text = "ขนมปัง";
			VocabImage.GetComponent<Image> ().sprite = BreadVisual;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = bread_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = bread_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 4) {

			textshowed.text = "wish";
			thai_textshowed.text = "ประสงค์";
			VocabImage.GetComponent<Image> ().sprite = WishVisual;
			Eng_RegularAudio.GetComponent <AudioSource> ().clip = wish_eng;
			Eng_SlowAudio.GetComponent <AudioSource> ().clip = wish_eng;
			PreviousButton.interactable = true;

		}

		if (currentIndex == 5) {

			textshowed.text = "river";
			thai_textshowed.text = "แม่น้ำ";
			VocabImage.GetComponent<Image> ().sprite = RiverVisual;
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
