using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(2.5f);


        SceneLoader.Instance.LoadNextScene("Menu");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
