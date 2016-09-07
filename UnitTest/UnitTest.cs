using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

using TravelSort;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestLink()
        {
            var source = new List<TravelCard>()
            {
                new TravelCard() { From = "c0", To = "c1"},
                new TravelCard() { From = "c1", To = "c2"},
                new TravelCard() { From = "c5", To = "c6"},
                new TravelCard() { From = "c2", To = "c3"},
                new TravelCard() { From = "c6", To = "c7"},
                new TravelCard() { From = "c3", To = "c4"},
                new TravelCard() { From = "c4", To = "c5"},
                new TravelCard() { From = "c8", To = "c9"},
                new TravelCard() { From = "c7", To = "c8"}
            };

            //выполняем сортировку (тест)
            var result = TravelCard.Sort(source);

            //проверяем коректность "ссылок"
            //for (int i = 1; i < result.Count - 1; ++i)
            //{
            //    Assert.IsTrue(result[i].To.Equals(result[i + 1].From));
            //    Assert.IsTrue(result[i].From.Equals(result[i - 1].To));
            //}   

            var sorted = new List<TravelCard>()
            {
                new TravelCard() { From = "c0", To = "c1"},
                new TravelCard() { From = "c1", To = "c2"},
                new TravelCard() { From = "c2", To = "c3"},
                new TravelCard() { From = "c3", To = "c4"},
                new TravelCard() { From = "c4", To = "c5"},
                new TravelCard() { From = "c5", To = "c6"},
                new TravelCard() { From = "c6", To = "c7"},
                new TravelCard() { From = "c7", To = "c8"},
                new TravelCard() { From = "c8", To = "c9"},
            };

            Assert.IsTrue(sorted.Count == result.Count);

            for (int i = 0; i < sorted.Count; ++i)
            {
                Assert.IsTrue(sorted[i].From.Equals(result[i].From));
                Assert.IsTrue(sorted[i].To.Equals(result[i].To));
            }
        }
    }
}
