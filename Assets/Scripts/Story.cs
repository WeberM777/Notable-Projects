using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the entire story
/// </summary>
[Serializable]
public class Story {

    public const int PHRASE_MAX = 75;

    public List<Sentence> Sentences { get; set; }
    public List<Phrase> Phrases { get; set; }

    public Story()
    {
        Sentences = new List<Sentence>();
        Phrases = new List<Phrase>();
    }
    public Story(List<Sentence> sent)
    {
        Sentences = sent;
        Phrases = new List<Phrase>();
    }

    /// <summary>
    /// Automatically sets the phrases for story mode
    /// </summary>
    public void setPhrases() // Once there are recordings for all sentences, make a sentence class and use StoryPhrase Class
    {

        int tmp = 0;
        int numSent = 0;
        Phrase phrase = new Phrase();

        numSent++;

        // updates tmp to the next line in the story
        for (int i = 0; i < Sentences.Count; i++)
        {

            if(tmp + Sentences[i].sentEng.Length < PHRASE_MAX && Sentences[i].sentEng != "" && !Sentences[i].sentEng.Contains("?"))
            {
                phrase.sentences.Add(Sentences[i]);
                tmp += Sentences[i].sentEng.Length;
            }
            else
            {
                Phrases.Add(phrase);
                numSent = 0;
                tmp = Sentences[i].sentEng.Length;
                phrase = new Phrase();
                phrase.sentences.Add(Sentences[i]);
                if(Sentences[i].sentEng.Contains("?"))
                {
                    tmp = PHRASE_MAX + 1;
                }
            }

        }
        Phrases.Add(phrase);

    }
}
