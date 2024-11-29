using LotteryApp.Extensions;
using LotteryApp.Models;

namespace LotteryApp.UserInterface;

public class UserInterface : IUserInterface
{
    private IUserInput UserInput { get; init; }
    public UserInterface(IUserInput userInput)
    {
        UserInput = userInput.ThrowIfNull(nameof(userInput));
    }

    public string GetIntro(string name)
    {
        return $"Welcome to Jude's Lottery, {name}{Environment.NewLine}";
    }

    public string GetPrompt(int min, int max)
    {
        return $"How many tickets do you want to buy? Enter between {min} and {max}.{Environment.NewLine}Type'q' to quit";
    }

    public (int, string) GetTicketRequest(int min, int max)
    {
        string? input = UserInput.GetUserInput();
        if (input is not null && input.ToLower() == "q")
        {
            return (-1, Consts.QUIT);
        }
        if (int.TryParse(input, out int numTickets))
        {
            if (numTickets <= max && numTickets >= min)
            {
                return (numTickets, string.Empty);
            }
        }
        return (-1, Consts.INVALID);
    }

    public string GetQuitMessage()
    {
        return $"You quit the game. Bye";
    }

    public string GetPlayersMsg(int num)
    {
        return $"{num} other CPU players have also bought tickets";
    }

    public string GetTicketsBoughtMsg(string playerName, int num, int balance)
    {
        return $"{playerName} bought {num} tickets and has {balance.ToCurrencyString()} remaining";
    }

    public string GetInsufficientFundsMsg()
    {
        return $"You have insufficient funds to purchase a ticket";
    }

    public string GetCPUInsufficientFundsMsg()
    {
        return $"THE CPU players all have insufficient funds to purchase a ticket";
    }

    public string GetExitMessage(string status)
    {
        return status switch
        {
            Consts.QUIT => GetQuitMessage(),
            Consts.ZERO_BALANCE => GetInsufficientFundsMsg(),
            Consts.LAST_PLAYER => GetCPUInsufficientFundsMsg(),
            _ => status,
        };
    }
}