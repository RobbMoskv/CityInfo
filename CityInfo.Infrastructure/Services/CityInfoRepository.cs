using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Infrastructure.Context;
using CityInfo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Infrastructure.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        /// Get all cities
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        /// Get a specific city
        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }

            return _context.Cities
                .Where(c => c.Id == cityId).FirstOrDefault();
        }

        /// Get all point of interest for a specific city
        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToList();
        }

        /// Get a specific point of interest of a specific city
        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();
        }

        /// Verify if a city exists.
        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

    }
}
