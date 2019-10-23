using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersControl.Entities;

namespace UsersControl.Services.Contracts
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
