using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.Admin;
using FlightTracker.Core.Service;
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

      
    }
}