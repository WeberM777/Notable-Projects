using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour {

	public Text userEntry;

	public void showUserName()
	{
		PlayerPrefs.SetString("Username", userEntry.text);
	}
}

