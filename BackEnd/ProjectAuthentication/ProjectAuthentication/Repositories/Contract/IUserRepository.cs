using ProjectAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAuthentication.Repositories.Contract
{
    public interface IUserRepository
    {
        Task<TblUser> ObterUsuario(int id); 
    }
}
