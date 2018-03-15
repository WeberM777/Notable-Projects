using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the entire story
/// </summary>
public class Story {

    public const int PHRASE_MAX = 75;

    private List<string> sentences;
    private List<string> phrases;

    public Story()
    {
        sentences = new List<string>();
        phrases = new List<string>();
    }

    public List<string> Sentences {
        get
        {
            return sentences;
        }
        set
        {
            sentences = value;
        }
    }

    public List<string> Phrases
    {
        get
        {
            return phrases;
        }
        set
        {
            phrases = value;
        }
    }

    /// <summary>
    /// Automatically sets the phrases for story mode
    /// </summary>
    public void setPhrases()
    {

        string tmp = "";
        int numSent = 0;

        tmp = sentences[0];
        numSent++;

        // updates tmp to the next line in the story
        for (int i = 1; i < sentences.Count; i++)
        {

            if(tmp.Length + sentences[i].Length < PHRASE_MAX && sentences[i] != "")
            {
                tmp += " " + sentences[i];

            }
            else
            {
                phrases.Add(tmp.Trim());
                numSent = 0;
                tmp = sentences[i];
            }

        }
        phrases.Add(tmp.Trim());

    }
}
