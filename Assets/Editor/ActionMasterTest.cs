using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup()
    {
        pinFalls = new List<int>();
    }

    [Test]
    public void T00FailingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03BowlGutterOnRoll1ReturnsTidy()
    {
        pinFalls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T04Bowl28ReturnsEndTurn()
    {
        pinFalls.AddRange(new[] { 8, 2 });                
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T05StrikeOnLastFrameReturnsReset()
    {
        pinFalls.AddRange(new[] { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10 });        
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T06CheckResetAtSpareInLastFrame()
    {
        pinFalls.AddRange(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9,1 });

        //bowl up to frame 9 and get a spare for frame 10, then        
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T07YouTubeRollsEndInEndgame()
    {
        pinFalls.AddRange(new[] { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2 , 9});
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T08GameEndsAtBowl20()
    {
        pinFalls.AddRange(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 , 1});
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T09StrikeOnBowl19AndNotStrikeOn20ReturnsTidy()
    {
        //bowl up to frame 9,bowl a strike on frame 10, first bonus didn't knock all the pins. Get tidy
        pinFalls.AddRange(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 3 });
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T10ZeroTenSpareReturnsEndTurnAndOddFrame()
    {
        pinFalls.AddRange(new[] { 0, 10 });

        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T11Dondi10thFrameTurkey()
    {
        pinFalls.AddRange(new[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 });
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T12ZeroOnegivesEndTurn()
    {
        pinFalls.AddRange(new[] { 0, 1 });
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }
}

