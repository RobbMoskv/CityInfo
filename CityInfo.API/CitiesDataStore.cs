using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        /// <summary>
        /// Returns an instance of CitiesDataStore
        /// </summary>
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "Where to cool and fancy people live and do business.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "Super cool experience."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Brookly Bridge",
                            Description = "Wow. So high."
                        },
                    }
                },
                new CityDto
                {
                    Id = 2,
                    Name = "London City",
                    Description = "Huge city but you never feel lost somehow.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Camden Market",
                            Description = "Special things to buy."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Musem of Art",
                            Description = "Was a bit annoying. No art at all."
                        },
                    }
                },
                new CityDto
                {
                    Id = 3,
                    Name = "Lucerne",
                    Description = "Just that big that you can turn around a reach nature in a bunch of minutes from everywhere.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Verkehrshaus",
                            Description = "So impressing how they managed the Gotthard went through."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Schwanenplatz",
                            Description = "They should call it China-Town. Crazy!"
                        },
                    }
                },
            };
        }

    }
}
