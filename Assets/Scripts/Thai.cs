using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thai {

    public static Dictionary<string, string> WORDS;

    static Thai()
    {
        WORDS = new Dictionary<string, string>();
        WORDS.Add("is", "คือ");
        WORDS.Add("glad", "ดีใจ");
        WORDS.Add("it", "มัน");
        WORDS.Add("krathong", "กระทง");
        WORDS.Add("day", "วัน");
        WORDS.Add("rudee", "ฤดี");
        WORDS.Add("made", "ทำ");
        WORDS.Add("a", "ใด");
    }

}
