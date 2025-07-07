using System.Data;
using Database;
using Database.ViewModel;
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

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Standard> Standard { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<AddOns> AddOns { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<PaymentAddOn> PaymentAddOn { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Top10Images> Top10Images { get; set; } 


        public DbSet<UserData> UserData { get; set; }
        public DbSet<Event_UserInfo> Event_UserInfo{ get; set; }
        public DbSet<EventSize_UserInfo> EventSize_UserInfo { get; set; }
        public DbSet<Package_UserInfo> Package_UserInfo { get; set; }
        public DbSet<OrderList> OrderList { get; set; }
        public DbSet<PaymentAddOnView> PaymentAddOnView { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event_UserInfo>()
                .HasNoKey()
                .ToView("Event_UserInfo");
            modelBuilder.Entity<Package_UserInfo>().HasNoKey().ToView("Package_UserInfo");

            base.OnModelCreating(modelBuilder);
        }

    }
}
