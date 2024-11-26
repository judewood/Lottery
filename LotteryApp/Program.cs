using LotteryApp.Games;
using LotteryApp.UserInterface;

Console.WriteLine("Hello, World!");

IUserInput userInput = new UserInput();
UserInterface userInterface = new UserInterface(userInput);

Lottery lottery = new Lottery(userInterface);

lottery.Game();
