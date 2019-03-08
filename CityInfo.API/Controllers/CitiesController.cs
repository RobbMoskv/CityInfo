using CityInfo.Infrastructure.Services;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description,
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest)
        {
            /// Find city
            var CityEntity = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (CityEntity == null)
            {
                return NotFound();
            }

            /// In case points of interest for a city are requested
            if (includePointsOfInterest)
            {
                var cityResult = new CityDto()
                {
                    Id = CityEntity.Id,
                    Name = CityEntity.Name,
                    Description = CityEntity.Description
                };

                foreach (var poi in CityEntity.PointsOfInterest)
                {
                    cityResult.PointsOfInterest.Add(
                        new PointOfInterestDto
                        {
                            Id = poi.Id,
                            Name = poi.Name,
                            Description = poi.Description,
                        }
                    );
                }

                return Ok(cityResult);
            }

            /// Otherwise return just the city
            var cityWithoutPointsOfInterestResult = new CityWithoutPointsOfInterestDto
            {
                Id = CityEntity.Id,
                Name = CityEntity.Name,
                Description = CityEntity.Description,
            };

            return Ok(cityWithoutPointsOfInterestResult);

        }
    }
}
