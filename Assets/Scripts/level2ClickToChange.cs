using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class level2ClickToChange : MonoBehaviour {

	public int currentIndex; //keeps track of the index for words

	//randomizes the word
	public static int randomWord;
	//randomizes the correct answer button
	public static int randomButton;

	//list of english vocab words
	List <string> vocabwords = new List <string> (){"banana", "leaves", "like", "bread", "wish", "river",
	"dancing", "friend", "bright", "moon", "candle", "music"};

	//list of thai vocab words
	List <string> thaivocabwords = new List <string> (){"กล้วย", "ใบไม้", "ชอบ", "ขนมปัง", "ประสงค์", "แม่น้ำ",
		"การเต้นรำ", "เพื่อน", "สดใส", "ดวงจันทร์", "เทียน", "เพลง"};

	//list of button names
	List <string> buttonTextName = new List <string>() {"BTNText1.text", "BTNText2.text", "BTNText3.text", "BTNText4.text"};

	//list of symbol names
	List <string> buttonSyms = new List <string>() {"Symbol1", "Symbol2", "Symbol3", "Symbol4"};



	//english button options
	public Text BTNText1;
	public Text BTNText2;
	public Text BTNText3;
	public Text BTNText4;

	//english button audio source
	public AudioSource Eng_Button1Audio;
	public AudioSource Eng_Button2Audio;
	public AudioSource Eng_Button3Audio;
	public AudioSource Eng_Button4Audio;

	//symbol button image
	public Image Symbol1;
	public Image Symbol2;
	public Image Symbol3;
	public Image Symbol4;

	//symbol image
	public Sprite checkmark;
	public Sprite xmark;

	//thai text on screen
	public Text thai_textshowed = null;
	//vocab placeholder for images on screen
	public Image VocabImage;
	//english audio button
	public AudioSource Eng_RegularAudio;
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
	//dancing
	public AudioClip dancing_eng;
	public Sprite DancingVisual;
	//friend
	public AudioClip friend_eng;
	public Sprite FriendVisual;
	//bright
	public AudioClip bright_eng;
	public Sprite BrightVisual;
	//moon
	public AudioClip moon_eng;
	public Sprite MoonVisual;
	//candle
	public AudioClip candle_eng;
	public Sprite CandleVisual;
	//music
	public AudioClip music_eng;
	public Sprite MusicVisual;

	//next button push
	public void nextMouseClick(){
		//vocabTextAsset = (TextAsset)Resources.Load(vocabTextFile);
		//vocabTextArr = vocabTextAsset.text.Split ('\n').ToList ();
		Symbol1.enabled=false;
		Symbol2.enabled=false;
		Symbol3.enabled=false;
		Symbol4.enabled=false;

		if (currentIndex <= 12) {
			currentIndex++;	
		}
		//loads everything for that screen
		//allTheStuff ();
	}

	//previous button push
	public void previousMouseClick(){
		if (currentIndex <= 12) {
			currentIndex--;
		}
		//loads everthing for that screen
		//ShowText();
		//allTheStuff ();
	}


	////IMPLMENT GAME LOGIC
	////all the components for each word
	//public void allTheStuff(){

	//	//int to save the random button number
	//	int buttonNumber;

	//	randomWord = Random.Range(0,12);
	//	randomButton = Random.Range (0, 4);

	//	/* TO USE IN FIRST VOCAB SCENE!!!
	//	while (currentIndex >= 0) {
	//		BTN3Text.text = vocabwords[0];
			
	//	}*/

	//	//randomize answers
	//	BTNText1.text = vocabwords [randomWord];
	//	BTNText2.text = vocabwords [randomWord];
	//	BTNText3.text = vocabwords [randomWord];
	//	BTNText4.text = vocabwords [randomWord];

	//	//pull in images and audio for word
	//	while (currentIndex >= 0) {

	//		randomButton = buttonNumber;

	//		if (currentIndex == 0) {
	//			PreviousButton.interactable = false;
	//		} else {
	//			PreviousButton.interactable = true;
	//		}

	//		if (currentIndex == 12) {
	//			//changeScenes6 ();
	//		}

	//		//create text variable to hold correct word
	//		Text correctWord;
	//		Text correctButton;

	//		//add in next word from the list - will be the correct word ie sample: will put in "banana" 
	//		correctWord = vocabwords [currentIndex];


	//		//randomizes the button with correct word ie sample will put in "BTNText1.text"
	//		correctButton = buttonTextName[buttonNumber];


	//		//adds in the correct text inside the correct button *hopefully
	//		correctButton = correctWord;
	//		//places in correct thai word
	//		thai_textshowed.text = thaivocabwords[currentIndex];
	//		//places in correct visual
	//		VocabImage.GetComponent<Image> ().sprite = BananaVisual;

	//		//places in correct audio clip
	//		Eng_RegularAudio.GetComponent <AudioSource> ().clip = correctWord + "_eng";

	//		//if correct button change that symbol to checkmark
	//		if (correctButton) {

	//			if (buttonNumber == 0) {
	//				Symbol1.GetComponent<Image> ().sprite = checkmark;
	//			}
	//			if (buttonNumber == 1) {
	//				Symbol2.GetComponent<Image> ().sprite = checkmark;
	//			}
	//			if (buttonNumber == 2) {
	//				Symbol3.GetComponent<Image> ().sprite = checkmark;
	//			}
	//			if (buttonNumber == 3) {
	//				Symbol4.GetComponent<Image> ().sprite = checkmark;
	//			}

	//		}


	//	}


	//}

	/*TO DO: CHANGE TO NEXT SCENE 
	//change the scene to Color Activity (then that scene is programed to go to story
	public void changeScenes6() {

		FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene("Level1Round1Activity");
	}
	*/
		
}
