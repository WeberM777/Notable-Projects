using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class clickToChange : MonoBehaviour {

	public Text textshowed = null;
	public Text thai_textshowed = null;
	public Sprite LeavesVisual;
	public Image VocabImage;
	public Button Eng_SpeakButton;
	public AudioClip leaves_english;


	public void changeWord (string vocabWord)
	{
		textshowed.text = vocabWord;

		if(vocabWord == "Leaves"){

			VocabImage.GetComponent<Image> ().sprite = LeavesVisual;
			Eng_SpeakButton.GetComponent<AudioSource> ().clip = leaves_english;

		}

	}

	public void changeWordThai(string thai_vocabWord)
	{
		thai_textshowed.text = thai_vocabWord;
	}



}
