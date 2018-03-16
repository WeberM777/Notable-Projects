using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a sentence in the story
/// </summary>
public class Sentence : MonoBehaviour {

    private string sentence;
    private AudioClip audioClip;

    public Sentence(string s, AudioClip ac)
    {
        sentence = s;
        audioClip = ac;
    }
}
