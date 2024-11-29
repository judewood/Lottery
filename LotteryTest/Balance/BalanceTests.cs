using LotteryApp.Balance;

namespace LotteryTest.Balance;

public class BalanceTests
{
    [Fact]
    public void SetInitialBalances_SetsBalances()
    {
        BalanceService uut = new();
        int totalPlayers = 3;
        int initBalance = 100;
        string prefix = "prefix";
        uut.SetInitialBalances(totalPlayers, prefix, "human", initBalance);
        int balance = uut.Get("human");
        Assert.Equal(initBalance, balance);
        for (int i = 1; i < totalPlayers; i++)
        {
            balance = uut.Get(prefix + (i+1));
            Assert.Equal(initBalance, balance);
        }
    }

    [Fact]
    public void SetBalance_SetsBalance()
    {
        BalanceService uut = new();
        int totalPlayers = 2;
        int initBalance = 100;
        string playerId = "human";
        uut.SetInitialBalances(totalPlayers, "prefix", playerId, initBalance);
        int balance = uut.Get("human");
        Assert.Equal(initBalance, balance);
        uut.Update(playerId, -10);
        var result = uut.Get(playerId);
        Assert.Equal(90, result);
    }
}