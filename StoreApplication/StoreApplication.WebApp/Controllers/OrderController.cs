using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApplication.DataModel;
using StoreApplication.DataModel.Repositories;
using StoreApplication.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepository _orderRepo;
        private CustomerRepository _custRepo;
        private LocationRepository _locRepo;
        private ProductRepository _prodRepo;

        public OrderController(DbContextOptions<Project0DBContext> context)
        {
            _orderRepo = new OrderRepository(context);
            _custRepo = new CustomerRepository(context);
            _locRepo = new LocationRepository(context);
            _prodRepo = new ProductRepository(context);
        }

        //GET - Order
        public IActionResult Index()
        {
            var orderList = _orderRepo.GetOrders();
            return View(orderList);
        }

        //GET - CREATE
        public ActionResult Create()
        {
            var newOrder = new WebApp.Models.Order();
            var allCustomers = _custRepo.GetCustomers();
            var allLocations = _locRepo.GetLocations();
            var allProducts = _prodRepo.GetProducts();

            foreach (var customer in allCustomers)
            {
                newOrder.AllCustomers.Add(customer);
            }
            foreach (var location in allLocations)
            {
                newOrder.AllLocations.Add(location);
            }
            foreach (var product in allProducts)
            {
                newOrder.AllProducts.Add(product);
            }
            newOrder.Total = 0;
            return View(newOrder);
        }

        // POST - Create a new Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebApp.Models.Order order)
        {
            var customerChoice = _custRepo.GetCustomerByID(order.CustomerId);
            var locationChoice = _locRepo.GetLocationByID(order.LocationId);
            var newOrder = new DataModel.Order();

            newOrder.Customer = customerChoice;
            newOrder.Location = locationChoice;
            newOrder.OrderDate = DateTime.Now;
            newOrder.Total = order.Total;

            _orderRepo.InsertOrder(newOrder);

            return RedirectToAction("Index");
            
        }

       // GET - Order Detail
        public ActionResult Detail(int id)
        {
            var orderDetail = _orderRepo.GetOrderDetail(id);
            return View(orderDetail);

        }

        // GET - Orders By Location
        public ActionResult GetByLocation(int locationId)
        {
            var locOrderList = _orderRepo.GetLocationOrders(locationId);
            return View(locOrderList);
        }

        // GET - Orders By Customer
        public ActionResult GetByCustomer(int customerId)
        {
            var custOrderList = _orderRepo.GetCustomerOrders(customerId);
            return View(custOrderList);
        }

    }
}
