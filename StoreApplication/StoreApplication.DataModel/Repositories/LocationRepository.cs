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

        public List<ClassLibrary.Location> GetLocations()
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbLocations = context.Locations.ToList();

            var appLocations = dbLocations.Select(l => new ClassLibrary.Location()
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

        public ClassLibrary.Location GetLocationByID(int locationId)
        {
            using var context = new Project0DBContext(_contextOptions);

            var dbLocations = context.Locations.ToList();

            var appLocations = dbLocations.Select(l => new ClassLibrary.Location()
            {
                LocationId = l.LocationId,
                Name = l.Name
            }).Where(l => l.LocationId == locationId).First();

            return appLocations;

                
        }

        
        
    }
}
