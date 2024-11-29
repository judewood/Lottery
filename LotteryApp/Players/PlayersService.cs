using LotteryApp.Extensions;
using LotteryApp.random;

namespace LotteryApp.Players;

public class PlayersService:IPlayersService
{
    private IRandomService RandomService { get; init; }
    public PlayersService(IRandomService randomService)
    {
        RandomService = randomService.ThrowIfNull(nameof(randomService));
    }

    public int GetTotalPlayers(int min, int max)
    {
        return RandomService.GetRandom(min, max + 1);
    }
}