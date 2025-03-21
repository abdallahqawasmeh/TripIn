using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightTracker.Core.Data
{
    public partial class Flight
    {
        public Flight()
        {
            Bookflights = new HashSet<Bookflight>();
            Invoices = new HashSet<Invoice>();
            Payments = new HashSet<Payment>();
            Reservations = new HashSet<Reservation>();
        }

        public decimal Flightid { get; set; }
        public string Flightnumber { get; set; } = null!;
        public DateTime Departuretime { get; set; }
        public DateTime Arrivaltime { get; set; }
        public decimal Price { get; set; }
        public decimal Availableseats { get; set; }
        public decimal? Status { get; set; } 
        public decimal? Arrivalairportid { get; set; }
        public decimal? Departureairportid { get; set; }
        public decimal Numberofpassengers { get; set; }
        public decimal Companyid { get; set; }
        [NotMapped]
        public decimal TotalProfit=> Price * Numberofpassengers;
        [NotMapped]
        public decimal TotalLose=> Price * Availableseats;
		[NotMapped]
		public TimeOnly Duration =>
			Arrivaltime < Departuretime
				? TimeOnly.FromTimeSpan(Arrivaltime.Add(TimeSpan.FromDays(1)) - Departuretime)
				: TimeOnly.FromTimeSpan(Arrivaltime - Departuretime);
		[NotMapped]
		public string Flag => DateTime.UtcNow < Departuretime ? "stop" : DateTime.UtcNow < Arrivaltime ? "flying" : "arrived";
        

        public virtual Airport? Arrivalairport { get; set; }
        public virtual Company Company { get; set; } = null!;
        public virtual Airport? Departureairport { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bookflight> Bookflights { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
