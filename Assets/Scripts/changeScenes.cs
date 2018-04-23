using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenes : MonoBehaviour {

	//change the scene to story scene
	public void changeStory1(){
        FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Story - Level1");
	}
}
