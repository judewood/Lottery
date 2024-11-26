using LotteryApp.Extensions;
using LotteryApp.Models;

namespace LotteryApp.UserInterface
{
    public class UserInterface : IUserInterface
    {
        private IUserInput UserInput { get; init; }
        public UserInterface(IUserInput userInput)
        {
            UserInput = userInput.ThrowIfNull(nameof(userInput));
        }

        public string GetIntro(string name)
        {
            return $"Welcome to the Bede Lottery, {name}{Environment.NewLine}";
        }

        public string GetPrompt(string name, int min, int max)
        {
            return $"How many tickets would you like {name}? Enter between {min} and {max}.{Environment.NewLine}. Type'q' to quit";
        }

        public (int, string) GetTicketRequest(int min, int max) {
            string? input = UserInput.GetUserInput();
            if (input is not null && input.ToLower() == "q")
            {
                return (-1, "quit");
            }
            if (int.TryParse(input, out int numTickets))
            {
                if (numTickets <= max &&  numTickets >= min)
                {
                    return (numTickets, string.Empty);
                }
            }
            return (-1, "invalid");
        }

        public int PromptPurchase(string name, Ticket ticket, int max)
        {
            for (; ; ) //forever 
            {
                Console.WriteLine($"How many tickets would you like {name}. Enter between {ticket.MinTickets} and {max}.{Environment.NewLine}. Type'q' to quit");
                string? input = Console.ReadLine();
                if (input is not null && input.ToLower() == "q") {
                    Console.WriteLine("You quit. Bye");
                    return -1;
                }
                if (int.TryParse(input, out int numTickets))
                {
                    if (numTickets <= max || numTickets < ticket.MinTickets)
                    {
                        return numTickets;
                    }
                }
                Console.WriteLine($"Alas '{input}' tickets cannot be purchased");
            }
        }
    }
}