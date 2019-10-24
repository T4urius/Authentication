using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] UserDto userDto)
        {
            var data = await _userRepository.ObterUsuario(userDto.Email);

            if (data == null)
                return NotFound();

            //if (!User.IsInRole(Role.Admin))
            //    return Forbid();

            return StatusCode(201, new { data.UserId, data.Email, data.FullName, data.Role } );
        }

        [HttpPut("alterar")]
        public async Task<IActionResult> AlterRole(AlterRoleDto alterRoleDto)
        {
            var data = await _userRepository.ObterUsuario(alterRoleDto.Email);

            if (data == null)
                return NotFound();

            var userToAlter = _mapper.Map<TblUser>(data);
            var alterRole = await _userRepository.AlterarPermissao(userToAlter, alterRoleDto.Role);


            return StatusCode(201, new { alterRole.Role, alterRole.Email });
        }
    }
}

