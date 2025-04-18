﻿using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Userlogin
    {
        public decimal Loginid { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? User_Id { get; set; }
        public decimal Role_id { get; set; }
        public decimal? Adminid { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
