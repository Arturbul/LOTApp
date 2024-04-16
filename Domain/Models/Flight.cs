using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int FlightNumber { get; set; }
        public DateTime DepartTime { get; set; }
        public string DepartLocation { get; set; } = null!;
        public string ArrivalLocation { get; set; } = null!;
        public PlaneType PlaneType { get; set; }
    }
}
