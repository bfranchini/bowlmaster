using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
    public Text standingDisplay;
    public bool ballOutOfPlay = false;
    private int lastSettledCount = 10;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private GameManager gameManager;
    
    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            updateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
        else
            standingDisplay.color = Color.green;
    }

    public void Reset()
    {
        lastSettledCount = 10;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
            ballOutOfPlay = true;
    }

    private void updateStandingCountAndSettle()
    {
        var currentStanding = CountStanding();

        if (lastStandingCount != currentStanding)
        {
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
        gameManager.Bowl(pinFall);
        lastStandingCount = -1; //indicates pins have settled, and ball not back in box
        ballOutOfPlay = false;
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
}
