using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;

namespace JoinCar.Database.Repositories.Repositories
{
    public class OpinionsRepository : IOpinionsRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddOpinion(Opinion opinion)
        {
            _context.Opinions.Add(opinion);
        }

        public void EditOpinion(Opinion opinion)
        {
            _context.Entry(opinion).State = EntityState.Modified;
        }

        public void DeleteOpinion(int opinionId)
        {
            var opinion = _context.Opinions.Find(opinionId);
            _context.Opinions.Remove(opinion);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public ICollection<Opinion> GetUserReceivedOpinions(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new Exception();
            return (from o in _context.Opinions
                join tr in _context.Trips on o.TripId equals tr.Id
                where tr.UserId == userId
                select o).ToList();
        }

        public ICollection<Opinion> GetUserIssuedOpinions(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new Exception();
            return _context.Opinions.Where(o => o.UserIssuingOpinionId == userId).ToList();
        }
    }
}
