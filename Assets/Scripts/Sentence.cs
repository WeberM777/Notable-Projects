using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a sentence in the story
/// </summary>
[Serializable]
public class Sentence {
    public string sentEng;
    public string sentThai;
    public AudioClip AudioEng { get; set; }
    public AudioClip AudioThai { get; set; }
}
