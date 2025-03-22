using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{

	public interface IContactInfoRepository
	{
		void CreateContactInfo(Contactinfo contactInfo);
		void UpdateContactInfo(Contactinfo contactInfo);
		void DeleteContactInfo(int contactId);
		Contactinfo? GetContactInfoById(int id);
	}
}
