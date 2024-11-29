namespace LotteryApp.Balance;

public interface IBalanceService
{
    void SetInitialBalances(int totalPlayers, string prefix, string humanId, int initialBalance);
    int Get(string playerId);

    void Update(string playerId, int adjustment);
}