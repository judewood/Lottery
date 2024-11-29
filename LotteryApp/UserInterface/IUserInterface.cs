using LotteryApp.Models;

namespace LotteryApp.UserInterface
{
    public interface IUserInterface
    {
        string GetIntro(string name);
        string GetPrompt(int min, int max);
        string GetQuitMessage();

        string GetInsufficientFundsMsg();

        string GetCPUInsufficientFundsMsg();
        string GetPlayersMsg(int num);
        string GetTicketsBoughtMsg(string playerName, int num, int balance);
        (int, string) GetTicketRequest(int min, int max);
        string GetExitMessage(string status);
    }
}