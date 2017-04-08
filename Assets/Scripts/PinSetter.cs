using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text standingDisplay;
    public GameObject pinSet;

    public bool ballOutOfPlay = false;
    private int lastStandingCount = -1;
    private int lastSettledCount = 10;
    private float lastChangeTime;

    private Ball ball;
    private Animator animator;

    private readonly ActionMaster actionMaster = new ActionMaster(); //we need it here as we only want 1 instance
    //private bool pinsSettled;    

    // Use this for initialization
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        animator = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            updateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
        else
            standingDisplay.color = Color.green;

    }

    private void updateStandingCountAndSettle()
    {
        var currentStanding = CountStanding();

        if (lastStandingCount != currentStanding)
        {
            //   standingDisplay.text = CountStanding().ToString();
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; //how long to wait to consider pins settled

        if (Time.time - lastChangeTime >= settleTime)
            pinsHaveSettled();
    }

    private void pinsHaveSettled()
    {
        int standing = CountStanding();
        var pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        animateSwiper(actionMaster.Bowl(pinFall));
        Debug.Log("Pinfall: " + pinFall);

        ball.Reset();
        lastStandingCount = -1; //indicates pins have settled, and ball not back in box
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }

    private void animateSwiper(ActionMaster.Action action)
    {
        Debug.Log("Action: " + action);
        switch (action)
        {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.Reset:
                animator.SetTrigger("resetTrigger");
                lastSettledCount = 10;
                break;
            case ActionMaster.Action.EndTurn:
                animator.SetTrigger("resetTrigger");
                lastSettledCount = 10;
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("don't know how to end game yet");
        }
    }

    private int CountStanding()
    {
        var standingPins = 0;

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
                standingPins++;
        }

        return standingPins;
    }

    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
            pin.RaiseIfStanding();
    }

    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
            pin.Lower();
    }

    public void RenewPins()
    {
        Instantiate(pinSet, new Vector3(0, 15, 1829f), Quaternion.identity);
    }
}
