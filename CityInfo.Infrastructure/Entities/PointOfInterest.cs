using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CityInfo.Infrastructure.Entities
{
    public class PointOfInterest
    {
        [Key]
        public int Id { get; set; }
    }
}
