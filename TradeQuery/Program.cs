using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

/// <summary>
/// Есть таблица хранящая линии покупки: Sales: Id, ProductId, CustomerId, DateCreated.
/// Мы хотим понять, через какие продукты клиенты «попадают» к нам в магазин.
/// Напишите запрос, который выводит продукт и количество случаев, когда он был первой покупкой клиента.
/// ----------------------------------------------------------------------------------------------------
/// Решение написано с помошью linq (преобразуется в один sql запрос)
/// </summary>

namespace TradeQuery
{
    class Program
    {
        class Sale
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int CustomerId { get; set; }
            public DateTime DateCreated { get; set; }
        }

        class Storage : DbContext
        {
            public Storage() : base("Storage") { }
            public DbSet<Sale> Sales { get; set; }
        }

        static void Main(string[] args)
        {
            //Отчистка базы данных (общий сброс)
            Database.SetInitializer<Storage>(new DropCreateDatabaseAlways<Storage>());

            //Исходные данные
            var source = new List<Sale>()
            {
                new Sale() { ProductId = 1, CustomerId = 1, DateCreated = new DateTime(2000, 1, 1)},
                new Sale() { ProductId = 2, CustomerId = 1, DateCreated = new DateTime(2001, 1, 1)},
                new Sale() { ProductId = 1, CustomerId = 1, DateCreated = new DateTime(2002, 1, 1)},

                new Sale() { ProductId = 2, CustomerId = 2, DateCreated = new DateTime(2007, 1, 1)},
                new Sale() { ProductId = 3, CustomerId = 2, DateCreated = new DateTime(2003, 1, 1)},
                new Sale() { ProductId = 3, CustomerId = 2, DateCreated = new DateTime(2005, 1, 1)},
            };
            
            //Открываем контекст для работы с бд
            using (var db = new Storage())
            {
                //Инициализация данных
                source.ForEach(x => db.Sales.Add(x));
                db.SaveChanges();

                //Первые покупки клиентов
                var q = db.Sales.GroupBy(x => x.CustomerId).Select(x => x.OrderBy(y => y.DateCreated).FirstOrDefault());

                //Товары которые хотя бы один раз были первой покупкой клиента
                var first = q.GroupBy(x => x.ProductId).Select(x => new { ProductId = x.Key, Count = x.Count() });

                //Все тавары (с нулевым счётчиком первых покупок)
                var all = db.Sales.GroupBy(x => x.ProductId).Select(x => new { ProductId = x.Key, Count = 0 });

                //Все товары с числом первых покупок 
                var result = all.Union(first).GroupBy(x => x.ProductId).Select(x => x.OrderByDescending(y => y.Count).FirstOrDefault());

                //Вывод результата
                foreach (var x in result.OrderBy(x => x.ProductId).ToList())
                {
                    var msg = String.Format("ProductId: {0}, Count: {1}", x.ProductId, x.Count);
                    Console.WriteLine(msg);
                }
            }


            Console.ReadKey();
        }
    }
}
