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
        var rolls = new[] { 1 };
        var rollString = "1";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02Bowl15()
    {
        var rolls = new[] { 1, 5 };
        var rollString = "15";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03BowlAll1()
    {
        var rolls = new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        var rollString = "111111111111111111111";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T04BowlSpare55()
    {
        var rolls = new[] { 5, 5 };
        var rollString = "55";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T05BowlSpare55Then43()
    {
        var rolls = new[] { 5, 5, 4, 3 };
        var rollString = "5543";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T06BowlTwoSpares()
    {
        var rolls = new[] { 5, 5, 2, 8 };
        var rollString = "5528";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07BowlSparesNormalSpare()
    {
        var rolls = new[] { 1, 9, 6, 2, 2, 8 };
        var rollString = "196228";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08BowlStrike()
    {
        var rolls = new[] { 10 };
        var rollString = "10";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T09BowlStrikeThenNormal()
    {
        var rolls = new[] { 10, 3, 4 };
        var rollString = "1034";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T10BowlPerfectGame()
    {
        var rolls = new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        var rollString = "10101010101010101010101010";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T11BowlAllGutters()
    {
        var rolls = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var rollString = "00000000000000000000";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    //// http://slocums.homestead.com/gamescore.html
    [Test]
    [Category("Verification")]
    public void TG02GoldenCopyA()
    {
        var rolls = new[] { 10, 7, 3, 9, 0, 10, 0, 8, 8, 2, 0, 6, 10, 10, 10, 8, 1 };
        var rollString = "1073901008820610101081";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    ////http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
    [Test]
    [Category("Verification")]
    public void TG03GoldenCopyB1of3()
    {
        int[] rolls = {10, 9, 1, 9, 1, 9, 1, 9, 1, 7, 0, 9, 0, 10, 8, 2, 8, 2, 10};
        var rollString = "1091919191709010828210";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    ////http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
    [Test]
    [Category("Verification")]
    public void TG03GoldenCopyB2of3()
    {
        int[] rolls = { 8, 2, 8, 1, 9, 1, 7, 1, 8, 2, 9, 1, 9, 1, 10, 10, 7, 1 };
        var rollString = "82819171829191101071";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    ////http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
    [Test]
    [Category("Verification")]
    public void TG03GoldenCopyB3of3()
    {
        int[] rolls = { 10, 10, 9, 0, 10, 7, 3, 10, 8, 1, 6, 3, 6, 2, 9, 1, 10 };
        var rollString = "1010901073108163629110";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    //// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
    [Test]
    [Category("Verification")]
    public void TG03GoldenCopyC1of3()
    {
        int[] rolls = { 7, 2, 10, 10, 10, 10, 7, 3, 10, 10, 9, 1, 10, 10, 9 };
        var rollString = "72101010107310109110109";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    //// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
    [Test]
    [Category("Verification")]
    public void TG03GoldenCopyC2of3()
    {
        int[] rolls = { 10, 10, 10, 10, 9, 0, 10, 10, 10, 10, 10, 9, 1 };
        var rollString = "1010101090101010101091";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}