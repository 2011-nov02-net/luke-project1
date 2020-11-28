using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary.Models
{
    public class OrderSale
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal SalePrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

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
