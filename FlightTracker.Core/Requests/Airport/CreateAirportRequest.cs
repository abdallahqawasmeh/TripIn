namespace FlightTracker.API.Requests.Airport
{
	public class CreateAirportRequest
	{

        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal? Status { get; set; } = 1;  //{0:disapble,1:enebled}  
    }
}
