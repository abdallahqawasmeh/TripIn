using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IPaymentRepository
	{
		Payment? GetPaymentById(int paymentId);
		void UpdateFlightId(int paymentId, int flightId);
	}
}
