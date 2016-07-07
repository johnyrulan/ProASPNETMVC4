using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        private readonly IValueCalc _calc;

        public ShoppingCart(IValueCalc calc)
        {
            _calc = calc;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal ProductTotalPrice
        {
            get { return _calc.ValueProducts(this.Products); }
        }

        public decimal CalculateProductTotal()
        {
            return _calc.ValueProducts(this.Products);
        }
    }
}