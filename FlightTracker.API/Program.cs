using FlightTracker.Core.Common;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Service;
using FlightTracker.Infra.Common;
using FlightTracker.Infra.Repository;
using FlightTracker.Infra.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();





builder.Services.AddScoped<IAirportService,AirportService>();
builder.Services.AddScoped<ICompanyService,CompanyService >();

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.Configure<JsonOptions>(options =>
{
	options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});






builder.Services.AddAuthentication(opt => {

	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>

{

	options.TokenValidationParameters = new TokenValidationParameters

	{

		ValidateIssuer = false,

		ValidateAudience = false,

		ValidateLifetime = true,

		ValidateIssuerSigningKey = true,

		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"))

	};

});




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
