using ProjectAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAuthentication.Repositories.Contract
{
    public interface IUserRepository
    {
        Task<TblUser> ObterUsuarioPorId(int id);
        Task<TblUser> AlterarPermissao(TblUser user, string role);
    }
}
