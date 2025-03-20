using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IAboutUsRepository
	{
		void CreateAboutUs(Aboutu aboutUs);
		void UpdateAboutUs(Aboutu aboutUs);
		void DeleteAboutUs(int id);
	}
}
