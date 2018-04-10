using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class TutorialScript : MonoBehaviour {

	//keeps track of current tutorial bubble
	public int currentTut; 

	//text in bubbles
	public GameObject Text1;
	public GameObject Text2;
	public GameObject Text3;
	public GameObject Text4;
	public GameObject Text5;
	public GameObject Text6;
	public GameObject Text7;
	public GameObject Text8;

	//image bubbles
	public GameObject BubbleOne;
	public GameObject BubbleTwo;
	public GameObject BubbleThree;
	public GameObject BubbleFour;
	public GameObject BubbleFive;
	public GameObject BubbleSix;
	public GameObject BubbleSeven;
	public GameObject BubbleEight;

	//bubble button itself
	public GameObject TutButton;
	public Text TutText;

	//Tut panel
	public GameObject TutPanel;

	//WelcomeBck Title
	public GameObject WelcomeBck;
	public GameObject WelcomeText;

	//background music
	public GameObject thaiBackgroundMusic;

	// Use this for initialization
	void Start () {
		//set all tutorial texts to hide
		Text1.gameObject.SetActive (false); 
		Text2.gameObject.SetActive (false);
		Text3.gameObject.SetActive (false);
		Text4.gameObject.SetActive (false);
		Text5.gameObject.SetActive (false);
		Text6.gameObject.SetActive (false);
		Text7.gameObject.SetActive (false);
		Text8.gameObject.SetActive (false);

		//set all tutorial bubbles to hide
		BubbleOne.gameObject.SetActive (false);
		BubbleTwo.gameObject.SetActive (false);
		BubbleThree.gameObject.SetActive (false);
		BubbleFour.gameObject.SetActive (false);
		BubbleFive.gameObject.SetActive (false);
		BubbleSix.gameObject.SetActive (false);
		BubbleSeven.gameObject.SetActive (false);
		BubbleEight.gameObject.SetActive (false);


	}


	//On Ok Button Push
	public void OkMouseClick(){

		if (currentTut <= 8) {
			currentTut++;
		}
		loadTheBubble ();
	}

	public void loadTheBubble(){
		if (currentTut == 0) {
			
			Text1.gameObject.SetActive (true); 
			BubbleOne.gameObject.SetActive (true);

		}

		if (currentTut == 1) {

			Text1.gameObject.SetActive (false); 
			BubbleOne.gameObject.SetActive (false);
			Text2.gameObject.SetActive (true); 
			BubbleTwo.gameObject.SetActive (true);

		}

		if (currentTut == 2) {
			Text2.gameObject.SetActive (false); 
			BubbleTwo.gameObject.SetActive (false);
			Text3.gameObject.SetActive (true); 
			BubbleThree.gameObject.SetActive (true);

		}

		if (currentTut == 3) {

			Text3.gameObject.SetActive (false); 
			BubbleThree.gameObject.SetActive (false);
			Text4.gameObject.SetActive (true); 
			BubbleFour.gameObject.SetActive (true);

		}

		if (currentTut == 4) {

			Text4.gameObject.SetActive (false); 
			BubbleFour.gameObject.SetActive (false);
			Text5.gameObject.SetActive (true); 
			BubbleFive.gameObject.SetActive (true);

		}

		if (currentTut == 5) {
			
			Text5.gameObject.SetActive (false); 
			BubbleFive.gameObject.SetActive (false);
			Text6.gameObject.SetActive (true); 
			BubbleSix.gameObject.SetActive (true);

		}

		if (currentTut == 6) {

			Text6.gameObject.SetActive (false); 
			BubbleSix.gameObject.SetActive (false);
			Text7.gameObject.SetActive (true); 
			BubbleSeven.gameObject.SetActive (true);

		}

		if (currentTut == 7) {

			Text7.gameObject.SetActive (false); 
			BubbleSeven.gameObject.SetActive (false);
			Text8.gameObject.SetActive (true); 
			BubbleEight.gameObject.SetActive (true);
			TutText.text = "Finish Tutorial | จบการสอน";

		}

		if (currentTut == 8) {
			Text8.gameObject.SetActive (false); 
			BubbleEight.gameObject.SetActive (false);
			TutButton.gameObject.SetActive (false);
			TutPanel.gameObject.SetActive (false);
			WelcomeBck.gameObject.SetActive (false);
			WelcomeText.gameObject.SetActive (false);
			thaiBackgroundMusic.gameObject.SetActive (true);
		}

	}

}
