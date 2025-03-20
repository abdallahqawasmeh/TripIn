using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Admin
    {
        public Admin()
        {
            Userlogins = new HashSet<Userlogin>();
        }

        public decimal Adminid { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }
        public string? Imagepath { get; set; }

        public virtual ICollection<Userlogin> Userlogins { get; set; }
    }
}
