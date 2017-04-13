using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster
{
    //returns list of cumulative scores like a normal score card
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        var cumulativeScores = new List<int>();

        int runningTotal = 0;

        foreach (var frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }

    //returns list of individual frame scores. Not cumulative
    public static List<int> ScoreFrames(List<int> rolls)
    {
        var frameList = new List<int>();

        //var currentRoll = 0;
        //var previousroll = 0;
        var frameRoll1 = false;
        var frameRoll2 = false;
        var currentFrameScore = 0;

        foreach (var roll in rolls)
        {
            if (!frameRoll1)
            {
                currentFrameScore += roll;
                frameRoll1 = true;
            }                               
            else if (!frameRoll2)
            {
                currentFrameScore += roll;
                frameRoll2 = true;
            }

            if (frameRoll1 && frameRoll2)
            {
                frameList.Add(currentFrameScore);
                frameRoll1 = frameRoll2 = false;
                currentFrameScore = 0;
            }              
        }

        //for (int i = 0; i < rolls.Count; i++)
        //{
        //    if (i + 1 == rolls.Count)
        //        return frameList;

        //    if(rolls[i] + rolls[i +1 ] != 10)
        //        frameList.Add(rolls[i] + rolls[i + 1]);
        //}       

        return frameList;
    }
}
