namespace PriceTagControlServer.Services.Interfeces
{
    public interface ISenderToAIService
    {
        Task<(string ProductCategory, string ProductName, decimal Price, bool IsSocialPrice)> GetInfo(string base64Img);
    }
}