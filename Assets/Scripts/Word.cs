using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a single word
/// </summary>
[System.Serializable]
public class Word {

    public string word;
    public string thai = "";
    public AudioClip audioEng;
    public AudioClip audioThai;
    public AudioClip audioSlow;

    public Word(string w, AudioClip ac)
    {
        word = w;
        audioEng = ac;
    }
}
