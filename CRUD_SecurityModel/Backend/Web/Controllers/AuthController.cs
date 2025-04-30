using Business.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _context.Set<User>()
                .Include(u => u.RolUsers)
                .ThenInclude(ru => ru.Rol)
                .FirstOrDefaultAsync(u => u.Username == dto.Username && u.Password == dto.Password && u.Active);

            if (user == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            var rol = user.RolUsers.FirstOrDefault()?.Rol?.Name ?? "User";

            var token = _tokenService.GenerateToken(user.Username, rol, user.Id);

            return Ok(new
            {
                token,
                username = user.Username,
                userId = user.Id,
                rol
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var person = new Person
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                DocumentNumber = dto.DocumentNumber,
                Phone = dto.Phone,
                Address = dto.Address,
                DocumentType = dto.DocumentType,
                BloodType = dto.BloodType,
                Active = true
            };
            await _context.Set<Person>().AddAsync(person);
            await _context.SaveChangesAsync();

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                PersonId = person.Id,
                Active = true
            };
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();

            var rolUser = new RolUser
            {
                UserId = user.Id,
                RolId = dto.RolId,
                Active = true
            };
            await _context.Set<RolUser>().AddAsync(rolUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro exitoso" });
        }

    }
}
