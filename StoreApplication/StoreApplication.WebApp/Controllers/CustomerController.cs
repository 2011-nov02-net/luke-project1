using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreApplication.DataModel.Repositories;

namespace StoreApplication.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepository _customerRepo;

        public CustomerController(CustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        //GET - Customer
        public IActionResult Index()
        {
            var custList = _customerRepo.GetCustomers();
            return View(custList);
        }

        //GET - CREATE
        public ActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassLibrary.Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.InsertCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}
