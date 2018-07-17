using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAction : MonoBehaviour {

	public void goBack()
	{
		SceneLoader.Instance.LoadNextScene("ColoringActivity");
	}
}
