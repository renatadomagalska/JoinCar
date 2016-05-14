using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinCar.Database.Entities;

namespace JoinCar.Database.Repositories.Interfaces
{
    public interface IApplicationUsersRepository
    {
        ApplicationUser GetApplicationUserById(string id);
    }
}
