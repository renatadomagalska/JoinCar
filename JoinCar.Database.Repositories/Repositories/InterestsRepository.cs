using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;

namespace JoinCar.Database.Repositories.Repositories
{
    public class InterestsRepository : IInterestsRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddInterest(Interest interest)
        {
            _context.Interests.Add(interest);
        }

        public void DeleteInterest(int interestId)
        {
            var interest = _context.Interests.Find(interestId);
            _context.Interests.Remove(interest);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public ICollection<Interest> GetInterestsByUserId(string userId)
        {
            return _context.Interests.Where(i => i.UserId == userId).ToList();
        }

        public Interest GetIntrestById(int id)
        {
            return _context.Interests.Find(id);
        }

        public Interest GetInterestByTripAndUserIds(int tripId, string userId)
        {
            return _context.Interests.FirstOrDefault(i => i.TripId == tripId && i.UserId == userId);
        }
    }
}
