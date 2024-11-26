using LotteryApp;
using LotteryApp.Models;
using LotteryApp.UserInterface;
using System;
using Moq;
using System.Globalization;

namespace LotteryTest.UserInterfaceTests;

public class UnitTest1
{
    [Fact]
    public void DisplayIntro_DisplaysIntro()
    {
        var mockUserInput = new Mock<IUserInput>();
        UserInterface uut = new UserInterface(mockUserInput.Object);
        string playerName = "playername";

        string result = uut.GetIntro(playerName);

        Assert.Equal($"Welcome to the Bede Lottery, {playerName}{Environment.NewLine}", result);
    }

    [Fact]
    public void GetPrompt_DisplaysPrompt()
    {
        var mockUserInput = new Mock<IUserInput>();
        UserInterface uut = new UserInterface(mockUserInput.Object);
        string playerName = "playername";
        int min = 1;
        int max = 10;
        string expected = $"How many tickets would you like {playerName}? Enter between {min} and {max}.{Environment.NewLine}. Type'q' to quit";

        string result = uut.GetPrompt(playerName, 1, 10);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2", 2, "")]
    [InlineData("3", 3, "")]
    [InlineData("1", -1, "invalid")]
    [InlineData(null, -1, "invalid")]
    [InlineData("4", -1, "invalid")]
    [InlineData("Q", -1, "quit")]
    [InlineData("q", -1, "quit")]
    [InlineData("NaN", -1, "invalid")]
    public void GetTicketRequest_ReturnsExpected(string? userInput, int expNum, string expStatus)
    {
        var mockUserInput = new Mock<IUserInput>();
        UserInterface uut = new UserInterface(mockUserInput.Object);
        mockUserInput.Setup(x => x.GetUserInput()).Returns((string?)userInput);
        int min = 2;
        int max = 3;

        (int num, string status) = uut.GetTicketRequest(min, max);

        Assert.Equal(expNum, num);
        Assert.Equal(expStatus, status);
    }
}