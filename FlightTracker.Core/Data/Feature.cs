using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Feature
    {
        public decimal Featuresid { get; set; }
        public string Featuresname { get; set; } = null!;
        public string Featurestext { get; set; } = null!;
    }
}
