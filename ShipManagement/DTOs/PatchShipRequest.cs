using System.Runtime.Serialization;

namespace ShipManagement.DTOs
{
    public class PatchShipsVelocityRequest
    {
        [DataMember(Name = "velocity")]
        public int Velocity { get; set; }
    }
}