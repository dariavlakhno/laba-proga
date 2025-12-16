using Lab7ORM.Entities;
using System.Data.Entity;

namespace Lab7ORM
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Создаем пользователей
            var user1 = new User { Username = "babuleh" };
            var user2 = new User { Username = "second_babuleh" };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();

            // Создаем заказы для первого пользователя
            var order1 = new Order
            {
                ProductName = "Diamonds",
                ProductCount = 100,
                UserId = user1.Id,
                User = user1
            };

            var order2 = new Order
            {
                ProductName = "Gold",
                ProductCount = 2000,
                UserId = user1.Id,
                User = user1
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.SaveChanges();
        }
    }
}