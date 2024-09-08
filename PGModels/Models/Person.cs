using System;
using System.Collections.Generic;

namespace PGModels.Models
{
    public partial class Person
    {
        public Person()
        {
            Passengers = new HashSet<Passenger>();
            Pilots = new HashSet<Pilot>();
        }

        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public DateTime? Dob { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Gender { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Pilot> Pilots { get; set; }
    }
}
