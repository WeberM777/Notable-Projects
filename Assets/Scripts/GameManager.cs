using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// difficulty consts
	const int BEGINNER = 0;
	const int INTERMEDIATE = 1;

    // Level Buttons
    public GameObject Level2Lock;
    public GameObject Level3Lock;
    public GameObject Level4Lock;
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;


    private string userName;
    public int progress;
    public UIManager UIManager;

    public string[] scenes;

    public void StartGame()
    {
        if (scenes[progress] == "Round1MidActivity")
            progress++;
        SceneManager.LoadScene(scenes[progress]);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        // allows game manager to remain active through entire application
        DontDestroyOnLoad(this);
    }
    
    public void StartApplication()
    {
        if (!userName.Equals(null))
        {
            loadUserProgress();

            if (progress > 15)
            {
                Level2Lock.SetActive(false);
                Level2Button.GetComponent<Button>().interactable = true;
                Level3Lock.SetActive(false);
                Level3Button.GetComponent<Button>().interactable = true;
                Level4Lock.SetActive(false);
                Level4Button.GetComponent<Button>().interactable = true;
            }
            else if (progress > 10)
            {
                Level2Lock.SetActive(false);
                Level2Button.GetComponent<Button>().interactable = true;
                Level3Lock.SetActive(false);
                Level3Button.GetComponent<Button>().interactable = true;
            }
            else if (progress > 5)
            {
                Level2Lock.SetActive(false);
                Level2Button.GetComponent<Button>().interactable = true;
            }
            else
            {
                Level2Lock.SetActive(true);
                Level2Button.GetComponent<Button>().interactable = false;
                Level3Lock.SetActive(true);
                Level4Lock.SetActive(true);
                Level3Button.GetComponent<Button>().interactable = false;
                Level4Button.GetComponent<Button>().interactable = false;
            }

            UIManager.showDifficultyPanel();
        }

    }

    public string UserName
    {
        get
        {
            return userName;
        }
    }

    public void setUserName(string userName)
    {
        this.userName = userName;
    }

    /// <summary>
    /// Saves the users progress for continuation at a later time
    /// </summary>
    public void SaveUserProgress(int progress)
    {
        if(progress >= scenes.Length)
        {
            PlayerPrefs.SetInt(userName, 0);
        }
        else
        {
                PlayerPrefs.SetInt(userName, progress);
        }
    }


    /// <summary>
    /// loads the users progress if exists
    /// </summary>
    private void loadUserProgress()
    {
        progress = PlayerPrefs.GetInt(userName);
    }
}
