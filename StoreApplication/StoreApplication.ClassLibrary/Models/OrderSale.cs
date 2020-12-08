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
        public Product Product { get; set; }
        public List<Product> Products { get; set; }

        public OrderSale()
        {
            Products = new List<Product>();
        }

        public OrderSale(int orderId, string productName, decimal salePrice, int quantity)
        {
            ProductId = orderId;
            ProductName = productName;
            SalePrice = salePrice;
            Quantity = quantity;
            Products = new List<Product>();
        }
    }

}
