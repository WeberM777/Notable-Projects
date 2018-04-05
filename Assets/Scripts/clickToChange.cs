using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class clickToChange : MonoBehaviour {

	/*public static List<string> vocabTextArr;
	public int[] vocabReadRows;
	public string vocabTextFile = "vocab";
	private TextAsset vocabTextAsset;*/

	public Text textshowed = null;
	public Text thai_textshowed = null;
	public Image VocabImage;
	public AudioSource Eng_RegularAudio;
	public AudioSource Eng_SlowAudio;

	public Button PreviousButton;

	public AudioClip banana_eng;
	public Sprite BananaVisual;

	public AudioClip leaves_eng;
	public Sprite LeavesVisual;

	public AudioClip like_eng;
	public Sprite LikeVisual;

	public AudioClip bread_eng;
	public Sprite BreadVisual;

	/*void Start(){
		vocabTextAsset = (TextAsset)Resources.Load(vocabTextFile);
	}*/

	/*public void vocabReadTextFile(){
		vocabTextArr = vocabTextAsset.text.Split ('\n').ToList ();

		for (int i = 0; i < vocabReadRows.Length; i++) {
			if (vocabReadRows [0] < 0 || vocabReadRows.Length == 0) {
				textshowed.text = vocabTextAsset.text;
			} else {
				textshowed.text += vocabTextArr [vocabReadRows[i]] + "\n";
			}
		}
	}*/

	public void changeWord (string vocabWord)
	{
		//vocabReadTextFile ();
		
		textshowed.text = vocabWord;

		if (vocabWord == "Banana") {

			VocabImage.GetComponent<Image> ().sprite = BananaVisual;
			Eng_RegularAudio.GetComponent <AudioSource>().clip = banana_eng;
			Eng_SlowAudio.GetComponent <AudioSource>().clip = banana_eng;
			PreviousButton.interactable = false;
		}
			
		if (vocabWord == "Leaves") {

			VocabImage.GetComponent<Image> ().sprite = LeavesVisual;
			Eng_RegularAudio.GetComponent <AudioSource>().clip = leaves_eng;
			Eng_SlowAudio.GetComponent <AudioSource>().clip = leaves_eng;
			PreviousButton.interactable = true;

		}

		if (vocabWord == "Like") {

			VocabImage.GetComponent<Image> ().sprite = LikeVisual;
			Eng_RegularAudio.GetComponent <AudioSource>().clip = like_eng;
			Eng_SlowAudio.GetComponent <AudioSource>().clip = like_eng;
			PreviousButton.interactable = true;


		}

		if (vocabWord == "Bread") {

			VocabImage.GetComponent<Image> ().sprite = BreadVisual;
			Eng_RegularAudio.GetComponent <AudioSource>().clip = bread_eng;
			Eng_SlowAudio.GetComponent <AudioSource>().clip = bread_eng;
			PreviousButton.interactable = true;
	
		}
			

	}

	public void changeScenes(){
		
		SceneManager.LoadScene("Story - Level1");
		
	}


	public void changeWordThai(string thai_vocabWord)
	{
		thai_textshowed.text = thai_vocabWord;
	}

}
