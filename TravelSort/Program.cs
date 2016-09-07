using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelSort
{
    class Program
    {
        /// <summary>
        /// Тестовый пример
        /// </summary>
        static void Main(string[] args)
        {
            var source = new List<TravelCard>()
            {
                new TravelCard() { From = "c0", To = "c1"},
                new TravelCard() { From = "c1", To = "c2"},
                new TravelCard() { From = "c2", To = "c3"},
                new TravelCard() { From = "c3", To = "c4"},
                new TravelCard() { From = "c4", To = "c5"},
                new TravelCard() { From = "c5", To = "c6"},
                new TravelCard() { From = "c6", To = "c7"},
                new TravelCard() { From = "c7", To = "c8"},
                new TravelCard() { From = "c8", To = "c9"}
            };

            //случайно перемешиваем элементы
            source = source.OrderBy(a => Guid.NewGuid()).Take(source.Count).ToList();
            
            Console.WriteLine("Исходные данные:");
            foreach (var card in source)
            {
                Console.WriteLine(card);
            }
            
            //выполняем сортировку (тест)
            var result = TravelCard.Sort(source);

            Console.WriteLine("Результат:");
            foreach (var card in result)
            {
                Console.WriteLine(card);
            }

            Console.ReadKey();
        }
    }
}
