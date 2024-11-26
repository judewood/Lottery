namespace LotteryApp.Models
{
    public record Prize(string Name, int PercentRevenue, int PercentPlayers);

    public record Ticket(int MinTickets, int MaxTickets, int Cost);

    public record Players(int Min, int Max, string Prefix);
}