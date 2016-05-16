using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;

namespace JoinCar.Database.Repositories.Repositories
{
    public class TripsRepository : ITripsRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddTrip(Trip trip)
        {
            _context.Trips.Add(trip);
        }

        public void EditTrip(Trip trip)
        {
            _context.Entry(trip).State = EntityState.Modified;
        }

        public void DeleteTrip(int tripId)
        {
            var trip = _context.Trips.Find(tripId);
            _context.Trips.Remove(trip);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public ICollection<Trip> GetAllActiveTrips()
        {
           return _context.Trips.Include("User").Where(t => t.DateTime >= DateTime.Now).ToList();
        }

        public ICollection<Trip> GetAllArchivedTrips()
        {
            return _context.Trips.Where(t => t.DateTime < DateTime.Now).ToList();
        }

        public Trip GetTripById(int id)
        {
            if (id != default(int))
                return _context.Trips.Find(id);
            else
                throw new Exception();
                //todo throw custom exception
        }

        public ICollection<Trip> GetUserTrips(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new Exception();
            return _context.Trips.Where(t => t.User.Id == userId).ToList();
        }

        public ICollection<Trip> GetActiveFilteredTrips(string searchStringFrom, string searchStringTo, DateTime? searchStartDate,
            DateTime? searchEndDate)
        {
            var trips = GetAllActiveTrips();
            if (!string.IsNullOrEmpty(searchStringFrom))
                trips = trips.Where(t => t.From.ToLower().Contains(searchStringFrom.ToLower())).ToList();
            if (!string.IsNullOrEmpty(searchStringTo))
                trips = trips.Where(t => t.To.ToLower().Contains(searchStringTo.ToLower())).ToList();
            if (searchStartDate != null)
                trips = trips.Where(t => t.DateTime >= searchStartDate.Value).ToList();
            if (searchEndDate != null)
                trips = trips.Where(t => t.DateTime >= searchEndDate.Value).ToList();
            return trips;
        }

        public ICollection<Trip> GetArchivedFilteredTrips(string searchStringFrom, string searchStringTo, DateTime? searchStartDate,
            DateTime? searchEndDate)
        {
            var trips = GetAllArchivedTrips();
            if (!string.IsNullOrEmpty(searchStringFrom))
                trips = trips.Where(t => t.From.Contains(searchStringFrom)).ToList();
            if (!string.IsNullOrEmpty(searchStringTo))
                trips = trips.Where(t => t.To.Contains(searchStringTo)).ToList();
            if (searchStartDate != null)
                trips = trips.Where(t => t.DateTime >= searchStartDate.Value).ToList();
            if (searchEndDate != null)
                trips = trips.Where(t => t.DateTime >= searchEndDate.Value).ToList();
            return trips;
        }

        public ICollection<ApplicationUser> GetTripPassengers(int id)
        {
            return (from u in _context.Users
                    join intr in _context.Interests on u.Id equals intr.UserId
                    where intr.TripId == id
                    select u).ToList();
        }
    }
}
