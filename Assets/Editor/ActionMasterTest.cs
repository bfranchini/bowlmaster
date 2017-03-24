﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;

    [Test]
    public void T00FailingTest()
    {
        Assert.AreEqual(1,1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {        
        var actionMaster = new ActionMaster();
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }
}

