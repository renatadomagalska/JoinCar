using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;

namespace JoinCar.Database.Repositories.Interfaces
{
    public interface IOpinionsRepository
    {
        void AddOpinion(Opinion opinion);
        void EditOpinion(Opinion opinion);
        void DeleteOpinion(int opinionId);
        void Save();
        ICollection<Opinion> GetUserReceivedOpinions(string userId);
        ICollection<Opinion> GetUserIssuedOpinions(string userId);
    }
}
