using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorActivityManager : MonoBehaviour {

    public void StartGirl()
    {
        SceneLoader.Instance.LoadNextScene("Girl");
    }

    public void StartBoy()
    {
        SceneLoader.Instance.LoadNextScene("Boy");
    }

    public void EndGame()
    {
        GameObject.FindObjectOfType<GameManager>().SaveUserProgress(SceneManager.sceneCountInBuildSettings+1);
        SceneLoader.Instance.LoadNextScene("Menu");
    }

    public void StartFlower()
    {
        SceneLoader.Instance.LoadNextScene("Flower");
    }
}
