using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Supermarket.Models.EntityLayer;

namespace Supermarket.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        public User CurrentUser { get; set; }

        //private readonly string connectionString = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SupermarketDb;Trusted_Connection=True;Encrypt=false");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReceiptItem>()
                .HasKey(k => new { k.ReceiptID, k.ProductID });

        }
    }
}
