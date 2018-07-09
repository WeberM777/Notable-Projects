using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a phrase of the story
/// </summary>
public class Phrase
{

    public List<Sentence> sentences { get; set; }

    public Phrase(List<Sentence> s, List<AudioClip> ac)
    {
        sentences = s;
    }

    public Phrase()
    {
        sentences = new List<Sentence>();
    }

    public override string ToString()
    {
        string tmp = "";
        foreach (Sentence sentence in sentences)
        {
            tmp += sentence.sentEng + " ";
        }
        return tmp;
    }

}
