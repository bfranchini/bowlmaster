using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{        
    private List<int>rolls = new List<int>();
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
            pinSetter.performAction(ActionMaster.NextAction(rolls));
            
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
        catch (Exception)
        {
            Debug.LogWarning("Something went wrong in FillRolls");            
        }
    } 
}
