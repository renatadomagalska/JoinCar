using System;
using System.Collections.Generic;
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


    }
}
