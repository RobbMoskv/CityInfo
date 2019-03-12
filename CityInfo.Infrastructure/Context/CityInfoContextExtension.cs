using CityInfo.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityInfo.Infrastructure.Context
{
    public static class CityInfoContextExtension
    {
        /// <summary>
        /// Static method to initially seed the database
        /// </summary>
        /// <param name="context"></param>
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            // Dont' seed database in case data already exists.
            if (context.Cities.Any())
            {
                return;
            }

            var cities = new List<City>()
            {
                #region Create and seed City objects
                new City()
                {
                    Name = "New York City",
                    Description = "Where to cool and fancy people live and do business.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Central Park",
                            Description = "Super cool experience."
                        },
                        new PointOfInterest()
                        {
                            Name = "Brookly Bridge",
                            Description = "Wow. So high."
                        },
                    }
                },
                new City()
                {
                    Name = "London City",
                    Description = "Huge city but you never feel lost somehow.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Camden Market",
                            Description = "Special things to buy."
                        },
                        new PointOfInterest()
                        {
                            Name = "Musem of Art",
                            Description = "Was a bit annoying. No art at all."
                        },
                    }
                },
                new City()
                {
                    Name = "Lucerne",
                    Description = "Just that big that you can turn around a reach nature in a bunch of minutes from everywhere.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Verkehrshaus",
                            Description = "So impressing how they managed the Gotthard went through."
                        },
                        new PointOfInterest()
                        {
                            Name = "Schwanenplatz",
                            Description = "They should call it China-Town. Crazy!"
                        },
                    }
                },
                #endregion
            };

            /// Add list to the context
            context.Cities.AddRange(cities);
            /// Execute statement on the database
            context.SaveChanges();

        }
    }
}
