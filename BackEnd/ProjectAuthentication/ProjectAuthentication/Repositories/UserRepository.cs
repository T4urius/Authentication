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
        public async Task<TblUser> ObterUsuario(int id)
        {
            var user = await _context.TblUser.FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
                return null;

            return user;
        }
    }
}
