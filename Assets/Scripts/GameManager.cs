using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// difficulty consts
	const int BEGINNER = 0;
	const int INTERMEDIATE = 1;

	public void StartBeginnerGame()
	{
        SceneManager.LoadScene("Level1Round1");
	}

	public void StartIntermediateGame()
	{
        SceneManager.LoadScene("Level1Round1"); // this is temporary, will be the intermediate vocabs
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
