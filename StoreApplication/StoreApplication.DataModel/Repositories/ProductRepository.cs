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

        public List<Product> GetProducts()
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbProducts = context.Products.ToList();

            var appProducts = dbProducts.Select(p => new Product()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                OrderLimit = p.OrderLimit
            }).ToList();

            //var products = new List<ClassLibrary.Models.Product>();
            //foreach(var product in appProducts)
            //{
            //    var addProduct = new ClassLibrary.Models.Product()
            //    {
            //        ProductId = product.ProductId,
            //        Name = product.Name,
            //        Price = product.Price,
            //        OrderLimit = product.OrderLimit
            //    };
            //    products.Add(addProduct);
            //}

            return appProducts;

        }
    }
}
