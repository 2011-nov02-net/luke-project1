using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.ClassLibrary
{
    public class Location
    {

        public int LocationId { get; set; }
        public string Name { get; set; }
        public ICollection<Models.Inventory> Inventory { get; }

        public Location()
        {

        }

        public Location(int locationId, string name)
        {
            LocationId = locationId;
            Name = name;
            Inventory = new List<Models.Inventory>();
        }
    }
}
