namespace LotteryApp.UserInterface
{
    public class UserInput : IUserInput
    {
        public string? GetUserInput(){
            return Console.ReadLine();
        }
    }
}