using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    RoleId = u.RoleId,
                    RoleName = u.Role != null ? u.Role.RoleName : string.Empty,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email
                })
                .ToListAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var u = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            if (u is null) return NotFound();
            return Ok(new UserDto
            {
                Id = u.Id,
                RoleId = u.RoleId,
                RoleName = u.Role?.RoleName ?? string.Empty,
                FullName = u.FullName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email
            });
        }

        // GET: api/users/by-role/2
        [HttpGet("by-role/{roleId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByRole(int roleId)
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == roleId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    RoleId = u.RoleId,
                    RoleName = u.Role != null ? u.Role.RoleName : string.Empty,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email
                })
                .ToListAsync();
            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
        {
            var roleExists = await _context.Roles.AnyAsync(r => r.Id == dto.RoleId);
            if (!roleExists) return BadRequest($"Роль с Id={dto.RoleId} не найдена.");

            var user = new User
            {
                RoleId = dto.RoleId,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserDto
            {
                Id = user.Id,
                RoleId = user.RoleId,
                RoleName = string.Empty,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            });
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return NotFound();

            var roleExists = await _context.Roles.AnyAsync(r => r.Id == dto.RoleId);
            if (!roleExists) return BadRequest($"Роль с Id={dto.RoleId} не найдена.");

            user.RoleId = dto.RoleId;
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ── Вспомогательный хэш ──────────────────────────────────────────────
        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}
