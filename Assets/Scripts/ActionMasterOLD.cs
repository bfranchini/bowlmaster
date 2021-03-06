﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMasterOLD
{
    private int[] bowls = new int[21];

    //bowl 1 is the first frame of the game.
    private int bowl = 1;
    public enum Action { Tidy, Reset, EndTurn, EndGame }

    public static Action NextAction(List<int> pinFalls)
    {
        var actionMaster = new ActionMasterOLD();
        var nextAction = new Action();

        foreach (var pinFall in pinFalls)
            nextAction = actionMaster.Bowl(pinFall);

        return nextAction;
    }

    //TODO: make private
    private Action Bowl(int pins)
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

        //not a strike on first bonus after getting a strike on bowl 19 (first roll of frame 10)
        if (bowl == 20 && Bowl21Awarded() && pins < 10 && bowls[19 - 1] == 10)
        {
            bowl++;
            return Action.Tidy;
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
          
        //first bowl of frame
        if (bowl % 2 != 0)
        {
            //strike, go to the next odd numbered roll(new frame)
            if (pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            }

            //not a stike. go to next bowl
            {
                bowl++;
                return Action.Tidy;
            }
        }

        //second bowl of frame always increments by one
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
