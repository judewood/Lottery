using LotteryApp.Extensions;

namespace LotteryTest.Extensions;

public class ExtensionsTests
{
    [Theory]
    [InlineData(200, "$2.00")]
    [InlineData(20,  "$0.20")]
    public void Extensions_ConvertsCurrency(int input, string expected) {
           string result = input.ToCurrencyString("$", ".");

           Assert.Equal(expected, result);
    }
}