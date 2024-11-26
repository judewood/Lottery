namespace LotteryApp.Account;

public class PlayerBalance {
    public string PlayerId { get; set;}
    public  int Balance {get;set;}
    public PlayerBalance(string playerId, int balance)
    {
        PlayerId = playerId;
        Balance = balance;
    }
}

public class BalanceService : IBalanceService
{
    private PlayerBalance[] balances { get; set; }

    private const int startBalance = 1000;

    public BalanceService(int totalPlayers, string prefix, string humanId)
    {
        balances = new PlayerBalance[totalPlayers];
        balances[0] = new(humanId, startBalance);
        for (int i = 1; i < totalPlayers; i++)
        {
            string playerId = prefix + (i +1).ToString();
            balances[i] = new(playerId, startBalance);
        }
    }

    public int Get(string playerId)
    {
        return balances.Where(p => p.PlayerId == playerId).First().Balance;
    }

    public void Update(string playerId, int adjustment)
    {
        PlayerBalance balance = balances.Where(p => p.PlayerId == playerId).First();
        balance.Balance = balance.Balance + adjustment;
    }
}