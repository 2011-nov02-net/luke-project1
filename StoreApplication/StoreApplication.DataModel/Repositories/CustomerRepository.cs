using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace StoreApplication.DataModel.Repositories
{
    public class CustomerRepository
    {
        private readonly DbContextOptions<Project0DBContext> _contextOptions;

        public CustomerRepository(DbContextOptions<Project0DBContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public IEnumerable<ClassLibrary.Models.Customer> GetCustomers()
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbCustomers = context.Customers.ToList();

            var appCustomers = dbCustomers.Select(c => new ClassLibrary.Models.Customer()
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            }).ToList();


            return appCustomers;
        }

        public ClassLibrary.Models.Customer GetCustomerByName(string firstName, string lastName)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbCustomers = context.Customers.ToList();

            var appCustomers = dbCustomers.Select(c => new ClassLibrary.Models.Customer()
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            }).Where(c => c.FirstName == firstName && c.LastName == lastName).First();
            return appCustomers;
        }

        public ClassLibrary.Models.Customer GetCustomerByID(int customerId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbCustomers = context.Customers.ToList();

            var appCustomers = dbCustomers.Select(c => new ClassLibrary.Models.Customer()
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            }).Where(c => c.CustomerId == customerId).First();
            return appCustomers;
        }
        //public List<ClassLibrary.Order> GetCustomerOrders(int customerId)
        //{
        //    using var context = new Project0DBContext(_contextOptions);

        //    var dbCustomerOrders = context.Orders.Where(o => o.CustomerId == customerId).ToList();

        //    var appCustomerOrders = dbCustomerOrders.Select(o => new ClassLibrary.Order()
        //    {
        //        OrderId = o.OrderId,
        //        CustomerId = o.CustomerId,
        //        LocationId = o.LocationId,
        //        OrderTime = o.OrderTime,
        //        Quantity = o.Quantity
        //    }
        //    ).ToList();

        //    return appCustomerOrders;
        //}

        public void InsertCustomer(ClassLibrary.Models.Customer customer)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbCustomer = new Customer()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            context.Customers.Add(dbCustomer);

            context.SaveChanges();
        }
    }
}
