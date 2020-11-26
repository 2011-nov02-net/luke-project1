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

        public IEnumerable<ClassLibrary.Models.Order> GetOrders()
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
            }).ToList();

            return appOrders;
        }

        public List<ClassLibrary.Models.Order> GetLocationOrders(int locationId)
        {    
            using var context = new Project0DBContext(_contextOptions);

            var dbCustomerOrders = context.Orders.Where(o => o.LocationId == locationId).ToList();


            var appLocationOrders = dbCustomerOrders.Select(co => new ClassLibrary.Models.Order()
            {
                OrderId = co.OrderId,
                CustomerId = co.CustomerId,
                LocationId = co.LocationId,
                Total = co.Total,
                OrderDate = co.OrderDate,
            }).ToList();



            return appLocationOrders;
        }

        public void InsertOrder(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbOrder = new Order()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                LocationId = order.LocationId,
                Total = order.Total,
                OrderDate = order.OrderDate
            };

            context.Orders.Add(dbOrder);

            context.SaveChanges();
        }
    }

}
