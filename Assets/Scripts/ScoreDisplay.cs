using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] RollTexts, FrameTexts;

    //populates individual roll score on score display
    public void FillRolls(List<int> rolls)
    {
        var scoreString = FormatRolls(rolls);

        for (var i = 0; i < rolls.Count; i++)
        {
            if (rolls[i] == 10)
            {
                RollTexts[i].text = scoreString[i].ToString() + scoreString[i + 1];
                i++;
            }                           
            else
            {
                RollTexts[i].text = scoreString[i].ToString();
            }
        }

        //for (int i = 0; i < scoreString.Length; i++)
        //{
        //    if()

        //    RollTexts[i].text = scoreString[i].ToString();
        //}
    }

    //populates cumulative frame score on display
    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            FrameTexts[i].text = frames[i].ToString();
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        var output = string.Empty;

        foreach (int roll in rolls)
        {
            output += roll.ToString();
        }

        return output;        
    }
}

