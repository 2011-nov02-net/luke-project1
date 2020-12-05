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
        private CustomerRepository _custRepo;
        private LocationRepository _locRepo;

        public OrderRepository(DbContextOptions<Project0DBContext> contextOptions)
        {
            _contextOptions = contextOptions;
            _locRepo = new LocationRepository(contextOptions);
            _custRepo = new CustomerRepository(contextOptions);
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

        public ClassLibrary.Models.Order GetOrderById(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.Orders.ToList();

            var appOrder = dbOrder.Select(o => new ClassLibrary.Models.Order()
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                LocationId = o.LocationId,
                OrderDate = o.OrderDate,
                Total = o.Total
            }).Where(o => o.OrderId == orderId).FirstOrDefault();

            return appOrder;
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

        public ClassLibrary.Models.OrderSale GetOrderDetail(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrderSale = context.OrderSales.ToList();

            var appOrder = dbOrderSale.Select(o => new ClassLibrary.Models.OrderSale()
            {
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                ProductName = o.ProductName,
                Quantity = o.Quantity,
                SalePrice = o.SalePrice
            }).Where(o => o.OrderId == orderId).First();


            //var productList = new List<OrderSale>();
            //foreach (var product in dbOrderSales)
            //{
            //    productList.Add(product);
            //}

            //return productList;
            return appOrder;
        }

        public void InsertOrder(DataModel.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = new DataModel.Order()
            {
                CustomerId = order.CustomerId,
                LocationId = order.LocationId,
                OrderDate = order.OrderDate,
                Total = order.Total,
                OrderSales = new List<DataModel.OrderSale>()
            };
            

            context.Orders.Add(dbOrder);

            context.SaveChanges();

            
        }

        public void AddProductToOrder(ClassLibrary.Models.OrderSale orderSale)
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

        public void DeleteOrder(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.Orders.Where(i => i.OrderId == order.OrderId).First();

            context.Remove(dbOrder);

            context.SaveChanges();

        }
    }

}
