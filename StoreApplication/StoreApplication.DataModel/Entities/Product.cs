using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataModel
{
    public partial class Product
    {
        public Product()
        {
            OrderSales = new HashSet<OrderSale>();
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OrderLimit { get; set; }

        public virtual ICollection<OrderSale> OrderSales { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
