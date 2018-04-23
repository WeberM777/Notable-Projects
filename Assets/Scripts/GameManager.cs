using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// difficulty consts
	const int BEGINNER = 0;
	const int INTERMEDIATE = 1;


    private string userName;
    public int progress;
    public UIManager UIManager;

    public string[] scenes;

    public void StartBeginnerGame()
	{
        if (scenes[progress] == "Round1MidActivity")
            progress++;
        SceneManager.LoadScene(scenes[progress]);
	}

	public void StartIntermediateGame()
	{
        SceneManager.LoadScene("Level1Round1"); // this is temporary, will be the intermediate vocabs
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
        loadUserProgress();
        UIManager.showDifficultyPanel();
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
