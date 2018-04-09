using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour {

	public Animator settingsAnimator;
	bool animateSettings = false;

	//Will animate settings panel
	public void showSettingsPanel()
	{
		// add code for if a profile is not selected
		animateSettings = !animateSettings;
		settingsAnimator.SetBool("AnimatePanel", animateSettings);

	}
}