using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a single word
/// </summary>
public class Word {

    public string sWord;
    public AudioClip audioClip;
    public bool eng;

    public Word(string w, AudioClip ac, bool e)
    {
        sWord = w;
        audioClip = ac;
        eng = e;
    }
}
