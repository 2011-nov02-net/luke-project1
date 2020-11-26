using Microsoft.AspNetCore.Mvc;
using StoreApplication.DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository _productRepo;

        public ProductController(ProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public IActionResult Index()
        {
            var prodList = _productRepo.GetProducts();
            return View(prodList);
        }
    }
}
