using LotteryApp.Balance;
using LotteryApp.Extensions;
using LotteryApp.Games;
using LotteryApp.Models;
using LotteryApp.Players;
using LotteryApp.random;
using LotteryApp.UserInterface;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider services;

Lottery lottery;
IBalanceService balanceService;
IPlayersService playersService;
try
{
    services = CreateServices();
    lottery = services.GetRequiredService<Lottery>();
    balanceService = services.GetService<IBalanceService>().ExitIfNull(2);
    playersService = services.GetService<IPlayersService>().ExitIfNull(2);
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
    throw;
}

TicketInfo ticketInfo = new(1, 10, 100);
PlayerInfo playerInfo = new(10, 15, "Player ");
int totalPlayers = playersService.GetTotalPlayers(playerInfo.Min, playerInfo.Max);
string humanId = "Player 1"; //fixed but could be made user selectable later
balanceService.SetInitialBalances(totalPlayers, playerInfo.Prefix, humanId, 1000);
lottery.RunGames(totalPlayers, ticketInfo, playerInfo, humanId);

static ServiceProvider CreateServices()
{
    var serviceProvider = new ServiceCollection()
        .AddSingleton<IRandomService, RandomService>()
        .AddSingleton<IPlayersService, PlayersService>()
        .AddSingleton<IBalanceService, BalanceService>()
        .AddSingleton<Lottery, Lottery>()
        .AddSingleton<IUserInput, UserInput>()
        .AddSingleton<IUserInterface, UserInterface>()
        .BuildServiceProvider();

    return serviceProvider;
}
