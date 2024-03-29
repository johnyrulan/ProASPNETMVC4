﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal DiscountSize { get; set; }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (this.DiscountSize/100m * totalParam));
        }
    }
}