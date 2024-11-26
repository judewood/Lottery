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

    public int GetTotalPlayers(int max)
    {
        return RandomService.GetRandom(1, max + 1) + 1;  //add the human player
    }
}