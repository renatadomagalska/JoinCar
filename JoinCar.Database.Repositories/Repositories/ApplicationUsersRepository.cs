using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;

namespace JoinCar.Database.Repositories.Repositories
{
    public class ApplicationUsersRepository : IApplicationUsersRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ApplicationUser GetApplicationUserById(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
