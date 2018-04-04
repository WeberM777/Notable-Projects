using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowButtonContoller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0))
		{
            if (!StoryManager.instance.locked)
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(point))
                {
                    StoryManager.instance.locked = true;
                    StoryManager.instance.PlaySlowAudio();
                    StoryManager.instance.locked = false;
                }
            }

		}

        if (Input.touchCount == 1)
        {
            if (!StoryManager.instance.locked)
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(point))
                {
                    StoryManager.instance.locked = true;
                    StoryManager.instance.PlaySlowAudio();
                    StoryManager.instance.locked = false;
                }
            }

        }

    }
}
