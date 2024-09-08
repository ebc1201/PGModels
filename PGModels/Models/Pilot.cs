using System;
using System.Collections.Generic;

namespace PGModels.Models
{
    public partial class Pilot
    {
        public string LicenseNumber { get; set; } = null!;
        public int? TotalHours { get; set; }
        public string? Certifications { get; set; }
        public string? Ratings { get; set; }
        public string? FlightId { get; set; }
        public int PersonId { get; set; }

        public virtual Flight? Flight { get; set; }
        public virtual Person Person { get; set; } = null!;
    }
}
