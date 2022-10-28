using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace IT_Consulting_CRM_API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Requests> Request { get; set; }
        public DbSet<Blogs> Blog { get; set; }
        public DbSet<Contacts> Contact { get; set; }
        public DbSet<Projects> Project { get; set; }
        public DbSet<Services> Service { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var options = optionsBuilder.UseSqlServer(connectionString)
            .Options;
        }

        //public DataContext(DbContextOptions<DataContext> options) : base(options)
        //{
        //    Database.EnsureCreated();
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Requests>().HasData(
        //            new Requests { Id = 1, Name = "Карл", SurName = "Карлов", EMail = "Carlov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" },
        //            new Requests { Id = 2, Name = "Иван", SurName = "Иванов", EMail = "Ivanov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" },
        //            new Requests { Id = 3, Name = "Пётр", SurName = "Петров", EMail = "Petrov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" },
        //            new Requests { Id = 4, Name = "Вася", SurName = "Васёв", EMail = "Vasov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" },
        //            new Requests { Id = 5, Name = "Гена", SurName = "Генов", EMail = "Genov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" },
        //            new Requests { Id = 6, Name = "Сидор", SurName = "Сидоров", EMail = "Sedorov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" },
        //            new Requests { Id = 7, Name = "Слава", SurName = "Славов", EMail = "Slavov@mail.ru", Information = "Напишите мне, меня заинтересовало ваше предложение!" }
        //    );
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
