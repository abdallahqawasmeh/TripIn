using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.ManagePages.ContactUs;
using FlightTracker.Core.Requests.User;
using FlightTracker.Core.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly IClaimsReader _claimsReader;
		private readonly ITestimonialRepository _TestimonialRepository;
		private readonly IConfiguration _configuration;

		public UserService(IUserRepository userRepository, IUserLoginRepository userLoginRepository,
			IClaimsReader claimsReader,ITestimonialRepository testimonialRepository,IConfiguration configuration)
		{
			_userRepository = userRepository;
			_userLoginRepository = userLoginRepository;
			_claimsReader = claimsReader;
			_TestimonialRepository = testimonialRepository;
			_configuration = configuration;

				

		}

		public User GetMyProfile()
		{
			var usernamme = _claimsReader.GetByClaimType(ClaimTypes.Name)!; 
			var userLogin = _userLoginRepository.GetLoginByUsername(usernamme)!;
			return  _userRepository.GetUserById((int)userLogin.User_Id!)!;
		}

		public bool UpdateMyProfile(UpdateUserRequest request)
		{
            var usernamme = _claimsReader.GetByClaimType(ClaimTypes.Name)!;
			 var userLogin = _userLoginRepository.GetLoginByUsername(usernamme)!;
            var user =  _userRepository.GetUserById((int)userLogin.User_Id!)!;

			user.Email = request.Email ?? user.Email;
			user.Firstname = request.Firstname ?? user.Firstname;
			user.Lastname = request.Lastname ?? user.Lastname;
			user.Imagepath = request.Imagepath ?? user.Imagepath;
			user.Phonenumber = request.Phonenumber ?? user.Phonenumber;


			if(request.Username != null && userLogin.Username != request.Username )
			{
				var newUsernameLogin = _userLoginRepository.GetLoginByUsername(request.Username);
			if (newUsernameLogin != null)
					return false;

				userLogin.Username = request.Username ?? userLogin.Username;
			}
			userLogin.Password = request.Password ?? userLogin.Password;

			_userLoginRepository.UpdateLogin(userLogin);
			_userRepository.UpdateUser(user);
			return true;
        }

			
			
			



		public List<User> GetAllUsers()
		{
			return _userRepository.GetAllUsers();
		}

		public List<User> GetUsersByName(string search)
		{
			return GetAllUsers().Where(x => x.Firstname.Contains(search)).ToList();
		}

		public User? GetUserById (int id)
		{
			return _userRepository.GetUserById(id);
		}

		[Authorize]
		public void CreateTestMonial(string testmonialText)
		{
			var user = GetMyProfile();

			var testmonial = new Testimonial()
			{
				Userid = user.Userid,
				Testimonialtext = testmonialText,
				Status = 0,
				Testimonialdate = DateTime.UtcNow
			};
			_TestimonialRepository.CreateTestimonial(testmonial);

		}
		
		public List<Testimonial> GetTestimonials()
		{
		
			var testimonials =  _TestimonialRepository.GetAllTestimonials().Where(x => x.Status == 1).ToList();
			List<Testimonial> result=new List<Testimonial>();
			foreach(var test in testimonials)
			{
                test.User = _userRepository.GetUserById((int)test.Userid!);
				result.Add(test);
            }
			return result;
		}


		
			public async Task SendContactUsEmailAsync(ContactUsRequest request)
			{
				var message = new MimeMessage();
				var emailSettings = _configuration.GetSection("EmailSettings");

				message.To.Add(new MailboxAddress(request.Fullname, "abdallahqawasmeh950@gmail.com"));
				message.From.Add(new MailboxAddress("Flight Tracker Support", emailSettings["SenderEmail"]));
				message.Subject = request.Subject;

				var builder = new BodyBuilder
				{
					HtmlBody = $@"
				<h2>New Contact Us Message</h2>
			<p><strong>Name:</strong> {request.Fullname}</p>
			<p><strong>Email:</strong> {request.Email}</p>
			<p><strong>Message:</strong></p>
			<p>{request.Message}</p>
			<p>Received on: {DateTime.Now:dd/MM/yyyy HH:mm}</p>",

					TextBody = $"New Contact Us Message\nName: {request.Fullname}\nEmail: {request.Email}\nMessage: {request.Message}\nReceived on: {DateTime.Now:dd/MM/yyyy HH:mm}"
				};

				message.Body = builder.ToMessageBody();

				using var client = new SmtpClient();
				await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}



		


	}
}
