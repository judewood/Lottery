using LotteryApp.Models;

namespace LotteryApp.Balance;

public class BalanceService : IBalanceService
{
    private List<PlayerBalance> Balances { get; set; }

    private const int startBalance = 1000;

    public BalanceService()
    {
        Balances = [];
    }

    public void SetInitialBalances(int totalPlayers, string prefix, string humanId)
    {
        Balances.Add(new PlayerBalance(humanId, startBalance));
        for (int i = 1; i < totalPlayers; i++)
        {
            string playerId = prefix + (i + 1).ToString();
            Balances.Add(new PlayerBalance(playerId, startBalance));
        }
    }

    public int Get(string playerId)
    {
        return Balances.Where(p => p.PlayerId == playerId).First().Balance;
    }

    public void Update(string playerId, int adjustment)
    {
        int index = Balances.FindIndex(p => p.PlayerId == playerId);
        int newBalance = Balances[index].Balance + adjustment;
        Balances[index] = new PlayerBalance(playerId, newBalance);
    }
}