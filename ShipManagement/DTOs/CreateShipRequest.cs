using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ShipManagement.DTOs
{
    public class CreateShipRequest
    {
        [Required]
        [DataMember(Name = "name")]
        public string? Name { get; set; }

        [Required]
        [DataMember(Name = "uniqueShipId")]
        public string? UniqueShipId { get; set; }

        [DataMember(Name = "velocity")]
        public int Velocity { get; set; }

        [Range(-90, 90)]
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        [DataMember(Name = "latitude")]
        public double Longitude { get; set; }
    }
}