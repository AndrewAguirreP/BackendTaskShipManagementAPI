using System.Runtime.Serialization;

namespace ShipManagement.DTOs
{
    public class GetClosestPortResponse
    {
        public GetClosestPortResponse()
        {
            ClosestPort = new PortDetail();
            ShipDetail = new ShipDetail();
            TravelInformation = new TravelInformation();
        }

        [DataMember(Name = "closestPort")]
        public PortDetail? ClosestPort { get; set; }

        [DataMember(Name = "shipDetail")]
        public ShipDetail? ShipDetail { get; set; }

        [DataMember(Name = "travelInformation")]
        public TravelInformation? TravelInformation { get; set; }
    }
}