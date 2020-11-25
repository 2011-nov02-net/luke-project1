using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; }
        public List<OrderSale> orderSales { get; set; }

        public Order(int orderId, int customerId, int locationId, decimal total, DateTime orderDate, List<OrderSale> orderSale)
        {
            OrderId = orderId;
            CustomerId = customerId;
            LocationId = locationId;
            Total = total;
            OrderDate = orderDate;
            orderSales = orderSale;
        }
    }

}
