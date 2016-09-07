using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelSort
{
    public class TravelCard
    {
        /// <summary>
        /// Пункт отправления
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string To { get; set; }
        
        /// <summary>
        /// Сортировка (согласно заданию в тесте)
        /// </summary>
        /// <param name="cards">Карточки</param>
        /// <returns>Отсортированнаые карточки</returns>
        public static IList<TravelCard> Sort(IList<TravelCard> cards)
        {
            if (cards == null)
                throw new ArgumentNullException(nameof(cards));

            if (cards.Count == 0)
                return new List<TravelCard>();
            
            var source = new List<TravelCard>(cards);
            var result = new List<TravelCard>(cards.Count);

            //Опорный элемент
            result.Add(cards.First());

            int counter = 0;
            while (result.Count < cards.Count && counter < cards.Count)
            {
                counter++;

                //Поиск связей справа
                var indexR = source.FindIndex(x => x.From.Equals(result.Last().To));
                if (indexR > 0)
                {
                    result.Add(source[indexR]);
                    source.RemoveAt(indexR);
                }

                //Поиск связей cлева
                var indexL = source.FindIndex(x => x.To.Equals(result.First().From));
                if (indexL > 0)
                {
                    result.Insert(0, source[indexL]);
                    source.RemoveAt(indexL);
                }
            }

            if (result.Count != cards.Count)
                throw new ArgumentException();

            return result;
        }

        public override string ToString()
        {
            return String.Format("Путь из: {0} в: {1}", From, To);
        }
    }
}
