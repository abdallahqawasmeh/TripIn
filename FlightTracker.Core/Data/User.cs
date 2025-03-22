using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class User
    {
        public User()
        {
            Contactus = new HashSet<Contactu>();
            Invoices = new HashSet<Invoice>();
            Reservations = new HashSet<Reservation>();
            Testimonials = new HashSet<Testimonial>();
            Userlogins = new HashSet<Userlogin>();
        }

        public decimal Userid { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }
        public string? Imagepath { get; set;}
        public decimal? Flightcount { get; set; }

        public virtual ICollection<Contactu> Contactus { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
        public virtual ICollection<Userlogin> Userlogins { get; set; }
    }
}
