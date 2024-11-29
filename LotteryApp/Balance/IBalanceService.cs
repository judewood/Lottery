namespace LotteryApp.Account;

public interface IBalanceService
{
    int Get(string playerId);

    void Update(string playerId, int adjustment);
}