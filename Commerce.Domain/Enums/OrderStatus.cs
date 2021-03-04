namespace Commerce.Domain.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        Saved,
        Approved,
        Voided,
        Completed,
        PayerActionRequired
    }
}