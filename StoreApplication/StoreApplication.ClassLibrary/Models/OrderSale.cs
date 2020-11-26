using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary.Models
{
    public class OrderSale
    {
        public int ProductId { get; set; }
        public string ProductName { get; }
        public decimal SalePrice { get;  }
        public int Quantity { get;  }

        public OrderSale()
        {

        }

        public OrderSale(int productId, string productName, decimal salePrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            SalePrice = salePrice;
            Quantity = quantity;
        }
    }

}
