using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Inventory> Inventory { get; }
    }
}
