using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.ManagePages.AboutUs;
using FlightTracker.Core.Requests.ManagePages.ContactInfo;
using FlightTracker.Core.Requests.ManagePages.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface IManagePages
	{

		
		bool UpdateAboutUs(int aboutUsid, UpdateAboutUsRequest aboutUs);
		Aboutu? GetAboutUsById(int id);


		bool UpdateContactInfo(int contactInfoid, UpdateContactInfoRequest contactInfo);
		Contactinfo? GetContactInfoById(int id);



		bool UpdateHome(int Homeid, UpdateHomeRequest home);
		Home? GetHomeById(int id);






		//	bool UpdateContactUs(int contactUsid, Contactu contactUs);

	}
}
