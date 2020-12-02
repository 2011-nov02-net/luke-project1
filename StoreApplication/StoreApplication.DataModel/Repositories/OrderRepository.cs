using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApplication.DataModel.Repositories
{
    public class OrderRepository
    {
        private readonly DbContextOptions<Project0DBContext> _contextOptions;

        public OrderRepository(DbContextOptions<Project0DBContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public List<Order> GetOrders()
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrders = context.Orders.ToList();

            var appOrders = dbOrders.Select(o => new Order()
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                LocationId = o.LocationId,
                Total = o.Total,
                OrderDate = o.OrderDate,
            }).ToList();

            return appOrders;
        }

        public IEnumerable<Order> GetLocationOrders(int locationId)
        {    
            using var context = new Project0DBContext(_contextOptions);

            return context.Orders.Where(o => o.Location.LocationId == locationId);
        }

        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            using var context = new Project0DBContext(_contextOptions);

            return context.Orders.Where(o => o.Customer.CustomerId == customerId);
        }

        public List<OrderSale> GetOrderDetail(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrderSales = context.OrderSales
                .Include(o => o.Product)
                .Include(o => o.Order)
                .Where(o => o.OrderId == orderId);

            var productList = new List<OrderSale>();
            foreach (var product in dbOrderSales)
            {
                productList.Add(product);
            }

            return productList;
        }

        public void InsertOrder(Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = new Order()
            {
                CustomerId = order.CustomerId,
                LocationId = order.LocationId,
                OrderDate = order.OrderDate,
                Total = order.Total,
            };
            context.Orders.Add(dbOrder);

            context.SaveChanges();

            foreach (var orderSale in order.OrderSales)
            {
                orderSale.OrderId = dbOrder.OrderId;
                AddProductToOrder(orderSale);
                
            }

            context.SaveChanges();
        }

        public void AddProductToOrder(OrderSale orderSale)
        {
            using var context = new Project0DBContext(_contextOptions);

            var newOrderSale = new OrderSale()
            {
                OrderId = orderSale.OrderId,
                ProductId = orderSale.ProductId,
                ProductName = orderSale.ProductName,
                Quantity = orderSale.Quantity,
                SalePrice = orderSale.SalePrice
            };

            context.OrderSales.Add(newOrderSale);

            context.SaveChanges();
        }
    }

}
