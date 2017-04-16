using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] RollTexts, FrameTexts;

    void Start()
    {
        RollTexts[0].text = "x";
        FrameTexts[0].text = "0";
    }

    public void FillRollCard(List<int> rolls)
    {
        var test = rolls[-1];
    }
}

