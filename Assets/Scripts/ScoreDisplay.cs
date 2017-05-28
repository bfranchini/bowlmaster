using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public Text[] RollTexts, FrameTexts;

    //populates individual roll score on score display
    public void FillRolls(List<int> rolls)
    {
        var scoreString = FormatRolls(rolls);

        for (var i = 0; i < scoreString.Length; i++)
            RollTexts[i].text = scoreString[i].ToString();
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

        for (var i = 0; i < rolls.Count; i++)
        {
            //score box 1 to 21
            var box = output.Length + 1;

            //always enter 0 as -
            if (rolls[i] == 0)
                output += "-";
            //spare anywhere or on frame 21
            else if ((box % 2 == 0 || box == 21) && rolls[i - 1] + rolls[i] == 10 &&
                output[output.Length - 1].ToString() != "/")
                output += "/";

            //strike in frame 10
            else if (box >= 19 && rolls[i] == 10)
                output += "X";
            //strike
            else if (rolls[i] == 10)
                output += "X ";
            //normal 1-9 bowl
            else
                output += rolls[i].ToString();
        }

        return output;
    }

    public string GetFinalScore()
    {
        var score = FrameTexts.LastOrDefault(s => !string.IsNullOrEmpty(s.text));

        return score == null ? "0" : score.text;
    }

    public void ResetScoreDisplay()
    {
        foreach (var text in  FrameTexts)        
            text.text = "";
        
        foreach (var text in RollTexts)
            text.text = "";
    }
}

