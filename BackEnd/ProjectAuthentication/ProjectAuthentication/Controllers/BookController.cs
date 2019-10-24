using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAuthentication.Entities;
using ProjectAuthentication.Models;
using System.Threading.Tasks;

namespace ProjectAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var data = await _context.TblBook.ToListAsync();

            if (data == null)
                return NotFound();

            //if (!User.IsInRole(Role.Admin))
            //    return Forbid();

            return Ok(data);
        }
    }
}
