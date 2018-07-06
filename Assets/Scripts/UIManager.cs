using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Animator difficultyPanel;
    //public Animator profilePanel;

    bool animateDiff = false;

	public GameObject krathongImage;
	public GameObject PlayButton;
	public GameObject purpbck;
	public GameObject userName;
	public Text GameTitle;

    /// <summary>
    /// Will animate and show the Difficulty panel
    /// </summary>
    public void showDifficultyPanel()
    {
    	// add code for if a profile is not selected
        animateDiff = !animateDiff;
        difficultyPanel.SetBool("AnimatePanel", animateDiff);

		//hide other ui elements - from martha
		krathongImage.SetActive(false);
		//SelectProfile.SetActive (false);
		PlayButton.SetActive (false);
		purpbck.SetActive (false);
		userName.SetActive (false);
		//select.GetComponent<Text>().enabled = false;
		GameTitle.GetComponent<Text>().enabled = false;

    }

}
