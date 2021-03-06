﻿using StoreApplication.DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [DisplayName("Location ID")]
        public int LocationId { get; set; }

        public decimal Total { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayName("Products")]
        public List<DataModel.OrderSale> orderSales { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Location Location { get; set; }

        public List<DataModel.Customer> AllCustomers { get; set; }

        public List<DataModel.Location> AllLocations { get; set; }

        public List<DataModel.Product> AllProducts { get; set; }

        

        public Order()
        {
            AllCustomers = new List<DataModel.Customer>();
            AllLocations = new List<DataModel.Location>();
            AllProducts = new List<DataModel.Product>();
        }

    }
}
