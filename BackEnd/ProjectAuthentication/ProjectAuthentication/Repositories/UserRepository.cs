using Microsoft.EntityFrameworkCore;
using ProjectAuthentication.Models;
using ProjectAuthentication.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAuthentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreContext _context;

        public UserRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<TblUser> ObterUsuario(string email)
        {
            var user = await _context.TblUser.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            return user;
        }

        public async Task<TblUser> AlterarPermissao(TblUser user, string role)
        {
            user.Role = role;
            _context.TblUser.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
