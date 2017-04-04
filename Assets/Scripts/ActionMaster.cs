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
        //strike bonus: pins this frame + next two rolls
        //spare bonus: pins this frame + next rolls
        //both bonuses apply to the tenth frame

        //not strike frame 1 = tidy
        //not strike frame 2 or spare  = reset(frame 2 is always reset
        //strike frame 1 = reset

        if (pins < 0 || pins > 10)
            throw new UnityException("Pins are less than 0 or more than 10");

        //last frame strike
        if (bowl == 21 && pins == 10)
        {
            bowl++;
            return Action.Reset;
        }

        //last frame first bonus strike
        if (bowl == 22 && pins == 10)
        {
            bowl++;
            return Action.Reset;
        }

        //last frame second bonus strike
        if (bowl == 23 && pins == 10)
        {
            bowl++;
            return Action.EndGame;
        }

        ////last frame spare 
        //if(bowl == 21 && pins )

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
