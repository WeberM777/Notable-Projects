﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIColorPick : MonoBehaviour
{

    private bool firstRound = true;
    public TMPro.TextMeshProUGUI textMesh;
    public TMPro.TextMeshProUGUI textMeshCorrect;
    private int questNum = 0;
    private string color = "";
    public GameObject doneButton;
    private AudioSource audioSource;
    public GameObject soundButton;

    Dictionary<string, string> questions;
    List<string> colors;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        questions = new Dictionary<string, string>();
        questions.Add("red", "Touch the color RED?");
        questions.Add("blue", "Touch the color BLUE?");
        questions.Add("green", "Touch the color GREEN?");
        questions.Add("yellow", "Touch the color YELLOW?");

        colors = new List<string>();
        colors.Add("red");
        colors.Add("blue");
        colors.Add("green");
        colors.Add("yellow");

        System.Random rnd = new System.Random();
        colors = colors.OrderBy(x => rnd.Next()).ToList();
    }

    public void RunGreenAnim()
    {
        if(firstRound)
        {
            loadColorQuestions();
            firstRound = false;
        }
        else if(color == "green")
        {
            textMeshCorrect.text = "";
            loadColorQuestions();
        }
        else
        {
            textMeshCorrect.text = "Try Again";
        }
    }
    public void RunBlueAnim()
    {
        if (firstRound)
        {
            loadColorQuestions();
            firstRound = false;
        }

        else if (color == "blue")
        {
            textMeshCorrect.text = "";
            loadColorQuestions();
        }
        else
        {
            textMeshCorrect.text = "Try Again";
        }
    }
    public void RunRedAnim()
    {
        if (firstRound)
        {
            loadColorQuestions();
            firstRound = false;
        }

        else if (color == "red")
        {
            textMeshCorrect.text = "";
            loadColorQuestions();
        }
        else
        {
            textMeshCorrect.text = "Try Again";
        }
    }
    public void RunYellowAnim()
    {
        if (firstRound)
        {
            loadColorQuestions();
            firstRound = false;
        }

        else if (color == "yellow")
        {
            textMeshCorrect.text = "";
            loadColorQuestions();
        }
        else
        {
            textMeshCorrect.text = "Try Again";
        }
    }
    public void LoadStoryScene()
    {
        StartCoroutine(LoadStorySceneDelay(0));
    }
    private IEnumerator LoadStorySceneDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
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

    private void loadColorQuestions()
    {
        if(questNum < 4)
        {
            textMesh.text = questions[colors[questNum]];
            color = colors[questNum];
            questNum++;
        }
        else
        {
            loadFriendActivity();
        }
        soundButton.SetActive(false);
        audioSource.Stop();
    }

    private void loadFriendActivity()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        doneButton.SetActive(true);
        textMesh.text = "Ask a friend, \"What color do you like?\"";
    }

    public void PlaySentence(string tmp)
    {
        audioSource.Play();
    }
}