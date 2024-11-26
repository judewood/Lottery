namespace LotteryApp.random;

public class RandomService : IRandomService
{
    // Wraps around random to allow mocking
    public int GetRandom(int min, int max)
    {
        return new Random().Next(min, max);
    }
}