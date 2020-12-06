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

        public IEnumerable<DataModel.Order> GetLocationOrders(int locationId)
        {    
            using var context = new Project0DBContext(_contextOptions);

            var dbOrders = context.Orders.Include(o => o.Customer).Include(o => o.Location)
                .Where(o => o.LocationId == locationId);

            var appOrders = new List<DataModel.Order>();

            foreach (var order in dbOrders)
            {
                appOrders.Add(order);
            }

            return appOrders;
        }

        public IEnumerable<ClassLibrary.Models.Order> GetCustomerOrders(int customerId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrders = context.Orders.ToList();

            var appOrders = dbOrders.Select(o => new ClassLibrary.Models.Order()
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                LocationId = o.LocationId,
                Total = o.Total,
                OrderDate = o.OrderDate,
            }).Where(o => o.CustomerId == customerId).ToList();

            return appOrders;

        }

        public ClassLibrary.Models.Order GetOrderDetail(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.Orders.Where(o => o.OrderId == orderId).First();

            var appOrder =  new ClassLibrary.Models.Order()
            {
                OrderId = dbOrder.OrderId,
                CustomerId = dbOrder.CustomerId,
                LocationId = dbOrder.LocationId,
                OrderDate = dbOrder.OrderDate,
                Total = dbOrder.Total
            };

            //var orderSales = GetOrderProducts(appOrder);
            //foreach (var product in orderSales)
            //{
            //    appOrder.orderSales.Add(product);
            //}

            return appOrder;
        }

        public List<ClassLibrary.Models.OrderSale> GetOrderProducts(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrderSales = context.OrderSales.Where(o => o.OrderId == order.OrderId).Include(o => o.Product).ToList();

            var appOrderSales = new List<ClassLibrary.Models.OrderSale>();

            foreach(var item in dbOrderSales)
            {
                var orderSale = new DataModel.Product()
                {
                    ProductId = item.Product.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                };

                var newOrderSale = new ClassLibrary.Models.OrderSale(item.ProductId, item.ProductName, item.SalePrice, item.Quantity);

                newOrderSale.ProductId = orderSale.ProductId;
                appOrderSales.Add(newOrderSale);
            }
            return appOrderSales;
        }

        public void InsertOrder(ClassLibrary.Models.Order order)
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

        public void DeleteOrder(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.Orders.Where(i => i.OrderId == order.OrderId).First();

            context.Remove(dbOrder);

            context.SaveChanges();

        }
    }

}
