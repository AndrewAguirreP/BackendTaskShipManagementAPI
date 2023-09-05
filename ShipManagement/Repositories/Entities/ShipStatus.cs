using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipManagement.Repositories.Models
{
    public class ShipStatus
    {
        [Key]
        public int Id { get; set; }

        public int Velocity { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime LastCheckedIn { get; set; }

        [ForeignKey("Ship")]
        public int ShipId { get; set; }

        public Ship Ship { get; set; } = null!;
    }
}