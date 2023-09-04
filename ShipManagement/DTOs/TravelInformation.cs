namespace ShipManagement.DTOs
{
    public class TravelInformation
    {
        public string? CurrentVelocity { get; set; }
        public string? Units { get; set; }
        public double Distance { get; set; }
        public string TravelTime { get; set; }
        public DateTime? EstimatedArrivalTimeUtc { get; set; }
    }
}
