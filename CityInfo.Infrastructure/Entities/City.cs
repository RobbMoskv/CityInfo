using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CityInfo.Infrastructure.Entities
{
    /// <summary>
    /// Database entities for Cities
    /// </summary>
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        ///  Initialize to an empty list to avoid null reference exception problems
        /// </summary>
        public ICollection<PointOfInterest> PointOfInterest { get; set; }
            = new List<PointOfInterest>();
    }
}
