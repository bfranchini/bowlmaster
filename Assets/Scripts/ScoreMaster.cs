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

        //only go up to the second to last roll since going to the last one would 
        //result in an index out of range operation
        for (int i = 0; i < rolls.Count - 1; i++)
        {            
            var currentRoll = rolls[i];
            var nextRoll = rolls[i + 1];
            int bonusRoll;

            //strike
            if (currentRoll == 10 || nextRoll == 10)
            {
                //there aren't enough rolls to score this strike frame
                if (i + 2 == rolls.Count)
                    return frameList;

                bonusRoll = rolls[i + 2];

                frameList.Add(currentRoll + nextRoll + bonusRoll);
                continue;                
            }

            //spare
            if (currentRoll + nextRoll == 10)
            {
                //there arent' enough rolls to score this spare frame
                if (i + 2 == rolls.Count)
                    return frameList;

                bonusRoll = rolls[i + 2];

                frameList.Add(currentRoll + nextRoll + bonusRoll);
                i ++;
                continue;
            }

            //regular frame
            if (currentRoll != 10 && nextRoll != 10 && currentRoll + nextRoll != 10 && frameList.Count < 10)
            {
                frameList.Add(currentRoll + nextRoll);
                i++;                
            }
        }

        return frameList;
    }
}
