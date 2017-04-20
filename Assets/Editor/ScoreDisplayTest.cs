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
        var rollString = "5/";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T05BowlSpare55Then43()
    {
        var rolls = new[] { 5, 5, 4, 3 };
        var rollString = "5/43";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T06BowlTwoSpares()
    {
        var rolls = new[] { 5, 5, 2, 8 };
        var rollString = "5/2/";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07BowlSparesNormalSpare()
    {
        var rolls = new[] { 1, 9, 6, 2, 2, 8 };
        var rollString = "1/622/";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08BowlStrike()
    {
        var rolls = new[] { 10 };
        var rollString = "X ";//remember the space
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T09BowlStrikeThenNormal()
    {
        var rolls = new[] { 10, 3, 4 };
        var rollString = "X 34";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }  

    [Test]
    public void T10BowlPerfectGame()
    {
        var rolls = new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        var rollString = "X X X X X X X X X XXX";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T11BowlAllGutters()
    {
        var rolls = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var rollString = "--------------------";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T12Bowl19()
    {
        var rolls = new[] { 1, 9 };
        var rollString = "1/";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T13BowlSpareInLastFrame()
    {
        var rolls = new[] { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,9,3 };
        var rollString = "1111111111111111111/3";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T14BowlStrikeInLastFrame()
    {
        var rolls = new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1 };
        var rollString = "111111111111111111X11";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T15BowlZero()
    {
        var rolls = new[] {0};
        var rollString = "-";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    //http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
    [Category("Verification")]
    [Test]
    public void TG01GoldenCopyB1of3()
    {
        int[] rolls = { 10, 9, 1, 9, 1, 9, 1, 9, 1, 7, 0, 9, 0, 10, 8, 2, 8, 2, 10 };
        string rollsString = "X 9/9/9/9/7-9-X 8/8/X";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    //http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
    [Category("Verification")]
    [Test]
    public void TG02GoldenCopyB2of3()
    {
        int[] rolls = { 8, 2, 8, 1, 9, 1, 7, 1, 8, 2, 9, 1, 9, 1, 10, 10, 7, 1 };
        string rollsString = "8/819/718/9/9/X X 71";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    //http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
    [Category("Verification")]
    [Test]
    public void TG03GoldenCopyB3of3()
    {
        int[] rolls = { 10, 10, 9, 0, 10, 7, 3, 10, 8, 1, 6, 3, 6, 2, 9, 1, 10 };
        string rollsString = "X X 9-X 7/X 8163629/X";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    // http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
    [Category("Verification")]
    [Test]
    public void TG04GoldenCopyC1of3()
    {
        int[] rolls = { 7, 2, 10, 10, 10, 10, 7, 3, 10, 10, 9, 1, 10, 10, 9 };
        string rollsString = "72X X X X 7/X X 9/XX9";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    // http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
    [Category("Verification")]
    [Test]
    public void TG05GoldenCopyC2of3()
    {
        int[] rolls = { 10, 10, 10, 10, 9, 0, 10, 10, 10, 10, 10, 9, 1 };
        string rollsString = "X X X X 9-X X X X X91";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}