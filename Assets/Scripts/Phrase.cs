using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents and controls a phrase of the story, along with all features and animations associated with the text
/// </summary>
public class Phrase : MonoBehaviour, IPointerClickHandler
{

    private TextMeshProUGUI TMP; // TextMesh gameObject in the UI
    bool locked = false;


    private void Awake()
    {
        TMP = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Word Touch Event
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!locked)
        {
            locked = true;
            int index = TMP_TextUtilities.FindIntersectingWord(TMP, eventData.position, eventData.enterEventCamera); // gets the words index in the sentence.
            if (index != -1)
            {
                TMP_WordInfo wordInfo = TMP.textInfo.wordInfo[index]; // gets the wordInfo of the given index
                StoryManager.instance.PlayWord(wordInfo.GetWord()); // plays audiofile of word
                Debug.Log("Word [" + wordInfo.GetWord() + "]");

                // Change word color
                string text;
                string tmp = TMP.text;
                text = TMP.text.Substring(0, wordInfo.firstCharacterIndex) + "<color=\"yellow\">" + wordInfo.GetWord() + "</color>" + TMP.text.Substring(wordInfo.lastCharacterIndex + 1);

                TMP.text = text;

                // reset word color
                StartCoroutine(rollbackHighlighting(tmp, wordInfo.GetWord()));

            }
        }
    }

    public void SetPhrase(string phrase)
    {
        TMP.text = phrase;
    }

    public string GetPhrase()
    {
        return TMP.text;
    }

    /// <summary>
    /// Resets word color after a certain time based on word length
    /// </summary>
    /// <param name="text">wanted text</param>
    /// <param name="word">word that is being reset</param>
    /// <returns></returns>
    private IEnumerator rollbackHighlighting(string text, string word)
    {
        if (word.Length < 2)
        {
            yield return new WaitForSeconds(.5f * word.Length);
        }
        else if (word.Length < 4)
        {
            yield return new WaitForSeconds(.3f * word.Length);
        }
        else if (word.Length < 8)
        {
            yield return new WaitForSeconds(.2f * word.Length);
        }
        else
        {
            yield return new WaitForSeconds(.1f * word.Length);
        }
        TMP.text = text;
        locked = false;
    }

}
