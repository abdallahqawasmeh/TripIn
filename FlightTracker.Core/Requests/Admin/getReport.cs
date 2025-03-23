using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.Admin
{
	public class getReport
	{
        public string? Start { get; set; }
        public string? End { get; set; }
        public DateOnly? StartDateOnly => Start == null ? null : DateOnly.Parse(Start);
        public DateOnly? EndDateOnly => End == null ? null : DateOnly.Parse(End);
    }
}
