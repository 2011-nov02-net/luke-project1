using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OrderLimit { get; set; }

        public Product(int productId, string name, decimal price, int orderLimit)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            OrderLimit = orderLimit;
        }
    }
}
