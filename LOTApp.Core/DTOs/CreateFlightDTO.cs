﻿using System.ComponentModel.DataAnnotations;

namespace LOTApp.Core.DTOs
{
    public class CreateFlightDTO
    {
        [Required]
        public string FlightNumber { get; set; } = null!;
        [Required]
        public DateTime DepartTime { get; set; }
        [Required]

        public string DepartLocation { get; set; } = null!;
        [Required]

        public string ArrivalLocation { get; set; } = null!;
        [Required]
        public string PlaneType { get; set; } = null!;
    }
}
