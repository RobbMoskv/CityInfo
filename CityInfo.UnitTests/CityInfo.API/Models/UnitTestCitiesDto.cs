using CityInfo.API;
using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CityInfo.UnitTests.CityInfo.API.Models
{

    public class UnitTestCitiesDto
    {
        [Fact]
        public void NumberOfPointsOfInterest_ReturnCorrectCount()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void CitiesDto_ReturnCorrectCityObject()
        {
            // Arrange
            CityDto expected = new CityDto()
            {
                Id = 1000,
                Name = "Test-Town",
                Description = "I like to test things",
            };

            // Act
            // Assert
            Assert.IsType<CityDto>(expected);

            Assert.Equal("1000", expected.Id.ToString());
            Assert.Equal("Test-Town", expected.Name.ToString());
            Assert.Equal("I like to test things", expected.Description.ToString());
        }
    }

}
