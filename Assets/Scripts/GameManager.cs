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
    public GameObject Level1Lock;
    public GameObject Level2Lock;
    public GameObject Level3Lock;
    public GameObject Level4Lock;
    public GameObject Level1Button;
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;

    public int numScenesLevel1;
    public int numScenesLevel2;
    public int numScenesLevel3;
    public int numScenesLevel4;
    public int numintroScenes;


    private string userName;
    public int progress;
    public UIManager UIManager;

    public void StartGame()
    {
        if (progress == 0 || progress == 1 || progress == 2)
            progress = 3;
        SceneLoader.Instance.LoadNextScene(progress);
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

            if (progress >= SceneManager.sceneCountInBuildSettings)
            {
                SaveUserProgress(0);
            }

            loadUserProgress();

            Level1Button.GetComponent<Button>().interactable = false;
            Level2Button.GetComponent<Button>().interactable = false;
            Level3Button.GetComponent<Button>().interactable = false;
            Level4Button.GetComponent<Button>().interactable = false;
            Level1Lock.SetActive(true);
            Level2Lock.SetActive(true);
            Level3Lock.SetActive(true);
            Level4Lock.SetActive(true);

            if(progress > (numScenesLevel1 + numScenesLevel2 + numScenesLevel3 + numScenesLevel4 + numintroScenes))
            {
                Level1Button.GetComponent<Button>().interactable = true;
                Level2Button.GetComponent<Button>().interactable = true;
                Level3Button.GetComponent<Button>().interactable = true;
                Level4Button.GetComponent<Button>().interactable = true;
                Level1Lock.SetActive(false);
                Level2Lock.SetActive(false);
                Level3Lock.SetActive(false);
                Level4Lock.SetActive(false);
            }
            else if (progress > (numScenesLevel1 + numScenesLevel2 + numScenesLevel3 + numintroScenes))
            {
                Level4Lock.SetActive(false);
                Level4Button.GetComponent<Button>().interactable = true;
            }
            else if (progress > (numScenesLevel1 + numScenesLevel2 + numintroScenes))
            {
                Level3Lock.SetActive(false);
                Level3Button.GetComponent<Button>().interactable = true;
            }
            else if (progress > numScenesLevel1 + numintroScenes)
            {
                Level2Lock.SetActive(false);
                Level2Button.GetComponent<Button>().interactable = true;
            }
            else
            {
                Level1Lock.SetActive(false);
                Level1Button.GetComponent<Button>().interactable = true;
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
        if(progress >= SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt(userName, 3);
        }
        else
        {
            PlayerPrefs.SetInt(userName, progress+1);
        }
    }

    /// <summary>
    /// loads the users progress if exists
    /// </summary>
    private void loadUserProgress()
    {
        progress = PlayerPrefs.GetInt(userName);
    }

    /// <summary>
    /// Exit Application
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }


}
