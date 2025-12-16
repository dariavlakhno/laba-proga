using Lab7ORM.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Lab7ORM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
            // Логируем все SQL запросы в консоль (требование лабораторной)
            Database.Log = Console.WriteLine;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Убираем множественное число для названий таблиц
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Настраиваем связь один-ко-многим
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(true); // Каскадное удаление
        }
    }
}