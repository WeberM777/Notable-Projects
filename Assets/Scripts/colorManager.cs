using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class colorManager : MonoBehaviour {

	//keeps track of current color
	public int currentColor; 

	//Text of color button
	public GameObject endText;

	//button itself
	public GameObject colorButton;

	//button text
	public Text ColorButtonText;

	//create gameobjects 
	public GameObject RedFrame;
	public GameObject YellowFrame;
	public GameObject BlueFrame;
	public GameObject GreenFrame;

	// Use this for initialization
	void Start () {
		//set up colors to hide
		RedFrame.gameObject.SetActive (false);
		YellowFrame.gameObject.SetActive (false);
		BlueFrame.gameObject.SetActive (false);
		GreenFrame.gameObject.SetActive (false);
		endText.gameObject.SetActive (false);
	}

	//On color Button Push
	public void ColorMouseClick(){

		if (currentColor <= 4) {
			currentColor++;
		}
		loadColor ();
	}

	public void loadColor(){

		if (currentColor == 1) {
			RedFrame.gameObject.SetActive (true); 
			ColorButtonText.text = "Next Color | สีถัดไป";
		}
		if (currentColor == 2) {
			YellowFrame.gameObject.SetActive (true); 
		}
		if (currentColor == 3) {
			BlueFrame.gameObject.SetActive (true); 
		}
		if (currentColor == 4) {
			GreenFrame.gameObject.SetActive (true); 
			colorButton.gameObject.SetActive (false);
			endText.gameObject.SetActive (true);
            GameObject.Find("next2").GetComponent<Button>().interactable = true;
		}

	}


}
