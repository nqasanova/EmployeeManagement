using System;
using System.Collections.Generic;
using EmployeeManagement.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EmployeeManagement;Trusted_Connection=false;User=SA;Password=Nata2004.;TrustServerCertificate=True;");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}