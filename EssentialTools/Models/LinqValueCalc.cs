using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalc : IValueCalc
    {
        private readonly IDiscountHelper _discountHelper;

        public LinqValueCalc(IDiscountHelper discountHelper)
        {
            _discountHelper = discountHelper;
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return _discountHelper.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}