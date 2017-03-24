using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame }

    public Action Bowl(int pins)
    {
        if(pins < 0 || pins > 10)
            throw new UnityException("Pins are less than 0 or more than 10");

        if(pins == 10)
            return Action.EndTurn;

        //other behavior here

        throw new UnityException("Not sure what action to return!");
    }
}
