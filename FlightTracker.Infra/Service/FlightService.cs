using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.Flight;
using FlightTracker.Core.Service;

using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Claims;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;

namespace FlightTracker.Infra.Service
{
	public class FlightService : IFlightService
	{


		private readonly IAirportRepository _airportRepository;
		private readonly IFlightRepository _flightRepository;
		private readonly ICompanyRepository _companyRepository;
		private readonly IClaimsReader _claimsReader;
		private readonly  IUserLoginRepository _userLoginRepository;
		private readonly IUserRepository _userRepository;
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly IPaymentRepository _paymentRepository;
		private readonly InvoiceService _invoiceService;
		private readonly IConfiguration _configuration;

		public FlightService(IAirportRepository airportRepository, IFlightRepository flightRepository, ICompanyRepository companyRepository,
			IClaimsReader claimsReader, IUserLoginRepository userLoginRepository, IUserRepository userRepository,
			IInvoiceRepository invoiceRepository, IPaymentRepository paymentRepository, InvoiceService invoiceService, IConfiguration configuration)
		{
			_airportRepository = airportRepository;
			_flightRepository = flightRepository;
			_companyRepository = companyRepository;
			_claimsReader = claimsReader;
			_userLoginRepository = userLoginRepository;
			_userRepository = userRepository;
			_invoiceRepository = invoiceRepository;
			_paymentRepository = paymentRepository;
			_invoiceService = invoiceService;
			_configuration = configuration;
		}

		public void CreateFlight(CreateFlightRequest flight)
		{
			var newFlight = new Flight()
			{
				Arrivalairportid = flight.Arrivalairportid,
				Departureairportid = flight.Departureairportid,
				Arrivaltime = flight.Arrivaltime,
				Companyid = flight.Companyid,
				Departuretime = flight.Departuretime,
				Flightnumber = flight.FlightName,
				Availableseats = flight.Availableseats,
				Status = 1,
				Price = flight.Price,
				Numberofpassengers = 0


			};


			_flightRepository.CreateFlight(newFlight);


		}

		public void DeleteFlight(int flightId)
		{


			_flightRepository.DeleteFlight(flightId);


		}

		public List<Flight> GetAllFlights()
		{
			return _flightRepository.GetAllFlights();
		}

		public Flight? GetFlightById(int flightId)
		{
			var flight = _flightRepository.GetFlightById(flightId);
			if (flight == null)
				return null;
            var arrAir = _airportRepository.GetAirportById((int)flight.Arrivalairportid!); 
			var depAir = _airportRepository.GetAirportById((int)flight.Departureairportid!);
			var comp = _companyRepository.GetCompanyById((int)flight.Companyid)!;
			flight.Arrivalairport = arrAir;
            flight.Departureairport = depAir;
			flight.Company = comp;
			return flight;

        }

        public bool UpdateFlight(int flightId, UpdateFlightRequest flight)
		{
			var oldFlight = _flightRepository.GetFlightById(flightId);
			if (oldFlight == null)
				return false;
			oldFlight.Status = flight.Status ?? oldFlight.Status;
			_flightRepository.UpdateFlight(oldFlight);
			return true;
		}
		public List<Flight> GetFlightsByDate(SearchFlightsRequest request)
		{
			var isRangeNull = request.EndDateOnly == null || request.StartDateOnly == null || request.StartDateOnly == null;


			var flights = GetAllFlights().Where(
				x =>
			(isRangeNull || (DateOnly.FromDateTime(x.Departuretime) >= request.StartDateOnly && DateOnly.FromDateTime(x.Departuretime) <= request.EndDateOnly))
			&& (request.ArrivalAirPortId == x.Arrivalairportid && x.Departureairportid == request.DepartureAirportId)
			);
			if (request.Des)
			{
				if (request.SortByFastest)
					return flights.OrderByDescending(x => x.Duration).ToList();
				return flights.OrderByDescending(x => x.Price).ToList();
			}

			if (request.SortByFastest)
				return flights.OrderBy(x => x.Duration).ToList();
			return flights.OrderBy(x => x.Price).ToList();
		}



        [Authorize(Roles = "user")]
        public async Task<bool?> BookFlight(int FlightId,int numberOfPassengers)
		{
			var flight = _flightRepository.GetFlightById(FlightId);
			if(flight == null)
			{
				return null;
			}
			var payment = _paymentRepository.GetPaymentById(1);
			if (payment.Balance < flight.Price* numberOfPassengers)
				return false;

            var username = _claimsReader.GetByClaimType(ClaimTypes.Name)!;
            var userLogin = _userLoginRepository.GetLoginByUsername(username)!;
            var user = _userRepository.GetUserById((int)userLogin.User_Id!)!;


            flight.Availableseats -= numberOfPassengers;
			flight.Numberofpassengers += numberOfPassengers;
			payment.Balance -= flight.Price * numberOfPassengers;

			

			_flightRepository.UpdateFlight(flight);
			_paymentRepository.UpdatePaymentBalance(payment.Balance!.Value);
            var invoice = new Invoice()
            {
                Userid = user.Userid,
                Paymentid = payment.Paymentid,
                Flightid = flight.Flightid,
                Invoicedate = DateTime.UtcNow,
                Tax = numberOfPassengers,
                Totalamount = numberOfPassengers * flight.Price,
                User = user,
                Flight = flight
            };

			user.Flightcount +=1;
			_userRepository.UpdateUser(user);
            invoice.Filepath = _invoiceService.GenerateInvoice(invoice);
            _invoiceRepository.CreateInvoice(invoice);

			// Send email if member has email address
			if (!string.IsNullOrEmpty(user.Email))
			{
				await SendInvoiceEmailAsync(user, invoice.Filepath);
			}
			

            return true;
		}

		public async Task SendInvoiceEmailAsync(User user, string invoicePath)
		{
			// Ensure invoicePath is an absolute or correctly rooted path
			var fullInvoicePath = Path.Combine(Directory.GetCurrentDirectory(), invoicePath.TrimStart('/'));

			var message = new MimeMessage();
			var emailSettings = _configuration.GetSection("EmailSettings");

			message.From.Add(new MailboxAddress("Flight Tracker", emailSettings["SenderEmail"]));
			message.To.Add(new MailboxAddress($"{user.Firstname} {user.Lastname}", user.Email));
			message.Subject = "Your Flight Invoice";

			var builder = new BodyBuilder
			{
				HtmlBody = $@"
        <h2>Thank you for booking your flight, {user.Firstname}!</h2>
        <p>Please find your invoice attached.</p>
        <p>Invoice Date: {DateTime.Now:dd/MM/yyyy}</p>
        <p>If you have any questions, please contact us.</p>
        <br>
        <p>Best regards,<br>Flight Tracker Team</p>",
				TextBody = "Thank you for booking your flight. Your invoice is attached."
			};

			builder.Attachments.Add(fullInvoicePath);
			message.Body = builder.ToMessageBody();

			using var client = new SmtpClient();
			await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), SecureSocketOptions.StartTls);
			await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}

		[Authorize("admin")]
        public FlightStatistics ?GetFlightStatistics (int flightId)
		{
			var flight = _flightRepository.GetFlightById(flightId);
			if (flight == null)
				return null;

			return new FlightStatistics
			{
				Profit = flight.TotalProfit,
				Lose = flight.TotalLose,
				RemainingSeats = flight.Availableseats,
				PassengersCount = flight.Numberofpassengers,
			};
		}

	












    }
}