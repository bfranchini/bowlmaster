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
    private Ball ball;

    // Use this for initialization
    void Start()
    {
        ball = FindObjectOfType<Ball>();
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
            var curentStanding = CountStanding();

            if (lastStandingCount != curentStanding)
            {
                standingDisplay.text = CountStanding().ToString();
                lastStandingCount = curentStanding;
                lastChangeTime = Time.time;
            }

            float settleTime = 3f;

            if (Time.time - lastChangeTime >= settleTime)
            {
                pinsHaveSettled();
            }
        }
    }

    private void pinsHaveSettled()
    {
        ball.Reset();
        lastStandingCount = -1; //indicates pins have settle, and ball not back in box
        ballEnteredBox = false;
        pinsSettled = true;
        standingDisplay.color = Color.green;
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
