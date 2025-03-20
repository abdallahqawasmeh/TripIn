using FlightTracker.Core.Common;
using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IHomeRepository
	{
		void CreateHome(Home home);
		void UpdateHome(Home home);
		void DeleteHome(int homeId);
	}
}
