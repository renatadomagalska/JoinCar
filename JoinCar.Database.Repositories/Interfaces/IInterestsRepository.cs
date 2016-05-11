using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;

namespace JoinCar.Database.Repositories.Interfaces
{
    public interface IInterestsRepository
    {
        void AddInterest(Interest interest);
        void DeleteInterest(int interestId);
        void Save();
        ICollection<Interest> GetInterestsByUserId(string userId);
    }
}
