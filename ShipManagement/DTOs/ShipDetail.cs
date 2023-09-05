using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ShipManagement.DTOs
{
    public class ShipDetail
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "uniqueShipId")]
        public string? UniqueShipId { get; set; }

        [Required]
        [DataMember(Name = "name")]
        public string? Name { get; set; }

        [DataMember(Name = "velocity")]
        public int Velocity { get; set; }

        [DataMember(Name = "currentGeoLocation")]
        public GeoLocation? CurrentGeoLocation { get; set; }
    }
}