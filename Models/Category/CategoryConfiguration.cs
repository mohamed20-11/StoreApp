using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
    
            builder.HasKey(b => b.ID);
            builder.Property(b => b.ID).ValueGeneratedOnAdd();

            builder.Property(b => b.Name).HasMaxLength(1000).IsRequired();
            builder.Property(b => b.Image).IsRequired();
        }
    }
}
