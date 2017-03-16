using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text standingDisplay;
    public Pin[] pins; 

    // Use this for initialization
    void Start()
    {
        pins = GameObject.FindObjectsOfType<Pin>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();
    }

    public int CountStanding()
    {
        var standingPins = 0;

        foreach (Pin pin in pins)
        {
            if (pin.IsStanding())
                standingPins++;
        }

        return standingPins;
    }
}
