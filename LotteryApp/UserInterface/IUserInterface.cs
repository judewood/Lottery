using LotteryApp.Models;

namespace LotteryApp.UserInterface
{
    public interface IUserInterface
    {
        int PromptPurchase(string name, Ticket ticket, int max);
        string GetIntro(string name);
        string GetPrompt(string name, int min, int max);
        (int, string) GetTicketRequest(int min, int max);
    }
}