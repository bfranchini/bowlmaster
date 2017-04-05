using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    private int[] bowls = new int[22];

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

        bowls[bowl-1] = pins;

        if (pins < 0 || pins > 10)
            throw new UnityException("Pins are less than 0 or more than 10");

        //frame 10 was was strike and we're on bonus frame 1. Do one more bonus
        if (bowl == 21 && bowls[18] == 10)
        {
            bowl++;
            return Action.Reset;
        }

        //frame 10 was was not a strike and we're on bonus frame 1. end game
        if (bowl == 21 && bowls[18] != 10)
        {
            return Action.EndGame;            
        }

        //last frame second bonus, end game
        if (bowl == 22)
        {            
            return Action.EndGame;
        }    

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
