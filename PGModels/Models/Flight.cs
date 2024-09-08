using System;
using System.Collections.Generic;

namespace PGModels.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Pilots = new HashSet<Pilot>();
            Passengers = new HashSet<Passenger>();
        }

        public TimeSpan? TimeOfFlight { get; set; }
        public int? Distance { get; set; }
        public DateTime? DateTime { get; set; }
        public decimal? FuelUsed { get; set; }
        public string FlightId { get; set; } = null!;
        public string? AirCraftId { get; set; }

        public virtual AirCraft? AirCraft { get; set; }
        public virtual ICollection<Pilot> Pilots { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}
