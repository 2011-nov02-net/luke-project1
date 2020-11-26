using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary.Models
{
    public class Inventory
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product product { get; }

        public Inventory()
        {

        }

        public Inventory(int quantity, Product prod)
        {
            Quantity = quantity;
            product = prod;
        }
    }
}
