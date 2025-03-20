using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IReservationRepository
	{
		List<Reservation> GetAllReservations();
		Reservation? GetReservationById(int reservationId);
		List<Reservation> GetReservationsByUser(int userId);
		List<Reservation> GetReservationsByFlight(int flightId);
		void CreateReservation(Reservation reservation);
		void UpdateReservation(Reservation reservation);
		void DeleteReservation(int reservationId);



	}
}
