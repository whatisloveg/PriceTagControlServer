namespace PriceTagControlServer.ApplictaionContext.Contracts
{
    public record Request(
        string Name,
        string Category,
        decimal Price,
        bool IsSocialPrice);
}
