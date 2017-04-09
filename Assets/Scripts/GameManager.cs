using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{        
    private List<int>bowls = new List<int>();
    private Ball ball;
    private PinSetter pinSetter;

    // Use this for initialization
    void Start()
    {
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
    }

    public void Bowl(int pinFall)
    {
        bowls.Add(pinFall);
        var nextAction = ActionMaster.NextAction(bowls);
        pinSetter.performAction(nextAction);
        ball.Reset();
    } 
}
