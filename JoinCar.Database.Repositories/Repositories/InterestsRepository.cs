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
        readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddInterest(Interest interest)
        {
            _context.Interets.Add(interest);
        }

        public void DeleteInterest(int interestId)
        {
            var interest = _context.Interets.Find(interestId);
            _context.Interets.Remove(interest);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public ICollection<Interest> GetInterestsByUserId(string userId)
        {
            return _context.Interets.Where(i => i.User.Id == userId).ToList();
        }
    }
}
