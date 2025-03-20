using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Contactu
    {
        public decimal Contactusid { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Sentdate { get; set; }
        public decimal? Userid { get; set; }

        public virtual User? User { get; set; }
    }
}
