using CityInfo.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Infrastructure.Services
{
    public interface ICityInfoRepository
    {
        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns></returns>
        IEnumerable<City> GetCities();

        /// <summary>
        /// Get a specific city
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="includePointsOfInterest"></param>
        /// <returns></returns>
        City GetCity(int cityId, bool includePointsOfInterest);

        /// <summary>
        /// Get all point of interest for a specific city
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        /// <summary>
        /// Get a specific point of interest of a specific city
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pointOfInterestId"></param>
        /// <returns></returns>
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        /// <summary>
        /// Verify if a city exists.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        bool CityExists(int cityId);

        /// <summary>
        /// Adds a specific Point of interest to a defined city
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pointOfInterest"></param>
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        /// <summary>
        /// Delete a point of interest
        /// </summary>
        /// <param name="pointOfInterest"></param>
        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        /// <summary>
        /// Return true if successfully saved.
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}
