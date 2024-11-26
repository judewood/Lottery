using LotteryApp.Account;
using LotteryApp.Games;
using LotteryApp.Models;
using LotteryApp.random;
using LotteryApp.UserInterface;
using Moq;

namespace LotteryTest.GamesTests;

public class GamesTests
{
    [Fact]
    public void Lottery_ReturnsOnPlayerQuit()
    {
        var mockBalanceService = new Mock<IBalanceService>();
        var mockUserInterface = new Mock<IUserInterface>();
        var mockRandomService = new Mock<IRandomService>();
        mockUserInterface.Setup(x => x.GetTicketRequest(It.IsAny<int>(), It.IsAny<int>())).Returns((-1, Consts.QUIT));
        mockBalanceService.Setup(x => x.Get(It.IsAny<string>())).Returns(1000);
        Lottery uut = new(mockBalanceService.Object, mockUserInterface.Object, mockRandomService.Object);
        TicketInfo ticketInfo = new(1, 10, 100);
        PlayerInfo playerInfo = new(10, 15, "Player ");
        int totalPlayers = 5;

        string result = uut.PlayOneLottery(totalPlayers, ticketInfo, playerInfo, "humanId");

        Assert.Equal(Consts.QUIT, result);
    }
    [Fact]
    public void Lottery_ReturnsOnPlayerInsufficientFunds()
    {
        var mockBalanceService = new Mock<IBalanceService>();
        var mockUserInterface = new Mock<IUserInterface>();
        var mockRandomService = new Mock<IRandomService>();
        mockUserInterface.Setup(x => x.GetTicketRequest(It.IsAny<int>(), It.IsAny<int>())).Returns((1, ""));
        mockBalanceService.Setup(x => x.Get(It.IsAny<string>())).Returns(0);
        Lottery uut = new(mockBalanceService.Object, mockUserInterface.Object, mockRandomService.Object);
        TicketInfo ticketInfo = new(1, 10, 100);
        PlayerInfo playerInfo = new(10, 15, "Player ");
        int totalPlayers = 5;

        string result = uut.PlayOneLottery(totalPlayers, ticketInfo, playerInfo, "humanId");

        Assert.Equal(Consts.ZERO_BALANCE, result);
    }

    [Fact]
    public void Lottery_ReturnsOnAllCPUPlayersInsufficientFunds()
    {
        string humanId = "humanId";
        var mockBalanceService = new Mock<IBalanceService>();
        var mockUserInterface = new Mock<IUserInterface>();
        var mockRandomService = new Mock<IRandomService>();
        mockUserInterface.Setup(x => x.GetTicketRequest(It.IsAny<int>(), It.IsAny<int>())).Returns((1, ""));
        mockBalanceService.Setup(x => x.Get(humanId)).Returns(1000);
        mockBalanceService.Setup(x => x.Get("Player 2")).Returns(0);
        mockBalanceService.Setup(x => x.Get("Player 3")).Returns(0);
        Lottery uut = new(mockBalanceService.Object, mockUserInterface.Object, mockRandomService.Object);
        TicketInfo ticketInfo = new(1, 10, 100);
        PlayerInfo playerInfo = new(3, 3, "Player ");
        int totalPlayers = 5;

        string result = uut.PlayOneLottery(totalPlayers, ticketInfo, playerInfo, humanId);

        Assert.Equal(Consts.LAST_PLAYER, result);
    }

    [Theory]
    [InlineData(100, 0, 1, 1)]
    [InlineData(100, 10, 0, 10)]
    [InlineData(2, 80, 2, 2)]
    [InlineData(0, 80, 2, 0)]
    [InlineData(-1, 80, 2, 0)]
    public void GetNumPrizes_ReturnsExpected(int ticketCount, int percentTickets, int numPlayers, int expected)
    {
        int anyValue = 50;
        Prize prize = new Prize("Prize Name", anyValue, percentTickets, numPlayers);
        int result = Lottery.GetNumPrizes(ticketCount, prize);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(100, 50, 50)]
    [InlineData(0, 50, 0)]
    public void GetPrizeValue_ReturnsExpected(int ticketCount, int percentRevenue, int expected)
    {
        int anyValue = 50;
        Prize prize = new("Prize Name", percentRevenue, anyValue, anyValue);
        int result = Lottery.GetPrizeValue(ticketCount, prize);
        Assert.Equal(expected, result);
    }
}