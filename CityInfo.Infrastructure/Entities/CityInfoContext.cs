using System;
using System.Collections.Generic;
using System.Text;
using Microsoft;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Infrastructure.Entities
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    }
}
