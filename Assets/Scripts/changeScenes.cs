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
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneLoader.Instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneLoader.Instance.LoadNextScene("Menu");
        }
    }

}
