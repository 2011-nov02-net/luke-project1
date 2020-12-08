using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StoreApplication.DataModel.Repositories
{
    public class LocationRepository
    {
        private readonly DbContextOptions<Project0DBContext> _contextOptions;

        public LocationRepository(DbContextOptions<Project0DBContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public List<Location> GetLocations()
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbLocations = context.Locations.ToList();

            var appLocations = dbLocations.Select(l => new Location()
            {
                LocationId = l.LocationId,
                Name = l.Name
            }).ToList();


            return appLocations;
        }

        public ClassLibrary.Location GetLocationByName(string name)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbLocations = context.Locations.ToList();

            var appLocations = dbLocations.Select(l => new ClassLibrary.Location()
            {
                LocationId = l.LocationId,
                Name = l.Name
            }).Where(l => l.Name == name).First();

            return appLocations;
        }

        public Location GetLocationByID(int locationId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbLocations = context.Locations.ToList();

            var appLocations = dbLocations.Select(l => new Location()
            {
                LocationId = l.LocationId,
                Name = l.Name
            }).Where(l => l.LocationId == locationId).First();

            return appLocations;

                
        }

        public List<StoreInventory> GetInventory(int locationId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbInventory = context.StoreInventories
                .Include(l => l.Location)
                .Include(p => p.Product)
                .Where(i => i.LocationId == locationId);

            List<StoreInventory> inventories = new List<StoreInventory>();

            foreach (var inventory in dbInventory)
            {
                inventories.Add(inventory);
            }

            return inventories;
        }

        public List<ClassLibrary.Models.StoreInventory> GetStoreInventories(ClassLibrary.Location location)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbInventory = context.StoreInventories.Where(i => i.LocationId == location.LocationId).Include(p => p.Product).ToList();

            var appInventory = new List<ClassLibrary.Models.StoreInventory>();

            foreach (var product in dbInventory)
            {
                var newProduct = new ClassLibrary.Models.StoreInventory(product.LocationId, product.ProductId, product.Quantity)
                {
                    ProductId = product.ProductId
                };
                appInventory.Add(newProduct);
            }

            return appInventory;
        }

        
        
    }
}
