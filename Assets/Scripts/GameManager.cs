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
        SceneManager.LoadScene("Round1Vocab_dancing");
	}

	public void StartIntermediateGame()
	{
        SceneManager.LoadScene("Round1Vocab_dancing"); // this is temporary, will be the intermediate vocabs
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
