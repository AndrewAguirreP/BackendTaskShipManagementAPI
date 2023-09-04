using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ShipManagement.DTOs
{
    public class PortDetail
    {
        [DataMember(Name= "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "geoLocation")]
        public GeoLocation GeoLocation { get; set; }
    }
}
