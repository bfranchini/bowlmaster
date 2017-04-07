using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    private int[] bowls = new int[21];

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

        bowls[bowl-1] = pins;

        //bonus frame, end game
        if (bowl == 21)
        {
            return Action.EndGame;
        }
        
        //handle last-frame special cases        
        if (bowl >= 19 && Bowl21Awarded())
        {
            bowl++;
            return Action.Reset;
        }

        //no bonus for you, game over
        if (bowl == 20 && !Bowl21Awarded())
        {
            return Action.EndGame;
        }                
        
        //strike, go to the next odd numbered roll(new frame)
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

    private bool Bowl21Awarded()
    {
        //Remember that arrays start counting at 0
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }
}
