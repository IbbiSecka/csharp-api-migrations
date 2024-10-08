﻿using exercise.pizzashopapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.pizzashopapi.Data
{
    public class DataContext : DbContext
    {
        private string connectionString;
        public DataContext() 
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseNpgsql(connectionString);

            //set primary of order?

            //seed data?

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>().HasKey(o => new { o.PizzaId, o.CustomerId });

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Pizza)
                .WithOne()
                .HasForeignKey<Order>(x => x.PizzaId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithOne()
                .HasForeignKey<Order>(k => k.CustomerId);       

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
