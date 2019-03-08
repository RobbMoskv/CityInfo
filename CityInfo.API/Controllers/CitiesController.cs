using CityInfo.Infrastructure.Services;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            #region Approach without AutoMapper
            //var results = new List<CityWithoutPointsOfInterestDto>();

            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = cityEntity.Id,
            //        Name = cityEntity.Name,
            //        Description = cityEntity.Description,
            //    });
            //}
            #endregion

            // Map each item from destination list to the source list
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest)
        {
            /// Find city
            var cityEntity = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (cityEntity == null)
            {
                return NotFound();
            }

            /// In case points of interest for a city are requested
            if (includePointsOfInterest)
            {
                var cityResult = Mapper.Map<CityDto>(cityEntity);
                return Ok(cityResult);
            }

            /// Otherwise return just the city
            var cityWithoutPointsOfInterestResult = Mapper.Map<CityWithoutPointsOfInterestDto>(cityEntity);

            return Ok(cityWithoutPointsOfInterestResult);

        }
    }
}
