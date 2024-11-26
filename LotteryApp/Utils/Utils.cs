namespace LotteryApp.Utils;

public static class Utils
{
    public static T? PositiveMin<T>(T x, T y ) where T : IComparable<T>
    {
        T rawMin = (Comparer<T>.Default.Compare(x, y) > 0) ? y : x;
        return (Comparer<T>.Default.Compare(rawMin, default) > 0) ? rawMin : default;
    }
}