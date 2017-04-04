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
        Assert.AreEqual(1,1);
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
    public void T03BowlGutterReturnsTidy()
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
        for(var i = 1; i<= 10; i++)
            actionMaster.Bowl(10);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T06StrikeOnLastFrameBonus1ReturnsReset()
    {
        for (var i = 1; i <= 11; i++)
            actionMaster.Bowl(10);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T07StrikeOnLastFrameBonus2ReturnsEndGame()
    {
        for (var i = 1; i <= 12; i++)
            actionMaster.Bowl(10);

        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

}

