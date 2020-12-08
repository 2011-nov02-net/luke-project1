using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Models
{
    public class OrderSale
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal SalePrice { get; set; }

        public virtual Order Order { get; set; }
        public List<DataModel.Product> Products{ get; set; }


        public OrderSale()
        {
            Products = new List<DataModel.Product>();
        }

        public OrderSale(int orderId, int productId, string productName, int salePrice, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            SalePrice = salePrice;
            Quantity = quantity;
            Products = new List<DataModel.Product>();
        }
    }
}
