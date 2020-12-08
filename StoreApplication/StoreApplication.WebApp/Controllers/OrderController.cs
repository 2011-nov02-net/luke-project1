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
            var customers = _custRepo.GetCustomers();
            var locations = _locRepo.GetLocations();
            var newOrder = new WebApp.Models.Order()
            {
                AllCustomers = customers,
                AllLocations = locations
            };

            newOrder.Total = 10;

            
            return View(newOrder);
        }

        // POST - Create a new Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassLibrary.Models.Order order)
        {
            using var context = new Project0DBContext(_contextOptions);

            if(ModelState.IsValid)
            {
                var createOrder = _orderRepo.CreateAndReturnOrder(order);
                return RedirectToAction(nameof(Detail), new { id = createOrder.OrderId });
            }
            return View("Index");            
        }

        public ActionResult AddProducts(int orderId)
        {
            var orderSale = new WebApp.Models.OrderSale
            {
                Quantity = 1,
            };

            orderSale.OrderId = orderId;

            var products = _prodRepo.GetProducts();

            foreach(var product in products)
            {
                
                orderSale.Products.Add(product);
            }

            return View(orderSale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProducts(WebApp.Models.OrderSale orderSale)
        { 

            if(ModelState.IsValid)
            {
                var order = _orderRepo.GetOrderById(orderSale.OrderId);

                var inventory = _locRepo.GetInventory(order.LocationId);

                var product = inventory.Find(p => p.ProductId == orderSale.ProductId);

                if (product.Quantity - orderSale.Quantity < 0)
                {
                    return RedirectToAction(nameof(Detail), new { OrderId = orderSale.OrderId });
                }
                else
                {
                    bool isInOrder = order.OrderSales.Any(p => p.Product.ProductId == product.ProductId);

                    var totalPrice = product.Product.Price * orderSale.Quantity;

                    if (isInOrder)
                    {
                        foreach (var prod in order.OrderSales)
                        {
                            if (prod.Product.ProductId == orderSale.ProductId)
                            {
                                var currentOrder = _orderRepo.GetOrderSaleById(prod.ProductId);
                                currentOrder.Quantity += orderSale.Quantity;
                                currentOrder.SalePrice = (decimal)totalPrice;
                                order.Total = (decimal)totalPrice;

                            }
                        };
                    }
                    else
                    {
                        var newOrderSale = new ClassLibrary.Models.OrderSale(orderSale.ProductId, orderSale.ProductName, (decimal)totalPrice, orderSale.Quantity);
                        order.Total += (decimal)totalPrice;
                        _orderRepo.AddProductToOrder(newOrderSale);
                        _orderRepo.UpdateOrder(order);
                    }
                    product.Quantity -= orderSale.Quantity;
                    return RedirectToAction(nameof(AddProducts), new { id = order.OrderId });
                }
                

                
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

        public ActionResult ShowOrderSearchFormByLocation()
        {
            return View();
        }

        public ActionResult ShowOrderSearchResultByLocation(string searchPhrase)
        {
            using var context = new Project0DBContext(_contextOptions);

            return View("Index",  context.Orders.Where(o => o.Location.Name.Contains(searchPhrase)).ToList());
        }

        public ActionResult ShowOrderSearchResultByCustomer(string searchPhrase, string searchPhrase2)
        {
            using var context = new Project0DBContext(_contextOptions);

            return View("Index", context.Orders.Where(o => o.Customer.FirstName.Contains(searchPhrase) && o.Customer.LastName.Contains(searchPhrase2)).ToList());
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

                return RedirectToAction("Index");
            }
            return View("Index");
            

        }

    }
}
