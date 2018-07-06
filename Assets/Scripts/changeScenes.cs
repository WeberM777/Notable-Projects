using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenes : MonoBehaviour {

	//change the scene to story scene
	public void changeStory1(){

        if (GameObject.FindObjectOfType<GameManager>().progress <= SceneManager.GetActiveScene().buildIndex)
        {
            GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.GetActiveScene().buildIndex);
        }
        if (GameObject.FindObjectOfType<GameManager>().progress < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}
