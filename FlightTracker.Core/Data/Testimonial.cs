using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Testimonial
    {
        public decimal Testimonialid { get; set; }
        public string Testimonialtext { get; set; } = null!;
        public DateTime Testimonialdate { get; set; }
        public decimal? Status { get; set; }//{0:pending,1:accepted,2:rejected}
        public decimal? Userid { get; set; }

        public virtual User? User { get; set; }
    }
}
