using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/workouttypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutTypeDto>>> GetAll()
        {
            var types = await _context.WorkoutTypes
                .Include(wt => wt.User)
                .Select(wt => new WorkoutTypeDto
                {
                    Id = wt.Id,
                    WorkoutTypeLvl = wt.WorkoutTypeLvl,
                    UserId = wt.UserId,
                    TrainerName = wt.User != null ? wt.User.FullName : string.Empty,
                    TypeName = wt.TypeName,
                    Description = wt.Description
                })
                .ToListAsync();
            return Ok(types);
        }

        // GET: api/workouttypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutTypeDto>> GetById(int id)
        {
            var wt = await _context.WorkoutTypes
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (wt is null) return NotFound();
            return Ok(new WorkoutTypeDto
            {
                Id = wt.Id,
                WorkoutTypeLvl = wt.WorkoutTypeLvl,
                UserId = wt.UserId,
                TrainerName = wt.User?.FullName ?? string.Empty,
                TypeName = wt.TypeName,
                Description = wt.Description
            });
        }

        // GET: api/workouttypes/by-trainer/3
        [HttpGet("by-trainer/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutTypeDto>>> GetByTrainer(int userId)
        {
            var types = await _context.WorkoutTypes
                .Include(wt => wt.User)
                .Where(wt => wt.UserId == userId)
                .Select(wt => new WorkoutTypeDto
                {
                    Id = wt.Id,
                    WorkoutTypeLvl = wt.WorkoutTypeLvl,
                    UserId = wt.UserId,
                    TrainerName = wt.User != null ? wt.User.FullName : string.Empty,
                    TypeName = wt.TypeName,
                    Description = wt.Description
                })
                .ToListAsync();
            return Ok(types);
        }

        // POST: api/workouttypes
        [HttpPost]
        public async Task<ActionResult<WorkoutTypeDto>> Create(CreateWorkoutTypeDto dto)
        {
            var trainerExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!trainerExists) return BadRequest($"Пользователь с Id={dto.UserId} не найден.");

            var wt = new WorkoutType
            {
                WorkoutTypeLvl = dto.WorkoutTypeLvl,
                UserId = dto.UserId,
                TypeName = dto.TypeName,
                Description = dto.Description
            };
            _context.WorkoutTypes.Add(wt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = wt.Id }, await GetById(wt.Id));
        }

        // PUT: api/workouttypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateWorkoutTypeDto dto)
        {
            var wt = await _context.WorkoutTypes.FindAsync(id);
            if (wt is null) return NotFound();

            var trainerExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!trainerExists) return BadRequest($"Пользователь с Id={dto.UserId} не найден.");

            wt.WorkoutTypeLvl = dto.WorkoutTypeLvl;
            wt.UserId = dto.UserId;
            wt.TypeName = dto.TypeName;
            wt.Description = dto.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/workouttypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var wt = await _context.WorkoutTypes.FindAsync(id);
            if (wt is null) return NotFound();
            _context.WorkoutTypes.Remove(wt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
