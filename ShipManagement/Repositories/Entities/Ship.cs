using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipManagement.Repositories.Models
{
    public class Ship
    {
        public Ship()
        {
            ShipStatuses = new List<ShipStatus>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? UniqueShipId { get; set; }
        public string? Name { get; set; }
        public ICollection<ShipStatus> ShipStatuses { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegistrationDateTime { get; set; }
    }
}