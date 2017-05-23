using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<int> rolls = new List<int>();
    private Ball ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;

    // Use this for initialization
    void Start()
    {
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    public void Bowl(int pinFall)
    {
        try
        {
            rolls.Add(pinFall);

            var actionToPerform = ActionMaster.NextAction(rolls);

            if (actionToPerform == ActionMaster.Action.EndGame)
            {
                endGame();
                return;
            }

            pinSetter.performAction(actionToPerform);
            ball.Reset();
        }
        catch (Exception)
        {
            Debug.LogWarning("Something went wrong in Bowl");
        }

        try
        {
            scoreDisplay.FillRolls(rolls);
            scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(rolls));
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Something went wrong in FillRolls");
        }
    }

    private void endGame()
    {
         
    }

    private void resetGame()
    {
        ball.Reset();       
    }
}
