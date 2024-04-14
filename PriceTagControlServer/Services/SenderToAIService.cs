using PriceTagControlServer.Services.Interfeces;

namespace PriceTagControlServer.Services
{
    public class SenderToAIService : ISenderToAIService
    {
        public async Task<(string ProductCategory, string ProductName, decimal Price, bool IsSocialPrice)> GetInfo(string base64Img)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync("http://89.111.174.185", new StringContent(base64Img));
                string responseBody = await response.Content.ReadAsStringAsync();
                return await ParseResponse(responseBody);
            }
        }

        private async Task<(string ProductCategory, string ProductName, decimal Price, bool IsSocialPrice)> ParseResponse(string respone)
        {
            //парсим
            return (string.Empty, string.Empty, 0, false);
        }
    }
}
