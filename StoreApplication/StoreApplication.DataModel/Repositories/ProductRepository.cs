using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApplication.DataModel.Repositories
{
    public class ProductRepository
    {
        private readonly DbContextOptions<Project0DBContext> _contextOptions;

        public ProductRepository(DbContextOptions<Project0DBContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public IEnumerable<ClassLibrary.Models.Product> GetProducts()
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbProducts = context.Products.ToList();

            var appProducts = dbProducts.Select(p => new ClassLibrary.Models.Product()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                OrderLimit = p.OrderLimit
            }).ToList();

            return appProducts;

        }
    }
}
