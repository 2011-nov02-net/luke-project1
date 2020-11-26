using Microsoft.AspNetCore.Mvc;
using StoreApplication.DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Controllers
{
    public class LocationController : Controller
    {
        private LocationRepository _locationRepo;

        public LocationController(LocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        //GET - Location
        public IActionResult Index()
        {
            var locList = _locationRepo.GetLocations();
            return View(locList);
        }

        //GET - CREATE
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //POST - CREATE
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ClassLibrary.Location location)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _locationRepo.InsertLocation(location);
        //        return RedirectToAction("Index");
        //    }
        //    return View(location);
        //}


    }
}
