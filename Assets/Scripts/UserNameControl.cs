using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameControl : MonoBehaviour {

    private GameManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
        GetComponent<Text>().text = manager.UserName;
	}
	
}
