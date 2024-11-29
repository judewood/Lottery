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

    public static T ExitIfNull<T>(this T? argument, int exitValue)
    {
        if (argument == null)
        {
            Environment.Exit(exitValue);
        }
        return argument;
    }

}