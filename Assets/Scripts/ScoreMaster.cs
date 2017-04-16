using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster
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
            var strike = currentRoll == 10 || nextRoll == 10;
            var spare = currentRoll + nextRoll == 10;

            if (strike || spare)
            {
                if (!calculatedBonus(i, rolls, frameList, currentRoll, nextRoll))
                    return frameList;

                //only skip a roll if we got a spare
                if (!strike)
                    i++;

                continue;
            }

            //regular frame
            if (frameList.Count < 10)
            {
                frameList.Add(currentRoll + nextRoll);
                i++;
            }
        }

        return frameList;
    }

    private static bool calculatedBonus(int i, List<int> rolls, List<int> frameList, int currentRoll, int nextRoll)
    {
        //there aren't enough rolls to score frame with bonus
        if (i + 2 == rolls.Count)
            return false;

        var bonusRoll = rolls[i + 2];

        frameList.Add(currentRoll + nextRoll + bonusRoll);
        return true;
    }
}
