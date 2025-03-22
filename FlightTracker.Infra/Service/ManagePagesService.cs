using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.ManagePages.AboutUs;
using FlightTracker.Core.Requests.ManagePages.ContactInfo;
using FlightTracker.Core.Requests.ManagePages.Home;
using FlightTracker.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Service
{
	public class ManagePagesService : IManagePages
	{
		private readonly IAboutUsRepository _aboutUsRepository;
		private readonly IHomeRepository _homeRepository;
		private readonly IContactInfoRepository _contactInfoRepository;
		private readonly IContactUsRepository _contactUsRepository;

		public ManagePagesService(IAboutUsRepository aboutUsRepository, IHomeRepository homeRepository, IContactInfoRepository contactInfoRepository, IContactUsRepository contactUsRepository)
		{
			_aboutUsRepository = aboutUsRepository;
			_homeRepository = homeRepository;
			_contactInfoRepository = contactInfoRepository;
			_contactUsRepository = contactUsRepository;
		}

		public Aboutu? GetAboutUsById(int id)
		{

			return _aboutUsRepository.GetAboutUsById(id);

		}

		public Contactinfo? GetContactInfoById(int id)
		{
			return _contactInfoRepository.GetContactInfoById(id);
		}

		public Home? GetHomeById(int id)
		{
			return _homeRepository.GetHomeById(id);
		}

		public bool UpdateAboutUs( int aboutUsid, UpdateAboutUsRequest aboutUs)
		{
			var oldAboutUs= _aboutUsRepository.GetAboutUsById(aboutUsid);

			if (oldAboutUs != null)
			{
				oldAboutUs.Aboutustext= aboutUs.Aboutustext??oldAboutUs.Aboutustext;
				oldAboutUs.Companyname = aboutUs.Companyname??oldAboutUs.Companyname;
				oldAboutUs.Imagepath1 = aboutUs.Imagepath1??oldAboutUs.Imagepath1;
				oldAboutUs.Imagepath2= aboutUs.Imagepath2 ?? oldAboutUs.Imagepath2;
				
				_aboutUsRepository.UpdateAboutUs( oldAboutUs);

				return true ;
			}
			return false ;

		}

		public bool UpdateContactInfo(int contactInfoid, UpdateContactInfoRequest contactInfo)
		{
			var oldContacInfo = _contactInfoRepository.GetContactInfoById(contactInfoid);

			if (oldContacInfo != null)
			{
				oldContacInfo.Phonenumber = contactInfo.Phonenumber ?? oldContacInfo.Phonenumber;
				oldContacInfo.Email = contactInfo.Email ?? oldContacInfo.Email;
				oldContacInfo.Facebooklink = contactInfo.Facebooklink ?? oldContacInfo.Facebooklink;
				oldContacInfo.Instagramlink = contactInfo.Instagramlink ?? oldContacInfo.Instagramlink;
				oldContacInfo.Xlink = contactInfo.Xlink ?? oldContacInfo.Xlink;
				oldContacInfo.Copyright = contactInfo.Copyright ?? oldContacInfo.Copyright;
				oldContacInfo.Location =contactInfo.Location ?? oldContacInfo.Location;

				_contactInfoRepository.UpdateContactInfo(oldContacInfo);

				return true;
			}
			return false ;
		}

	

		public bool UpdateHome(int Homeid,UpdateHomeRequest home)
		{


		var	oldHome = _homeRepository.GetHomeById(Homeid);
			if (oldHome != null)
			{
				oldHome.Companyname = home.Companyname ?? oldHome.Companyname;
				oldHome.Imagepath1 = home.Imagepath1 ?? oldHome.Imagepath1;
				oldHome.Imagepath2 = home.Imagepath2 ?? oldHome.Imagepath2;
				oldHome.Imagepath3 = home.Imagepath3 ?? oldHome.Imagepath3;
				oldHome.Paragraphbig = home.Paragraphbig ?? oldHome.Paragraphbig;
				oldHome.Paragraphsmall = home.Paragraphsmall ?? oldHome.Paragraphsmall;
				oldHome.Traveltext = home.Traveltext ?? oldHome.Traveltext;
				oldHome.Experiencetext = home.Experiencetext ?? oldHome.Experiencetext;
				oldHome.Additionaltext = home.Additionaltext ?? oldHome.Additionaltext;
				oldHome.Footerparagraph = home.Footerparagraph ?? oldHome.Footerparagraph;


				_homeRepository.UpdateHome(oldHome);
				return true;
			}
			return false;








		}




		
	}
}
