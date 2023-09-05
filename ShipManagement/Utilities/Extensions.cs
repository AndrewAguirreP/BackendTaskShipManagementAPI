using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Common
{
    public static class Extensions
    {
        private const double EarthRadiusNauticalMiles = 3440.065;

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null || !source.Any())
                return true;

            return false;
        }

        /// <summary>
        /// Uses Haversine Formula https://www.themathdoctors.org/distances-on-earth-2-the-haversine-formula/
        ///     http://www.movable-type.co.uk/scripts/latlong.html
        /// <returns></returns>
        public static TravelInformation GetTravelInformation(this GeoLocation currentGeoLocation, GeoLocation targetGeoLocation, double velocityKnots)
        {
            var distanceNauticalMiles = GetDistanceInNauticalMiles(currentGeoLocation, targetGeoLocation);

            var response = new TravelInformation()
            {
                Units = "NauticalMiles",
                Distance = distanceNauticalMiles,
                TravelTime = $"N/A",
                EstimatedArrivalTimeUTC = null,
                CurrentVelocity = $"{velocityKnots} knots"
            };

            if (velocityKnots > 0 && distanceNauticalMiles > 0)
            {
                var travelTimeHours = distanceNauticalMiles / velocityKnots;
                var timeSpan = TimeSpan.FromHours(travelTimeHours);

                int days = timeSpan.Days;
                int hours = timeSpan.Hours;
                int minutes = timeSpan.Minutes;

                DateTime currentUtcTime = DateTime.UtcNow;
                DateTime estimateDArrivalTime = currentUtcTime.Add(timeSpan);

                response = new TravelInformation()
                {
                    Units = "NauticalMiles",
                    Distance = distanceNauticalMiles,
                    TravelTime = $"{days} day(s), {hours} hour(s), {minutes} minute(s)",
                    EstimatedArrivalTimeUTC = estimateDArrivalTime,
                    CurrentVelocity = $"{velocityKnots} knots"
                };
            }

            return response;
        }

        /// <summary>
        ///  Get Distance in Nautical Miles between Two Geo Locations
        /// </summary>
        /// <param name="currentGeoLocation"></param>
        /// <param name="targetGeoLocation"></param>
        /// <returns>Distance in Nautical Miles</returns>
        public static double GetDistanceInNauticalMiles(this GeoLocation currentGeoLocation, GeoLocation targetGeoLocation)
        {
            var startLatitudeInRadians = DegreesToRadians(currentGeoLocation.Latitude);
            var startLonitudeInRadians = DegreesToRadians(currentGeoLocation.Longitude);
            var endLatitudeInRadians = DegreesToRadians(targetGeoLocation.Latitude);
            var endLongitudeRadians = DegreesToRadians(targetGeoLocation.Longitude);

            var latitudeDifference = endLatitudeInRadians - startLatitudeInRadians;
            var longitudeDifference = endLongitudeRadians - startLonitudeInRadians;

            // Haversine formula
            var halfChordLength = Math.Pow(Math.Sin(latitudeDifference / 2.0), 2) +
                       Math.Cos(startLatitudeInRadians) * Math.Cos(endLatitudeInRadians) *
                       Math.Pow(Math.Sin(longitudeDifference / 2.0), 2);

            var angularDistance = 2 * Math.Atan2(Math.Sqrt(halfChordLength), Math.Sqrt(1 - halfChordLength));

            return EarthRadiusNauticalMiles * angularDistance;
        }

        public static GeoLocation ToGeoLocation(this Port port)
        {
            return new GeoLocation(port.Latitude, port.Longitude);
        }

        public static ShipStatus? GetLatestStatus(this Ship ship)
        {
            if (ship == null || ship.ShipStatuses.IsNullOrEmpty())
                return null;

            //ShipStatus can be Sorted By latest status Id or If Table Doesn't have key By LastCheckedIn DateTime.
            return ship.ShipStatuses.OrderByDescending(status => status.Id).FirstOrDefault();
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public static Port? ToClosestPort(this Ship ship, IEnumerable<Port> ports)
        {
            var shipsLatestStatus = ship.GetLatestStatus();

            if (shipsLatestStatus == null)
                return null;

            var shipCurrentLocation = new GeoLocation(shipsLatestStatus.Latitude, shipsLatestStatus.Longitude);
            var portsSortedByDistance = ports.OrderBy(port => port.ToGeoLocation().GetDistanceInNauticalMiles(shipCurrentLocation));

            return portsSortedByDistance.FirstOrDefault();
        }
    }
}