using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TypeScript : MonoBehaviour {

	public float delay = .5f;
	public string fullText = "";
	public string currentText = "";

	public int vCount;

    //list of english vocab words
    List<string> vocabwords = new List<string>(){"banana ", "leaves ", "like ", "bread ", "wish ", "river ",
    "dancing ", "friend ", "bright ", "moon ", "candle ", "music "};

	// Use this for initialization
	void Start () {
		StartCoroutine(ShowText());
	}

    //next button push
    public void nextMouseClick()
    {
        if (vCount <= 12)
        {
            vCount++;
        }
        StartCoroutine(ShowText());
    }

    //next button push
    public void previousMouseClick()
    {
        if (vCount <= 12)
        {
            vCount--;
        }
        StartCoroutine(ShowText());
    }

	public IEnumerator ShowText(){
        fullText = vocabwords[vCount];

		for(int i = 0; i < fullText.Length; i++){
			currentText = fullText.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}
		
}