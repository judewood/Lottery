namespace LotteryApp.Extensions;

public static class IntExtensions
{
    //Takes a int representing main currency eg dollar, GBP and returns as string 
    //with decimal point in correct place. We don't need the accuracy of decimals for this game
    public static string ToCurrencyString(this int totalCents, string currencySymbol = "$", string separator = ".")
    {
        string dollars = (totalCents / 100).ToString();
        string cents = (totalCents % 100).ToString().PadLeft(2, '0');

        return currencySymbol + dollars + separator + cents;
    }
}