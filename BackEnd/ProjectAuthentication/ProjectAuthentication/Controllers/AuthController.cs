using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectAuthentication.Dtos;
using ProjectAuthentication.Models;
using ProjectAuthentication.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration, IMapper mapper)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para registrar novos usuários
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
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
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userFromRepo = await _authRepository.Login(loginDto.Email, loginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), email = userFromRepo.Email, fullname = userFromRepo.FullName });
        }
    }
}
