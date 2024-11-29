namespace LotteryApp.Players
{
    public interface IPlayersService
    {
        int GetTotalPlayers(int min, int max);
    }
}