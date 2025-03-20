using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Contactinfo
    {
        public decimal Contactid { get; set; }
        public string Phonenumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Facebooklink { get; set; }
        public string? Instagramlink { get; set; }
        public string? Xlink { get; set; }
        public string? Copyright { get; set; }
        public string Location { get; set; } = null!;
    }
}
