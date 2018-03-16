using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a phrase of the story
/// </summary>
public class Phrase : MonoBehaviour
{

    private string sPhrase;
    private List<AudioClip> audioClips;

    public Phrase(string p, List<AudioClip> ac)
    {
        sPhrase = p;
        audioClips = new List<AudioClip>();
    }

    public string SPhrase
    {
        get
        {
            return sPhrase;
        }
    }

    public List<AudioClip> AudioClips
    {
        get
        {
            return audioClips;
        }

        set
        {
            audioClips = value;
        }
    }

}
