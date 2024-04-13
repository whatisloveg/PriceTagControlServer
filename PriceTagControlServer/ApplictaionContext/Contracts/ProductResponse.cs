namespace PriceTagControlServer.ApplictaionContext.Contracts
{
    public record ProductResponse(
        string Name,
        string Category,
        string ShopAddress,
        decimal Price);
}
