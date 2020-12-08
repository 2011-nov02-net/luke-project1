using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary.Models
{
    public class StoreInventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Location Location { get; set; }
        public Product Product { get; set; }

        public StoreInventory(int locationId, int productId, int quantity)
        {
            LocationId = locationId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
