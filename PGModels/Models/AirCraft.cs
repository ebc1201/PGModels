using System;
using System.Collections.Generic;

namespace PGModels.Models
{
    public partial class AirCraft
    {
        public AirCraft()
        {
            Flights = new HashSet<Flight>();
        }

        public string? Type { get; set; }
        public string AirCraftId { get; set; } = null!;
        public string? EngineType { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
