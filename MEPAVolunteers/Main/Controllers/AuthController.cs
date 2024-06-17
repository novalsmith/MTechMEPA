using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Main.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Volunteer.Interface;
using Volunteer.Utility;

namespace Main.Controller
{
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IVolunteerService _service;

        public AuthController(ILogger<AuthController> logger, IVolunteerService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            // Validasi kredensial user (misalnya, periksa database)
            var resultData = await _service.ValidateVolunteer(login);
            if (resultData != null && (login.Username == resultData.VolunteerID))
            {
                // Jika kredensial valid, buat token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("UWETU23507953NOVALSMITH8793485!!##2739458");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {

                        new Claim(VolunteerClaimTypes.VolunteerID, resultData.VolunteerID),
                        new Claim(VolunteerClaimTypes.Email, resultData.Email),
                        new Claim(VolunteerClaimTypes.Name, resultData.Name),
                        new Claim(VolunteerClaimTypes.Role, resultData.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "https://mosframtech.com",
                    Audience = "https://mosframtech.com",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Kembalikan token ke client
                return Ok(new { Token = string.Format("Bearer {0}", tokenString) });
            }
            else
            {
                // Jika kredensial tidak valid, kembalikan respon Unauthorized
                return Unauthorized();
            }
        }
        [HttpPost("relawan/register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
            register.Password = hashedPassword;
            var result = await _service.RegisterVolunteer(register, Role.VOLUNTEER);
            if (result)
            {
                return Ok("Relawan berhasil terdaftar");
            }
            else {
                return BadRequest("Relawan gagal terdaftar.");
            }
        }

        [HttpPost("pemilih/register")]
        public async Task<IActionResult> VotesRegister(RegisterModel register)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
            register.Password = hashedPassword;
            var result = await _service.RegisterVolunteer(register, Role.VOTER);
            if (result)
            {
                return Ok("Pemilih berhasil terdaftar");
            }
            else
            {
                return BadRequest("Pemilih gagal terdaftar.");
            }
        }
    }
}