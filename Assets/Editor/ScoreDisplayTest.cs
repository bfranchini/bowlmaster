using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest
{

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl1()
    {
        var rolls = new[]{1};
        var rollString = "1";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}