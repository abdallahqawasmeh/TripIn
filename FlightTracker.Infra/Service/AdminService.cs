using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.Admin;
using FlightTracker.Core.Service;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlightTracker.Infra.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepsitory;
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly IClaimsReader _claimsReader;
        private readonly IUserRepository _userRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        public AdminService(IAdminRepository adminRepository, IUserLoginRepository userLoginRepository,
            ITestimonialRepository testimonialRepository, IClaimsReader claimsReader, IUserRepository userRepository,IInvoiceRepository invoiceRepository)
        {
            _adminRepsitory = adminRepository;
            _userLoginRepository = userLoginRepository;
            _testimonialRepository = testimonialRepository;
            _claimsReader = claimsReader;
            _userRepository = userRepository;
            _invoiceRepository = invoiceRepository;
        }
        [Authorize]
        public Admin GetMyProfile()
        {
            var username = _claimsReader.GetByClaimType(ClaimTypes.Name);
            var adminLogin = _userLoginRepository.GetLoginByUsername(username!);


            var admin = _adminRepsitory.GetAdminById((int)adminLogin?.Adminid!);
            return admin!;
        }
        public bool UpdateAdminProfile(UpdateAdminRequest request)
        {
            var username = _claimsReader.GetByClaimType(ClaimTypes.Name);
            var adminLogin = _userLoginRepository.GetLoginByUsername(username!);
            var admin = _adminRepsitory.GetAdminById((int)adminLogin?.Adminid!);
            if (admin == null)
                return false;

            admin.Firstname = request.Firstname ?? admin.Firstname;
            admin.Lastname = request.Lastname ?? admin.Lastname;
            admin.Phonenumber = request.Phonenumber ?? admin.Phonenumber;
            admin.Email = request.Email ?? admin.Email;
            admin.Imagepath = request.Imagepath ?? admin.Imagepath;

            if (request.userName != null && adminLogin.Username != request.userName)
            {
                var newUsernameLogin = _userLoginRepository.GetLoginByUsername(request.userName);
                if (newUsernameLogin != null)
                    return false;

                adminLogin.Username = request.userName ?? adminLogin.Username;
            }
            adminLogin.Password = request.Password ?? adminLogin.Password;

            _userLoginRepository.UpdateLogin(adminLogin);
            _adminRepsitory.UpdateAdmin(admin);
            return true;

        }


        public void UpdateTestmonialStatus(int status, int testmonialId)
        {
            _testimonialRepository.UpdateTestimonialStatus(testmonialId, status);
        }

        public List<Testimonial> GetNewTestimonials()
        {
            var testimonials = _testimonialRepository.GetAllTestimonials().ToList();
            List<Testimonial> result = new List<Testimonial>();
            foreach (var test in testimonials)
            {
                test.User = _userRepository.GetUserById((int)test.Userid!);
                result.Add(test);
            }
            return result;
        }

        public List<User> GetTop10()
        {
            return _userRepository.GetAllUsers().OrderByDescending(x => x.Flightcount).Take(10).ToList();
        }

        public Report Reports (DateOnly? Start , DateOnly? End)
        {
            if (!Start.HasValue)
            {
                Start = DateOnly.MinValue;

            }


            if (!End.HasValue)
            {
                End = DateOnly.MaxValue;
            }

            var invoices = _invoiceRepository.GetAllInvoices()                     
                   .Where(x => DateOnly.FromDateTime(x.Invoicedate.Value) >= Start && DateOnly.FromDateTime(x.Invoicedate.Value) <= End)!;
            var flightCount = invoices.Count();
            var profit = invoices.Sum(x => x.Totalamount)!;
            var report = new Report()
            {
               
                Profit = profit.Value,
                Flightcount = flightCount
            };
            return report;
        }





		public string GenerateReport(DateOnly? start, DateOnly? end)
		{
			var reportFolder = Path.Combine("Reports");
			Directory.CreateDirectory(reportFolder);

			var reportFileName = $"Report_{DateTime.Now:yyyyMMddHHmmss}.pdf";
			var reportPath = Path.Combine(reportFolder, reportFileName);

			if (!start.HasValue)
				start = DateOnly.MinValue;

			if (!end.HasValue)
				end = DateOnly.MaxValue;

			var invoices = _invoiceRepository.GetAllInvoices()
				.Where(x => DateOnly.FromDateTime(x.Invoicedate.Value) >= start && DateOnly.FromDateTime(x.Invoicedate.Value) <= end)
				.ToList();

			var flightCount = invoices.Count;
			var profit = invoices.Sum(x => x.Totalamount) ?? 0;

			using (var writer = new PdfWriter(reportPath))
			using (var pdf = new PdfDocument(writer))
			using (var document = new Document(pdf))
			{
				document.SetMargins(40, 40, 40, 40);

				document.Add(new Paragraph("Flight Tracker Report")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(24)
					.SetBold());

				document.Add(new Paragraph($"Report Period: {start.Value:dd/MM/yyyy} - {end.Value:dd/MM/yyyy}")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(16)
					.SetMarginBottom(20));

				var table = new Table(2)
					.SetWidth(UnitValue.CreatePercentValue(100))
					.SetMarginBottom(20);

				AddReportRow(table, "Total Flights:", flightCount.ToString());
				AddReportRow(table, "Total Profit:", $"{profit:F2} JOD");

				document.Add(table);

				document.Add(new Paragraph("Generated On:")
					.SetBold()
					.SetMarginTop(10));

				document.Add(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
					.SetFontSize(10));

				document.Add(new Paragraph("Thank you for choosing Flight Tracker!")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetMarginTop(30)
					.SetItalic());
			}

			return $"/Reports/{reportFileName}";
		}

		private void AddReportRow(Table table, string label, string value)
		{
			table.AddCell(new Cell()
				.Add(new Paragraph(label))
				.SetBold()
				.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
				.SetPadding(5));

			table.AddCell(new Cell()
				.Add(new Paragraph(value))
				.SetPadding(5));
		}






	}
}