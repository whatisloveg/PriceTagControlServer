using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PriceTagControlServer.ApplictaionContext.Models;

namespace PriceTagControlServer.ApplictaionContext.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.HasOne(c => c.ProductCategory)
                .WithMany() 
                .HasForeignKey(c => c.CategoryId)
                .IsRequired(); 

            builder.HasOne(c => c.Shop)
                .WithMany() 
                .HasForeignKey(c => c.ShopId) 
                .IsRequired();
        }
    }
}
