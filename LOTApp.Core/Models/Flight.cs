using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOTApp.Core.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(6)")]
        [RegularExpression(@"^([a-zA-Z][\d]|[\d][a-zA-Z]|[a-zA-Z]{2})(\d{1,})$", ErrorMessage = "required IATA (marketing) code")]
        public string FlightNumber { get; set; } = null!;
        public DateTime DepartTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^[a-zA-Z]{3}$$", ErrorMessage = "required IATA Airport Commercial service mark")]
        public string DepartLocation { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^[a-zA-Z]{3}$$", ErrorMessage = "required IATA Airport Commercial service mark")]
        public string ArrivalLocation { get; set; } = null!;
        public PlaneType PlaneType { get; set; }
    }
}
