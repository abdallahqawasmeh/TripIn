namespace FlightTracker.Infra.Service
{
	public interface IClaimsReader
	{


		string? GetByClaimType(string claimType);

	}
}