﻿using System.Data;
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
        public DbSet<EventSize> EventSize{ get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<MaxOrderLimit> MaxOrderLimit { get; set; }
        public DbSet<PrevOrders> PrevOrders { get; set; }



        public DbSet<UserData> UserData { get; set; }
        public DbSet<Event_UserInfo> Event_UserInfo{ get; set; }
        public DbSet<EventSize_UserInfo> EventSize_UserInfo { get; set; }
        public DbSet<Package_UserInfo> Package_UserInfo { get; set; }
        public DbSet<OrderList> OrderList { get; set; }


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
