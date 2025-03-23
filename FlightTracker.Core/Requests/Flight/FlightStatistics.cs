namespace FlightTracker.Core.Requests.Flight
{
	public class FlightStatistics
	{
		public decimal Profit { get; set; }
		public decimal Lose { get; set; }
		public decimal RemainingSeats { get; set; }
		public decimal PassengersCount { get; set; }
	}

}