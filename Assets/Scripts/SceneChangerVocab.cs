using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerVocab : MonoBehaviour {

	public GameObject RightButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void onRightClick()
	{
		SceneManager.LoadScene (sceneName:"Round1Vocab_Leaves");
	}
}
