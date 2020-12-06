using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataModel
{
    public partial class OrderSale
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal SalePrice { get; set; }
        public List<DataModel.Product> Products { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
