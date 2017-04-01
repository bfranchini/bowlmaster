using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    //private int[] bowls = new int[21];

    //bowl 1 is the first frame of the game.
    private int bowl = 1;
    public enum Action { Tidy, Reset, EndTurn, EndGame }

    public Action Bowl(int pins)
    {
        if(pins < 0 || pins > 10)
            throw new UnityException("Pins are less than 0 or more than 10");

        //other behavior here, e.g. last frame

        if (pins == 10)
        {
            bowl += 2;
            return Action.EndTurn;            
        }
          
        //mid frame or last frame(frame is odd)
        if (bowl % 2 != 0)
        {
            bowl++;
            return Action.Tidy;
        }

        //end of frame(frame is even)
        if (bowl % 2 == 0)
        {
            bowl++;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return!");
    }
}
