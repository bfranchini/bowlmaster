using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{    
    public GameObject pinSet;    
    private Animator animator;
    private PinCounter pinCounter;

    // Use this for initialization
    void Start()
    {
        animator = FindObjectOfType<Animator>();
        pinCounter = FindObjectOfType<PinCounter>();
    }

    public void performAction(ActionMaster.Action action)
    {
        Debug.Log("Action: " + action);

        switch (action)
        {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.Reset:
            case ActionMaster.Action.EndTurn:
            case ActionMaster.Action.EndGame:
                animator.SetTrigger("resetTrigger");
                pinCounter.Reset();
                break;
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
        Instantiate(pinSet, new Vector3(0, 15, 1829f), Quaternion.identity);
    }
}
