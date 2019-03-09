using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using CityInfo.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        /// Instances for injected services
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;

        /// Inject the Logger Instance via DI in the constructor
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        /// <summary>
        /// Get all point of interest of a specific city
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                // Verify id specific City even exists
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with ID #{cityId} was not found!.");
                    return NotFound();
                }

                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                // Map each item from destination list to the source list
                var pointsOfInterestForCityResults = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);

                return Ok(pointsOfInterestForCityResults);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to load city with ID #{cityId}!.", ex);
                return StatusCode(500, "A problem occured during your request.");
            }

        }

        /// <summary>
        /// Get a specific point of interest of a city.
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{cityId}/PointsOfInterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointsofInterest(int cityId, int id)
        {
            // Verify id specific City even exists
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestForCity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestForCity == null)
            {
                return NotFound();
            }
            // Map each item from destination list to the source list
            var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterestForCity);

            return Ok(pointOfInterestResult);
        }

        /// <summary>
        /// Create a point of interest for a specific city.
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pointOfInterest"></param>
        /// <returns></returns>
        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<Infrastructure.Entities.PointOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while hanlding your request.");
            }
            // Return point of interest for display
            var createdPointOfInterestToReturn = Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, id = createdPointOfInterestToReturn.Id },
                createdPointOfInterestToReturn);

        }

        /// <summary>
        /// Update a point of interest of a specific city
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="id"></param>
        /// <param name="pointsOfInterest"></param>
        /// <returns></returns>
        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            // Create own error model
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The please do not use just the name as description.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if related city exists
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            // Check if specified point of interest exists.
            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // Update all content (put)
            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            // Save
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while hanlding your request.");
            }

            // Default response for update requests
            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartialUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            // Check if related city exists
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            // Check if specified point of interest exists.
            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // 
            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Update all content (Source, Destination)
            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            // Save
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while hanlding your request.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            // Check if related city exists
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // Check if specified point of interest exists.
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            /// Notify via email in case a PoI gets deleted
            _mailService.Send("Point of interest was deleted",
                $"Point of interest #{pointOfInterestFromStore.Id} - {pointOfInterestFromStore.Name} was deleted");

            return NoContent();
        }
    }
}