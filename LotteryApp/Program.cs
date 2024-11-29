using LotteryApp.Balance;
using LotteryApp.Games;
using LotteryApp.Models;
using LotteryApp.Players;
using LotteryApp.random;
using LotteryApp.UserInterface;

IRandomService randomService = new RandomService();
PlayersService playersService = new(randomService);
TicketInfo ticketInfo = new TicketInfo(1, 10, 100);
PlayerInfo playerInfo = new PlayerInfo(10, 15, "Player ");
int totalPlayers = playersService.GetTotalPlayers(playerInfo.Max);

string humanId = "Player 1"; //fixed but could be made user selectable later
BalanceService balanceService = new BalanceService();
balanceService.SetInitialBalances(totalPlayers, playerInfo.Prefix, humanId);
IUserInput userInput = new UserInput();
IUserInterface userInterface = new UserInterface(userInput);

Lottery lottery = new(balanceService, userInterface, randomService);
lottery.RunGames(totalPlayers, ticketInfo, playerInfo, humanId);
