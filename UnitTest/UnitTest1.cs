using BusinessLogicLibrary;
using BusinessLogicLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetTopProducts_PositiveScenario()
        {
            List<Line> lineProducts = new List<Line>
            {
                new Line
                {
                    MerchantProductNo ="TestMerchant-00001",
                    Gtin = "TestGtin-00001",
                    Quantity = 3,
                    Description = "TestDescription1"
                },
                 new Line
                {
                    MerchantProductNo ="TestMerchant-00002",
                    Gtin = "TestGtin-00002",
                    Quantity = 4,
                    Description = "TestDescription2"
                },
                  new Line
                {
                    MerchantProductNo ="TestMerchant-00003",
                    Gtin = "TestGtin-00003",
                    Quantity = 1,
                    Description = "TestDescription3"
                },
                   new Line
                {
                    MerchantProductNo ="TestMerchant-00004",
                    Gtin = "TestGtin-00004",
                    Quantity = 2,
                    Description = "TestDescription4"
                },
                    new Line
                {
                    MerchantProductNo ="TestMerchant-00005",
                    Gtin = "TestGtin-00003",
                    Quantity = 1,
                    Description = "TestDescription5"
                },
                     new Line
                {
                    MerchantProductNo ="TestMerchant-00006",
                    Gtin = "TestGtin-00006",
                    Quantity = 8,
                    Description = "TestDescription6"
                },
                      new Line
                {
                    MerchantProductNo ="TestMerchant-00007",
                    Gtin = "TestGtin-00007",
                    Quantity = 3,
                    Description = "TestDescription3"
                },
                new Line
                {
                    MerchantProductNo ="TestMerchant-00008",
                    Gtin = "TestGtin-00008",
                    Quantity = 2,
                    Description = "TestDescription8"
                }
            };

            BusinessLogic business = new BusinessLogic();
            List<TopProducts> topProducts = business.GetTopProducts(lineProducts, 5);
            Assert.IsNotNull(topProducts);
            Assert.AreEqual(5, topProducts.Count);
        }
    }
}
