using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenControl : MonoBehaviour {

    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Use this for initialization
    void Start () {
        slider.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = SceneLoader.Instance.progress;
	}
}
