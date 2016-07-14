using System;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class ShoppingCartTests
    {
        private Product[] _products = {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            // arrange
            // Setup the mock object
            var discountMockObj = new Mock<IDiscountHelper>();
            // Insert behavior for the object. In this case, it is passing in a method.
            discountMockObj.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                           .Returns<decimal>(totalInput => totalInput);

            var target = new LinqValueCalc(discountMockObj.Object);
            var goalTotal = _products.Sum(e => e.Price);

            // act
            var result = target.ValueProducts(_products);

            // assert
            Assert.AreEqual(goalTotal, result);
        }

        private Product[] CreateProduct(decimal value)
        {
            return new[] {new Product() {Price = value}};
        }

        [TestMethod]
        [ExpectedException(typeof (System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            // arrange
            var mock = new Mock<IDiscountHelper>();

            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
                .Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                .Returns<decimal>(total => (total * 0.9M));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100,
                Range.Inclusive))).Returns<decimal>(total => total - 5);

            var target = new LinqValueCalc(mock.Object);

            // act
            decimal fiveDollarDiscount = target.ValueProducts(CreateProduct(5));
            decimal tenDollarDiscount = target.ValueProducts(CreateProduct(10));
            decimal fiftyDollarDiscount = target.ValueProducts(CreateProduct(50));
            decimal hundredDollarDiscount = target.ValueProducts(CreateProduct(100));
            decimal fiveHundredDollarDiscount = target.ValueProducts(CreateProduct(500));

            // assert
            Assert.AreEqual(5, fiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, tenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, fiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, hundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, fiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(CreateProduct(0));

        }

    }
}
