using System;
using System.Collections.Generic;

namespace PGModels.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Flights = new HashSet<Flight>();
        }

        public int? LuggageWeight { get; set; }
        public string? CompanionName { get; set; }
        public string? CompanionRelation { get; set; }
        public int PassengerId { get; set; }
        public int? PersonId { get; set; }

        public virtual Person? Person { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
