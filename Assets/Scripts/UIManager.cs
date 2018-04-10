using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Animator difficultyPanel;
    //public Animator profilePanel;

    bool animateDiff = false;
    //bool animateProf = false;

	public GameObject krathongImage;
	//public GameObject SelectProfile;
	public GameObject PlayButton;
	public GameObject purpbck;
	public GameObject userName;
	public Text GameTitle;
	//public Text select;


    // bool profileSelected = false;

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


	/* We may use this later
    public void showProfilePanel()
    {
        animateProf = !animateProf;
        profilePanel.SetBool("AnimatePanel", animateProf);

		//hide other ui elements - from martha
		krathongImage.SetActive(false);
		SelectProfile.SetActive (false);
		PlayButton.SetActive (false);
		purpbck.SetActive (false);
		select.GetComponent<Text>().enabled = false;
		GameTitle.GetComponent<Text>().enabled = false;
    }*/

}
