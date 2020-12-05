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
        private readonly DbContextOptions<Project0DBContext> _contextOptions;

        public OrderController(DbContextOptions<Project0DBContext> context)
        {
            _orderRepo = new OrderRepository(context);
            _custRepo = new CustomerRepository(context);
            _locRepo = new LocationRepository(context);
            _prodRepo = new ProductRepository(context);
            _contextOptions = context;
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
            newOrder.Total = (decimal)5.00;
            return View(newOrder);
        }

        // POST - Create a new Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataModel.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            if(ModelState.IsValid)
            {
                //var customerChoice = _custRepo.GetCustomerByID(order.CustomerId);
                //var locationChoice = _locRepo.GetLocationByID(order.LocationId);
                //var newOrder = new ClassLibrary.Models.Order();

                //newOrder.Customer = customerChoice;
                //newOrder.Location = locationChoice;
                //newOrder.OrderDate = DateTime.Now;
                //newOrder.Total = order.Total;
                

                _orderRepo.InsertOrder(order);

                return RedirectToAction("Index");
            }
            return View();            
        }


        public ActionResult AddProducts(int orderId)
        {
            var orderSale = new WebApp.Models.OrderSale();
            orderSale.OrderId = orderId;
            orderSale.Quantity += 1;
            orderSale.SalePrice = 0;


            var allProducts = _prodRepo.GetProducts();
            foreach(var product in allProducts)
            {
                orderSale.Products.Add(product);
            }

            return View(orderSale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProducts(ClassLibrary.Models.OrderSale orderSale)
        {
            using var context = new Project0DBContext(_contextOptions);           

            if(ModelState.IsValid)
            {
                _orderRepo.AddProductToOrder(orderSale);
                return RedirectToAction("Detail", context.Orders.Where(o => o.OrderId == orderSale.OrderId));
            }
            return View();
        }

       // GET - Order Detail
        public ActionResult Detail(int id)
        {
            if(ModelState.IsValid)
            {
                var orderDetail = _orderRepo.GetOrderDetail(id);
                return View(orderDetail);
            }
            return View("Index");
            
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

        public ActionResult ShowOrderSearchFormByLocation()
        {
            return View();
        }

        public ActionResult ShowOrderSearchResultByLocation(string searchPhrase)
        {
            using var context = new Project0DBContext(_contextOptions);

            return View("Index",  context.Orders.Where(o => o.Location.Name.Contains(searchPhrase)).ToList());
        }

        public ActionResult ShowOrderSearchResultByCustomer(string searchPhrase)
        {
            using var context = new Project0DBContext(_contextOptions);

            return View("Index", context.Orders.Where(o => o.Customer.FirstName.Contains(searchPhrase) || o.Customer.LastName.Contains(searchPhrase)).ToList());
        }

        public ActionResult ShowOrderSearchFormByCustomer()
        {
            return View();
        }

        public ActionResult Delete(int orderId)
        {
            var orderToDelete = _orderRepo.GetOrderById(orderId);
            return View(orderToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSuccess(int orderId)
        {
            using var context = new Project0DBContext(_contextOptions);

            if(ModelState.IsValid)
            {
                var orderToDelete = _orderRepo.GetOrderById(orderId);
                _orderRepo.DeleteOrder(orderToDelete);

                return RedirectToAction("Index");
            }
            return View("Index");
            

        }

    }
}
