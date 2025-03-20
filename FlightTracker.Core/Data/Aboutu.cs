using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Aboutu
    {
        public decimal Aboutusid { get; set; }
        public string Aboutustext { get; set; } = null!;
        public string? Imagepath1 { get; set; }
        public string? Imagepath2 { get; set; }
        public string Companyname { get; set; } = null!;
    }
}
