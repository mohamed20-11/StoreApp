using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            //builder.HasIndex(b => b.Name).IsUnique();
            builder.HasKey(b => b.ID);
            builder.Property(b => b.ID).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).HasMaxLength(1000).IsRequired();
            builder.Property(b => b.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(b => b.Quantity).IsRequired().HasDefaultValue(1);
            builder.Property(b => b.Price).IsRequired();
            builder
            .HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        }
    }
}
