﻿using System;
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
        public DateTime OrderDate { get; set; }
        public ICollection<OrderSale> orderSales { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual Location Location { get; set; }

        public Order()
        {

        }

        public Order(int orderId, int customerId, int locationId, decimal total)
        {
            OrderId = orderId;
            CustomerId = customerId;
            LocationId = locationId;
            Total = total;
            OrderDate = DateTime.Now;
        }

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
