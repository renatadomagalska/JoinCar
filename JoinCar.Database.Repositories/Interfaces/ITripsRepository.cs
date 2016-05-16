using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;

namespace JoinCar.Database.Repositories.Interfaces
{
    public interface ITripsRepository
    {
        void AddTrip(Trip trip);
        void EditTrip(Trip trip);
        void DeleteTrip(int tripId);
        void Save();
        ICollection<Trip> GetAllActiveTrips();
        ICollection<Trip> GetAllArchivedTrips();
        Trip GetTripById(int id);
        ICollection<Trip> GetUserTrips(string userId);

        ICollection<Trip> GetActiveFilteredTrips(string searchStringFrom, string searchStringTo,
            DateTime? searchStartDate, DateTime? searchEndDate);

        ICollection<Trip> GetArchivedFilteredTrips(string searchStringFrom, string searchStringTo,
            DateTime? searchStartDate, DateTime? searchEndDate);

        ICollection<ApplicationUser> GetTripPassengers(int id);
        void IncrementAvailableSeats(int id);
        void DecrementAvailableSeats(int id);
    }
}
