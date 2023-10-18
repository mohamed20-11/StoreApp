using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class MyDBContext : IdentityDbContext<User>
    {
        public MyDBContext(DbContextOptions options) : base(options)
        { }
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductAttachment> ProductAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<ProductAttachment>(new ProductAttachmentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
