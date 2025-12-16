using System;
using System.Data.Entity;
using System.Linq;

namespace Lab7ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа: Работа с RDBMS через ORM ===");
            Console.WriteLine("Требования лабораторной:");
            Console.WriteLine("1. Использование Entity Framework (ORM)");
            Console.WriteLine("2. Логирование всех SQL запросов в консоль");
            Console.WriteLine("3. Связь один-ко-многим (User-Order)");
            Console.WriteLine("4. Lazy loading по умолчанию");
            Console.WriteLine("5. Eager loading для получения данных\n");

            try
            {
                // Устанавливаем инициализатор БД
                Database.SetInitializer(new DatabaseInitializer());

                using (var db = new ApplicationDbContext())
                {
                    Console.WriteLine("1. Инициализация базы данных...");
                    db.Database.Initialize(true);
                    Console.WriteLine("   ✅ База данных создана\n");

                    Console.WriteLine("2. Получение всех пользователей с заказами (EAGER загрузка)...");
                    Console.WriteLine("   SQL запрос будет содержать JOIN для заказов:\n");

                    // EAGER загрузка - один запрос с JOIN
                    var usersWithOrders = db.Users
                        .Include(u => u.Orders)  // Ключевой момент: EAGER загрузка
                        .ToList();

                    Console.WriteLine("\n3. Результаты в требуемом формате:");
                    Console.WriteLine("=========================================");

                    foreach (var user in usersWithOrders)
                    {
                        Console.WriteLine($"\nUser: {user.Username}");
                        Console.WriteLine("Orders:");

                        if (user.Orders != null && user.Orders.Any())
                        {
                            foreach (var order in user.Orders)
                            {
                                Console.WriteLine($"- Product: {order.ProductName}, Count: {order.ProductCount}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("  No orders");
                        }
                    }

                    Console.WriteLine("\n=========================================");

                    // Демонстрация LAZY загрузки
                    Console.WriteLine("\n4. Демонстрация LAZY загрузки (по умолчанию):");
                    Console.WriteLine("   Получаем пользователя БЕЗ заказов...");

                    var userLazy = db.Users.First();
                    Console.WriteLine($"   Пользователь: {userLazy.Username}");
                    Console.WriteLine("   Обращаемся к заказам (здесь будет выполнен дополнительный SQL запрос)...");

                    // При обращении к Orders выполнится отдельный запрос
                    var orderCount = userLazy.Orders?.Count ?? 0;
                    Console.WriteLine($"   Количество заказов: {orderCount}");

                    // Статистика
                    Console.WriteLine("\n5. Статистика:");
                    Console.WriteLine($"   Всего пользователей: {db.Users.Count()}");
                    Console.WriteLine($"   Всего заказов: {db.Orders.Count()}");
                }

                Console.WriteLine("\n✅ ЛАБОРАТОРНАЯ РАБОТА ВЫПОЛНЕНА УСПЕШНО!");
                Console.WriteLine("\nВсе SQL запросы (создание таблиц, вставка данных, выборка)");
                Console.WriteLine("были выведены выше в консоль, как и требовалось.");

                // Ответы на вопросы
                Console.WriteLine("\n=== Краткие ответы на вопросы ===");
                Console.WriteLine("1. ORM - отображение объектов на таблицы БД");
                Console.WriteLine("2. Импеданс - несоответствие ООП и реляционных моделей");
                Console.WriteLine("3. Состояния сущностей: Detached, Unchanged, Added, Modified, Deleted");
                Console.WriteLine("4. Lazy загрузка - данные загружаются при первом обращении");
                Console.WriteLine("5. Проблема N+1 - 1 запрос для списка + N запросов для связанных данных");
                Console.WriteLine("6. CRUD - Create, Read, Update, Delete операции");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ОШИБКА: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Детали: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}