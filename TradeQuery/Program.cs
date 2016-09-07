using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Есть таблица хранящая линии покупки: Sales: Id, ProductId, CustomerId, DateCreated.
/// Мы хотим понять, через какие продукты клиенты «попадают» к нам в магазин.
/// Напишите запрос, который выводит продукт и количество случаев, когда он был первой покупкой клиента.
/// ----------------------------------------------------------------------------------------------------
/// Решение написано с помошью linq, в тестовом примере аналогом базы служит коллекция элементов Sales.
/// </summary>

namespace TradeQuery
{
    class Program
    {
        class Sales
        {
            public int Id;
            public int ProductId;
            public int CustomerId;
            public DateTime DateCreated;
        }

        static void Main(string[] args)
        {
            //Исходные данные
            var table = new List<Sales>()
            {
                new Sales() { ProductId = 1, CustomerId = 1, DateCreated = new DateTime(2000, 1, 1)},
                new Sales() { ProductId = 2, CustomerId = 1, DateCreated = new DateTime(2001, 1, 1)},
                new Sales() { ProductId = 1, CustomerId = 1, DateCreated = new DateTime(2002, 1, 1)},
                new Sales() { ProductId = 2, CustomerId = 2, DateCreated = new DateTime(2007, 1, 1)},
                new Sales() { ProductId = 3, CustomerId = 2, DateCreated = new DateTime(2003, 1, 1)},
                new Sales() { ProductId = 3, CustomerId = 2, DateCreated = new DateTime(2005, 1, 1)},
            };

            //Первые покупки клиентов (список уникальных товаров и число первых покупок)
            var firstProducts = table.OrderBy(x => x.DateCreated).GroupBy(x => x.CustomerId).Select(x => x.First());
            var p1 = firstProducts.GroupBy(x => x.ProductId).Select(x => new { ProductId = x.First().ProductId, Count = x.Count() });

            //Другие покупки клиентов (список уникальных товаров и число первых покупок)
            var otherProducts = table.Where(x => !p1.Any(y => x.ProductId == y.ProductId));
            var p2 = otherProducts.GroupBy(x => x.ProductId).Select(x => new { ProductId = x.First().ProductId, Count = 0 });

            //Объединяем товары и сортируем
            var res = p1.Union(p2).OrderBy(x => x.ProductId);

            //Вывод результата
            foreach (var x in res)
            {
                var msg = String.Format("ProductId: {0}, Count: {1}", x.ProductId, x.Count);
                Console.WriteLine(msg);
            }

            Console.ReadKey();
        }
    }
}
