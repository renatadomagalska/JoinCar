using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;

namespace JoinCar.Database.Repositories.Repositories
{
    public class TripsRepository : ITripsRepository
    {
        readonly ApplicationDbContext _context = new ApplicationDbContext();

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
           return _context.Trips.Where(t => t.DateTime >= DateTime.Now).ToList();
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
    }
}
