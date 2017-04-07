using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;

    [SetUp]
    public void Setup()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00FailingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03BowlGutterOnRoll1ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T04Bowl28ReturnsEndTurn()
    {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T05StrikeOnLastFrameReturnsReset()
    {
        for (var i = 1; i <= 18; i++)
            actionMaster.Bowl(1);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T06CheckResetAtSpareInLastFrame()
    {
        //bowl up to frame 9 and... 
        for (var i = 1; i <= 18; i++)
            actionMaster.Bowl(1);

        //get a spare for frame 10
        actionMaster.Bowl(9);

        //here we're bowling bonus 1
        Assert.AreEqual(reset, actionMaster.Bowl(1));
    }

    [Test]
    public void T07YouTubeRollsEndInEndgame()
    {
        var rolls = new[] { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2 };

        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T08GameEndsAtBowl20()
    {
        var rolls = new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        foreach (var roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T09StrikeOnBowl19AndNotStrikeOn20ReturnsTidy()
    {
        //bowl up to frame 9
        for (var i = 1; i <= 18; i++)
            actionMaster.Bowl(1);

        //bowl a strike on frame 10
        actionMaster.Bowl(10);

        //first bonus didn't knock all the pins. Get tidy
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void T10BensBowl20Test()
    {
        //bowl up to frame 9
        for (var i = 1; i <= 18; i++)
            actionMaster.Bowl(1);

        //bowl a strike on frame 10
        actionMaster.Bowl(10);

        //first bonus didn't knock all the pins. Get tidy
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }
}

