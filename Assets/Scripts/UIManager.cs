using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public Animator difficultyPanel;
    public Animator profilePanel;

    bool animateDiff = false;
    bool animateProf = false;

    // bool profileSelected = false;

    /// <summary>
    /// Will animate and show the Difficulty panel
    /// </summary>
    public void showDifficultyPanel()
    {
            // add code for if a profile is not selected
            animateDiff = !animateDiff;
            difficultyPanel.SetBool("AnimatePanel", animateDiff);
    }

    public void showProfilePanel()
    {
        animateProf = !animateProf;
        profilePanel.SetBool("AnimatePanel", animateProf);
    }

}
