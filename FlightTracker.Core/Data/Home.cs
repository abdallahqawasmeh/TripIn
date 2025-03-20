using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Home
    {
        public decimal Homeid { get; set; }
        public string Companyname { get; set; } = null!;
        public string? Imagepath1 { get; set; }
        public string? Imagepath2 { get; set; }
        public string? Imagepath3 { get; set; }
        public string? Paragraphbig { get; set; }
        public string? Paragraphsmall { get; set; }
        public string? Traveltext { get; set; }
        public string? Experiencetext { get; set; }
        public string? Additionaltext { get; set; }
        public string? Footerparagraph { get; set; }
    }
}
