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

    }
}
