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
    private GameObject promptsParent;
    private Transform replayPrompt;

    // Use this for initialization
    void Start()
    {
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();

        promptsParent = GameObject.Find("Prompts");

        if (!promptsParent)
        {
            Debug.LogError("Could not find prompts parent object!");
            return;
        }

        replayPrompt = promptsParent.transform.Find("ReplayPrompt");

        if (!replayPrompt)
            Debug.LogError("Could not find a replay prompt!");
        
        /////////////Test code///////////////
        //var testScores = new List<int> { 10, 10, 10, 10, 9, 0, 10, 10, 10, 10, 10, 9, 1 };

        //foreach (var testScore in testScores)
        //{
        //    Bowl(testScore);
        //}
        /////////////Test code///////////////
    }

    public void Bowl(int pinFall)
    {
        var endGame = false;

        try
        {
            rolls.Add(pinFall);

            var actionToPerform = ActionMaster.NextAction(rolls);

            if (actionToPerform == ActionMaster.Action.EndGame)
                endGame = true;

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

            if (endGame)                            
                EndGame();                         
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Something went wrong in FillRolls: " + ex.Message);
        }
    }

    public void EndGame()
    {
        replayPrompt.gameObject.SetActive(true);

        var finalScoreText = GameObject.Find("FinalScore");

        if (!finalScoreText)
            Debug.LogError("Could not find Final Score Text");

        finalScoreText.GetComponent<Text>().text = scoreDisplay.GetFinalScore();
    }

    public void ResetGame()
    {
        replayPrompt.gameObject.SetActive(false);
        rolls = new List<int>();
        scoreDisplay.ResetScoreDisplay();
        pinSetter.performAction(ActionMaster.Action.Reset);
        ball.Reset();
    }
}
