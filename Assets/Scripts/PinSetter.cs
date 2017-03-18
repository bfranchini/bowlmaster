using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text standingDisplay;
    public Pin[] pins;
    private bool ballEnteredBox = false;

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

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Ball>() != null)
        {
            standingDisplay.color = Color.red;
            ballEnteredBox = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Pin>() != null)
            Destroy(col.gameObject);
    }
}
