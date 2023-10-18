using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ProductAttachmentConfiguration
         : IEntityTypeConfiguration<ProductAttachment>
    {
        public void Configure(EntityTypeBuilder<ProductAttachment> builder)
        {
            builder.ToTable("ProductAttachment");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Image).IsRequired();

            builder
               .HasOne(e => e.Product)
               .WithMany(e => e.ProductAttachments)
               .HasForeignKey(e => e.ProductID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
