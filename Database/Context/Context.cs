using System.Data;
using Database;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Database.Context
{
    public class EventContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=ASUS;Database=EventManagement;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0",
                sqlOptions => sqlOptions.EnableRetryOnFailure());
        }

        public DbSet<UserInfo>? UserInfo { get; set; }
    }
}
