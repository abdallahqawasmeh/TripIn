using FlightTracker.Core.Common;
using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IContactUsRepository
	{
		void CreateContactUs(Contactu contactUs);
		void UpdateContactUs(Contactu contactUs);
		void DeleteContactUs(int contactUsId);
	}
}
