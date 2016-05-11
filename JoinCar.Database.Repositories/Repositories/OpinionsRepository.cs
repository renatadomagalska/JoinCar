using System;
using System.Data.Entity;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;

namespace JoinCar.Database.Repositories.Repositories
{
    public class OpinionsRepository : IOpinionsRepository
    {
        readonly ApplicationDbContext _context = new ApplicationDbContext();

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
    }
}
