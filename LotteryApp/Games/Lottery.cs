using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotteryApp.UserInterface;
using LotteryApp.Models;
using LotteryApp.Extensions;

namespace LotteryApp.Games
{

    public class Lottery
    {
        private IUserInterface UserInterface { get; init; }
        public Lottery(IUserInterface userInterface)
        {
            UserInterface = userInterface.ThrowIfNull(nameof(userInterface));
        }

        public void Game()
        {
            Ticket ticket = new Ticket(1, 10, 1);
            Players players = new Players(10, 15, "Player ");
            string intro = UserInterface.GetIntro(players.Prefix + "1");
            Console.WriteLine(intro);
            string playerName = players.Prefix + "1";
            //int balance = 10;
            int availableMaxTickets = ticket.MaxTickets;
            for (; ; )
            {
                Console.WriteLine(UserInterface.GetPrompt(playerName, ticket.MinTickets, availableMaxTickets));
                int numTickets = UserInterface.PromptPurchase(players.Prefix + "1", ticket, availableMaxTickets);
                if (numTickets == -1)
                {
                    break;
                }
            }
        }


    }
}