using CityInfo.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Infrastructure.Services
{
    public interface ICityInfoRepository
    {
        // Get all cities
        IEnumerable<City> GetCities();

        // Get a specific city
        City GetCity(int cityId, bool includePointsOfInterest);

        // Get all point of interest for a specific city
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        // Get a specific point of interest of a specific city
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

    }
}
