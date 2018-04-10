using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour {

	public Animator settingsAn;
	bool animateSet = false;

	//Will animate settings panel
	public void showSettingsPanel()
	{
		// add code for if a profile is not selected
		animateSet = !animateSet;
		settingsAn.SetBool("AnimateSettings", animateSet);
	}

    public void SaveAndQuit()
    {
        SceneManager.LoadScene("Menu");
    }
}
