using DataAnnotationsExtensions;

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ShipManagement.DTOs
{
    public class GeoLocation
    {
        public GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }


        //The latitude must be a number between -90 and 90 and the longitude between -180 and 180.

        [Range(-90, 90, ErrorMessage = "The latitude must be a number between -90 and 90.")]
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }


        [Range(-180, 180, ErrorMessage = "The longitude must be a number between -180 and 180.")]
        [DataMember(Name = "latitude")]
        public double Longitude { get; set; }
    }
}
