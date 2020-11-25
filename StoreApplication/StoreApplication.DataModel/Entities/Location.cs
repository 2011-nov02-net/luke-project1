using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataModel
{
    public partial class Location
    {
        public Location()
        {
            Orders = new HashSet<Order>();
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
