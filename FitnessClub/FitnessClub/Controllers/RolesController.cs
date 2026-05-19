using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _context.Roles
                .Select(r => new RoleDto { Id = r.Id, RoleName = r.RoleName })
                .ToListAsync();
            return Ok(roles);
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null) return NotFound();
            return Ok(new RoleDto { Id = role.Id, RoleName = role.RoleName });
        }

        // POST: api/roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(CreateRoleDto dto)
        {
            var role = new Role { RoleName = dto.RoleName };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            var result = new RoleDto { Id = role.Id, RoleName = role.RoleName };
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, result);
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateRoleDto dto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null) return NotFound();
            role.RoleName = dto.RoleName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null) return NotFound();
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
