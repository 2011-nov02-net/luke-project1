using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataModel
{
    public partial class StoreInventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
