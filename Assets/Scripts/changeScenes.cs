using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenes : MonoBehaviour {

	//change the scene to story scene
	public void changeStory1(){

		SceneManager.LoadScene("Story - Level1");
	}
}
