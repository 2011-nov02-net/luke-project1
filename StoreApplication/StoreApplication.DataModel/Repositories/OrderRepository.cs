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

        public Order GetOrderById(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.Orders.Where(o => o.OrderId == orderId).First();

            return dbOrder;
        }

        public OrderSale GetOrderSaleById(int id)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrderSale = context.OrderSales.ToList();

            var appOrderSale = dbOrderSale.Select(o => new OrderSale()
            {
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                ProductName = o.ProductName,
                Quantity = o.Quantity,
                SalePrice = o.SalePrice
            }).Where(p => p.ProductId == id).FirstOrDefault();

            return appOrderSale;
        }

        public List<OrderSale> GetOrderDetail(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.OrderSales.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == orderId);

            var allProducts = new List<OrderSale>();

            foreach (var product in dbOrder)
            {
                allProducts.Add(product);
            }

            return allProducts;
        }

        public List<ClassLibrary.Models.OrderSale> GetOrderProducts(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrderSales = context.OrderSales.Where(o => o.OrderId == order.OrderId).Include(o => o.Product).ToList();

            var appOrderSales = new List<ClassLibrary.Models.OrderSale>();

            foreach(var item in dbOrderSales)
            {
                var orderSale = new Product()
                {
                    ProductId = item.Product.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                };

                var newOrderSale = new ClassLibrary.Models.OrderSale(item.OrderId, item.ProductName, item.SalePrice, item.Quantity);

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

            foreach(var orderSale in dbOrder.OrderSales)
            {
                orderSale.OrderId = dbOrder.OrderId;
            }

            context.SaveChanges();

            
        }

        public Order CreateAndReturnOrder(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);
            var dbOrder = new Order()
            {
                CustomerId = order.CustomerId,
                LocationId = order.LocationId,
                OrderDate = order.OrderDate,
                Total = order.Total
            };
            context.Orders.Add(dbOrder);
            context.SaveChanges();
            
            return GetOrderById(dbOrder.OrderId);
        }
    

        public void AddProductToOrder(ClassLibrary.Models.OrderSale orderSale)
        {
            using var context = new Project0DBContext(_contextOptions);


            var newOrderSale = new OrderSale()
            {
                ProductId = orderSale.ProductId,
                ProductName = orderSale.ProductName,
                Quantity = orderSale.Quantity,
                SalePrice = orderSale.SalePrice
            };

            context.OrderSales.Add(newOrderSale);

            context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = context.Orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault();

            dbOrder.Total = order.Total;

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
