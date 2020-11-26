using Microsoft.AspNetCore.Mvc;
using StoreApplication.DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepository _orderRepo;

        public OrderController(OrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassLibrary.Models.Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepo.InsertOrder(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public ActionResult GetByLocation(int locationId)
        {
            var locOrderList = _orderRepo.GetLocationOrders(locationId);
            return View(locOrderList);
        }
    }
}
