using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeScript : MonoBehaviour {

	public float delay = 2.1f;
	public string fullText;
	public string currentText = "";
	public int vCount;

	// Use this for initialization
	void Start () {
		StartCoroutine(ShowText());
	}

	public IEnumerator ShowText(){
		vCount++;
		if (vCount <= 5) {

			if (vCount == 0) {
				fullText = "Banana";
			}
			if (vCount == 1) {
				fullText = "Leaves";
			}
			if (vCount == 2) {
				fullText = "Like";
			}
			if (vCount == 3) {
				fullText = "Bread";
			}
			if (vCount == 4) {
				fullText = "Wish";
			}
			if (vCount == 5) {
				fullText = "River";
			}
				
		}

		for(int i = 0; i < fullText.Length; i++){
			currentText = fullText.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}
		
}