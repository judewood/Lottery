using LotteryApp.Utils;

namespace LotteryTest.UtilsTests;

public class UtilsTests
{
    [Theory]
    [InlineData(1, 2, 1)]
    [InlineData(0, 1, 0)]
    [InlineData(-1, 0, 0)]
    public void PositiveMin(int input1, int input2, int expected)
    {
        int result = Utils.PositiveMin(input1, input2);

        Assert.Equal(expected, result);
    }
}