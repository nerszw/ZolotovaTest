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
            result.Add(source.First());
            source.RemoveAt(0);

            int indexR = 0, indexL = 0;
            while (indexL != -1 || indexR != -1)
            {
                //Поиск связей справа
                indexR = source.FindIndex(x => x.From.Equals(result.Last().To));
                if (indexR != -1)
                {
                    result.Add(source[indexR]);
                    source.RemoveAt(indexR);
                }

                //Поиск связей cлева
                indexL = source.FindIndex(x => x.To.Equals(result.First().From));
                if (indexL != -1)
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
