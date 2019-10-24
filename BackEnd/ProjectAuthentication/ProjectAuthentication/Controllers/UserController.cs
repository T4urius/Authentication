using Microsoft.AspNetCore.Mvc;
using ProjectAuthentication.Dtos;
using ProjectAuthentication.Models;
using ProjectAuthentication.Repositories.Contract;
using System.Threading.Tasks;

namespace ProjectAuthentication.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] UserDto userDto)
        {
            var data = await _userRepository.ObterUsuario(userDto.IdUser);

            if (data == null)
                return NotFound();

            //if (!User.IsInRole(Role.Admin))
            //    return Forbid();

            return StatusCode(201, new { data.Email, data.FullName } );
        }
    }
}

