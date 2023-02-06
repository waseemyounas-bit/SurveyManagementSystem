using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "admin@surveysystem.com",
                    Password = "12345678",
                    ConfirmPassword = "12345678",
                    Phone="123456789",
                    RoleId = 1
                }
            );
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<Survey>? Surveys { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<QuestionType>? QuestionTypes { get; set; }
        public DbSet<Answer>? Answers { get; set; }
    }
}
