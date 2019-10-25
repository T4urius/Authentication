using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAuthentication.Dtos;
using ProjectAuthentication.Models;
using ProjectAuthentication.Repositories.Contract;
using System.Threading.Tasks;

namespace ProjectAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookStoreContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(BookStoreContext context, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByEmail([FromQuery] UserDto userDto)
        {
            var data = await _userRepository.ObterUsuarioPorEmail(userDto.Email);

            if (data == null)
                return NotFound();

            //if (!User.IsInRole(Role.Admin))
            //    return Forbid();

            return StatusCode(201, new { data.UserId, data.Email, data.FullName, data.Role });
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.TblUser.ToListAsync();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPut("alterar")]
        public async Task<IActionResult> AlterRole(AlterRoleDto alterRoleDto)
        {
            var data = await _userRepository.ObterUsuarioPorEmail(alterRoleDto.Email);

            if (data == null)
                return NotFound();

            var userToAlter = _mapper.Map<TblUser>(data);
            var alterRole = await _userRepository.AlterarPermissao(userToAlter, alterRoleDto.Role);


            return StatusCode(201, new { alterRole.Role, alterRole.Email });
        }
    }
}

