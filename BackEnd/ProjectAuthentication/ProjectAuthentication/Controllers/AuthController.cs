using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectAuthentication.Dtos;
using ProjectAuthentication.Helpers;
using ProjectAuthentication.Models;
using ProjectAuthentication.Repositories.Contract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthController(IAuthRepository authRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Método para registrar novos usuários
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            registerDto.Email = registerDto.Email.ToLower();
            if (await _authRepository.UserExists(registerDto.Email))
                return BadRequest("Email já existe");

            var userToCreate = _mapper.Map<TblUser>(registerDto);
            var createdUser = await _authRepository.Register(userToCreate, registerDto.Password);
            return StatusCode(201, new { email = createdUser.Email, fullname = createdUser.FullName });
        }

        /// <summary>
        /// Método de login com autenticação jwt
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userFromRepo = await _authRepository.Login(loginDto.Email, loginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userFromRepo.UserId.ToString()),
                    new Claim(ClaimTypes.Role, userFromRepo.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            userFromRepo.Password = null;

            return Ok(new { token = tokenHandler.WriteToken(token), email = userFromRepo.Email, fullname = userFromRepo.FullName });
        }
    }
}
