using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text standingDisplay;
    public Pin[] pins;
    public int lastStandingCount = -1;
    public GameObject pinSet;
    private bool ballEnteredBox;
    private float lastChangeTime;
    private bool pinsSettled;
    private int lastSettledCount = 10;
    private Ball ball;
    private readonly ActionMaster actionMaster = new ActionMaster();
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        animator = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballEnteredBox)
            updateStandingCountAndSettle();
    }

    private void updateStandingCountAndSettle()
    {
        //check the last standing count
        //call pinsHaveSettled     
        if (!pinsSettled)
        {
            var currentStanding = CountStanding();

            if (lastStandingCount != currentStanding)
            {
                standingDisplay.text = CountStanding().ToString();
                lastStandingCount = currentStanding;
                lastChangeTime = Time.time;
            }

            float settleTime = 3f;           

            if (Time.time - lastChangeTime >= settleTime)
                pinsHaveSettled();
        }
    }

    private void pinsHaveSettled()
    {        
        int standing = CountStanding();
        var pinFall = lastSettledCount - standing;
        lastSettledCount = CountStanding();
        animateSwiper(actionMaster.Bowl(pinFall)); 
        Debug.Log("Pinfall: " + pinFall);      

        ball.Reset();
        lastStandingCount = -1; //indicates pins have settled, and ball not back in box
        ballEnteredBox = false;
        pinsSettled = true;
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

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Ball>() != null)
        {
            standingDisplay.color = Color.red;
            ballEnteredBox = true;
            pinsSettled = false;
        }
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
        Instantiate(pinSet, new Vector3(0, 20, 1829f), Quaternion.identity);
    }
}
