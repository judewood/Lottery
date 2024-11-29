namespace LotteryApp.Models
{
    public record Prize(string Name, int PercentRevenue, int PercentTickets, int NumPlayers);

    public record TicketInfo(int MinTickets, int MaxTickets, int CostCents);

    public record PlayerInfo(int Min, int Max, string Prefix);

    public record PlayerTicket(Guid TicketId, string PlayerId);

    public record PlayerBalance(string PlayerId, int Balance);
}