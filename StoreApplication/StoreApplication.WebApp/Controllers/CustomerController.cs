using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreApplication.DataModel.Repositories;
using StoreApplication.DataModel;

namespace StoreApplication.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepository _customerRepo;
        private readonly DbContextOptions<Project0DBContext> _contextOptions;

        public CustomerController(DbContextOptions<Project0DBContext> context)
        {
            _customerRepo = new CustomerRepository(context);
            _contextOptions = context;
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

        public ActionResult SearchByNameForm()
        {
            return View();
        }

        public ActionResult SearchByNameResult(string SearchPhrase, string SearchPhrase2)
        {
            using var context = new Project0DBContext(_contextOptions);

            return View("Index", context.Customers.Where(c => c.FirstName.Contains(SearchPhrase) && c.LastName.Contains(SearchPhrase2)).ToList());
        }

    }
}
