namespace LotteryApp.Extensions;

public static class GenericExtensions
{
    public static T ThrowIfNull<T>(this T argument, string argumentName)
    {
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
        return argument;
    }

}