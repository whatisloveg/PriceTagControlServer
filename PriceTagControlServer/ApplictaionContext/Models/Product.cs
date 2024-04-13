namespace PriceTagControlServer.ApplictaionContext.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public decimal Price { get; set; }  
    }
}
