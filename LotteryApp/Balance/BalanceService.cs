using LotteryApp.Models;

namespace LotteryApp.Account;


public class BalanceService : IBalanceService
{
    private List<PlayerBalance> balances { get; set; }

    private const int startBalance = 1000;

    public BalanceService(int totalPlayers, string prefix, string humanId)
    {
        balances = new List<PlayerBalance>();

        balances.Add(new PlayerBalance(humanId, startBalance));
        for (int i = 1; i < totalPlayers; i++)
        {
            string playerId = prefix + (i + 1).ToString();
            balances.Add( new PlayerBalance(playerId, startBalance));
        }
    }

    public int Get(string playerId)
    {
        return balances.Where(p => p.PlayerId == playerId).First().Balance;
    }

    public void Update(string playerId, int adjustment)
    {
        int index = balances.FindIndex(p => p.PlayerId == playerId);
        int newBalance = balances[index].Balance + adjustment;
        balances[index] = new PlayerBalance(playerId, newBalance);
    }
}