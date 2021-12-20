using CarRentalAPI.Models;
using CarRentalAPI.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Context
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<LogCustomer> LogCustomer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to one
            modelBuilder.Entity<Role>()
                 .HasOne(a => a.Employee)
                 .WithOne(b => b.Role)
                 .HasForeignKey<Employee>(b => b.RoleId);
            modelBuilder.Entity<Rental>()
                 .HasOne(a => a.LogCustomer)
                 .WithOne(b => b.Rental)
                 .HasForeignKey<LogCustomer>(b => b.OrderId);
            //Many to many Role
            modelBuilder.Entity<Customer>()
               .HasMany(a => a.Rental)
               .WithOne(b => b.Customer);
            modelBuilder.Entity<Employee>()
              .HasMany(a => a.Rental)
              .WithOne(b => b.Employee);
            modelBuilder.Entity<Car>()
              .HasMany(a => a.Rental)
              .WithOne(b => b.Car);
        }
    }

}
