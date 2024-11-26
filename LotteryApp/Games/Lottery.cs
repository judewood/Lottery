using LotteryApp.UserInterface;
using LotteryApp.Models;
using LotteryApp.Extensions;
using LotteryApp.random;
using LotteryApp.Account;
using static LotteryApp.Utils.Utils;

namespace LotteryApp.Games;

public class Lottery
{
    private IUserInterface UserInterface { get; init; }
    private IRandomService RandomService { get; init; }
    private IBalanceService Balances { get; init; }
    public Lottery(IBalanceService balanceService, IUserInterface userInterface, IRandomService randomService)
    {
        Balances = balanceService.ThrowIfNull(nameof(balanceService));
        UserInterface = userInterface.ThrowIfNull(nameof(userInterface));
        RandomService = randomService.ThrowIfNull(nameof(randomService));
    }

    public void GameIntro(string playerName, int balance, int cost)
    {
        Console.WriteLine(UserInterface.GetIntro(playerName));
        Console.WriteLine($"* Your digital balance : {balance.ToCurrencyString()}");
        Console.WriteLine($"* Ticket Price : {cost.ToCurrencyString()} each{Environment.NewLine}{Environment.NewLine}");
    }

    private static Prize[] GetPrizes()
    {
        Prize[] prizes = new Prize[3];
        prizes[0] = new Prize("Grand Prize", 50, 0, 1);
        prizes[1] = new Prize("Second Tier", 30, 10, 0);
        prizes[2] = new Prize("Third Tier", 10, 20, 0);
        return prizes;
    }

    public string RunGames(int totalPlayers, TicketInfo ticketInfo, PlayerInfo playerInfo, string humanId)
    {
        string status;
        for (; ; ) //forever
        {
            status = PlayOneLottery(totalPlayers, ticketInfo, playerInfo, humanId);
            if (status != string.Empty)
            {
                break;
            }
        }
        return status;

    }

    public string PlayOneLottery(int totalPlayers, TicketInfo ticketInfo, PlayerInfo playerInfo, string humanId)
    {
        string status;
        (List<PlayerTicket> tickets, status) = BuyTickets(totalPlayers, ticketInfo, playerInfo, humanId);
        if (status != string.Empty)
        {
            return status;
        }
        AwardPrizes(tickets, ticketInfo.CostCents);
        return status;
    }

    private (List<PlayerTicket>, string) BuyTickets(int totalPlayers, TicketInfo ticketInfo, PlayerInfo playerInfo, string humanId)
    {
        List<PlayerTicket> GameTickets = [];
        GameIntro(humanId, Balances.Get(humanId), ticketInfo.CostCents);
        string status;
        (GameTickets, status) = BuyHumanTickets(ticketInfo, humanId, GameTickets);
        if (status != string.Empty)
        {
            return (GameTickets, status);
        }

        bool CPUAllInsufficientFunds = true;
        for (int i = 1; i < totalPlayers; i++)
        {
            CPUAllInsufficientFunds = BuyCPUTickets(ticketInfo, playerInfo, GameTickets, CPUAllInsufficientFunds, i);
        }
        if (CPUAllInsufficientFunds)
        {
            return (GameTickets, Consts.LAST_PLAYER);
        }
        return (GameTickets, string.Empty);
    }

    private (List<PlayerTicket>, string) BuyHumanTickets(TicketInfo ticketInfo, string humanId, List<PlayerTicket> GameTickets)
    {
        string status;
        (int numHumanTickets, status) = GetNumHumanTickets(ticketInfo, Balances.Get(humanId));
        if (status != string.Empty)
        {
            return (GameTickets, status);
        }
        Balances.Update(humanId, -(numHumanTickets * ticketInfo.CostCents));
        Console.WriteLine(UserInterface.GetTicketsBoughtMsg(humanId, numHumanTickets, Balances.Get(humanId)));
        GameTickets.AddRange(GetPlayersTickets(numHumanTickets, humanId));
        return (GameTickets, string.Empty);
    }

    private bool BuyCPUTickets(TicketInfo ticketInfo, PlayerInfo playerInfo, List<PlayerTicket> GameTickets, bool CPURunOutOfMoney, int i)
    {
        int ticketsBought = 0;
        string playerId = playerInfo.Prefix + (i + 1).ToString();
        if (Balances.Get(playerId) >= ticketInfo.CostCents) //can buy at least one ticket
        {
            CPURunOutOfMoney = false;
            int maxTickets = GetMaxTickets(Balances.Get(playerId), ticketInfo.MaxTickets, ticketInfo.CostCents);
            ticketsBought = RandomService.GetRandom(1, maxTickets + 1);
            Balances.Update(playerId, -(ticketsBought * ticketInfo.CostCents));
            GameTickets.AddRange(GetPlayersTickets(ticketsBought, playerId));
        }
        Console.WriteLine(UserInterface.GetTicketsBoughtMsg(playerId, ticketsBought, Balances.Get(playerId)));
        return CPURunOutOfMoney;
    }

    private (int, string) GetNumHumanTickets(TicketInfo ticket, int balance)
    {
        int maxHumanTickets = GetMaxTickets(balance, ticket.MaxTickets, ticket.CostCents);
        if (maxHumanTickets <= 0)
        {
            return (0, Consts.ZERO_BALANCE);
        }
        return GetNumHumanTickets(ticket.MinTickets, maxHumanTickets);
    }

    private int GetMaxTickets(int balance, int max, int cost)
    {
        return Math.Min(balance / cost, max);
    }
    private (int, string) GetNumHumanTickets(int min, int max)
    {
        int numHumanTickets;
        for (; ; )
        {
            Console.WriteLine(UserInterface.GetPrompt(min, max));
            (numHumanTickets, string status) = UserInterface.GetTicketRequest(min, max);
            if (status == Consts.QUIT)
            {
                return (0, status);
            }
            if (status != Consts.INVALID)
            {
                break;
            }
        }
        return (numHumanTickets, string.Empty);
    }

    public void AwardPrizes(List<PlayerTicket> tickets, int ticketCost)
    {
        Console.WriteLine($"{Environment.NewLine}Ticket Draw Results:");
        Prize[] prizes = GetPrizes();
        int totalTicketRevenue = tickets.Count * ticketCost;
        int totalPrizeValue = 0;
        int ticketsSold = tickets.Count;
        Console.WriteLine($"Tickets sold:{ticketsSold}, total revenue {totalTicketRevenue.ToCurrencyString()}");
        foreach (var prize in prizes)
        {
            int prizeValue = GetPrizeValue(totalTicketRevenue, prize);
            int numPrizes = GetNumPrizes(ticketsSold, prize);
            int prizePerPlayer = numPrizes <= 0 ? 0: prizeValue / numPrizes;
            totalPrizeValue += prizeValue;
            Console.WriteLine($"{prize.Name} value {prizeValue.ToCurrencyString()} will be shared by {numPrizes} players, {prizePerPlayer.ToCurrencyString()} each");
            List<PlayerTicket> winningTickets = AwardPrize(numPrizes, tickets, prize);
            foreach (PlayerTicket wt in winningTickets)
            {
                Balances.Update(wt.PlayerId, prizePerPlayer);
                Console.WriteLine($"* {prize.Name}: {wt.PlayerId} won {prizePerPlayer.ToCurrencyString()}!. New balance is {Balances.Get(wt.PlayerId).ToCurrencyString()}");
                PlayerTicket? ticketToRemove = tickets.Where(t => t.TicketId == wt.TicketId).FirstOrDefault();
                if (ticketToRemove is not null)
                {
                    tickets.Remove(ticketToRemove);
                }
            }
        }
        Console.WriteLine($"Congratulations to the winners!{Environment.NewLine}");

        int houseRevenue = totalTicketRevenue - totalPrizeValue;
        Console.WriteLine($"House Revenue: {houseRevenue.ToCurrencyString()}{Environment.NewLine}{Environment.NewLine}");
    }

    public static int GetPrizeValue(int totalTicketRevenue, Prize prize)
    {
        return totalTicketRevenue * prize.PercentRevenue / 100;
    }

    private List<PlayerTicket> GetPlayersTickets(int numTickets, string playerId)
    {
        var tickets = new List<PlayerTicket>();
        for (int i = 0; i < numTickets; i++)
        {
            PlayerTicket playerTicket = new PlayerTicket(Guid.NewGuid(), playerId);
            tickets.Add(playerTicket);
        }
        return tickets;
    }

    public List<PlayerTicket> AwardPrize(int numPrizes, List<PlayerTicket> tickets, Prize prize)
    {
        List<PlayerTicket> winningTickets = [];
        for (; ; )
        {
            if (winningTickets.Count >= numPrizes)
            {
                break;
            }
            int winnerIndex = RandomService.GetRandom(0, tickets.Count);
            var duplicate = winningTickets.Where(p => p.TicketId == tickets[winnerIndex].TicketId).FirstOrDefault();
            if (duplicate is null)
            {
                winningTickets.Add(tickets[winnerIndex]);
            }
        }
        return winningTickets;
    }

    public static int GetNumPrizes(int numTickets, Prize prize)
    {
        int numPrizes;

        if (prize.NumPlayers > 0) //grand prize
        {
            numPrizes = PositiveMin(prize.NumPlayers, numTickets);
        }
        else
        {
            numPrizes = numTickets * prize.PercentTickets / 100; //will round down 
        }

        return numPrizes;
    }


}