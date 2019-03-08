using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    /// <summary>
    /// This is a view model
    /// DTO stands for "Data transfer object"
    /// </summary>
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count();
            }
        }

        /// <summary>
        ///  Initialize Collection direclty here instead in constructor
        /// </summary>
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
        = new List<PointOfInterestDto>();
    }
}
