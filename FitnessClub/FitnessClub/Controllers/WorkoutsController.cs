using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetAll()
        {
            var workouts = await _context.Workouts
                .Include(w => w.WorkoutType)
                .Include(w => w.User)
                .Include(w => w.WorkoutRegistrations)
                .Select(w => new WorkoutDto
                {
                    Id = w.Id,
                    WorkoutTypeId = w.WorkoutTypeId,
                    WorkoutTypeName = w.WorkoutType != null ? w.WorkoutType.TypeName : string.Empty,
                    UserId = w.UserId,
                    TrainerName = w.User != null ? w.User.FullName : string.Empty,
                    WorkoutDate = w.WorkoutDate,
                    MaxParticipants = w.MaxParticipants,
                    CurrentParticipants = w.WorkoutRegistrations.Count
                })
                .ToListAsync();
            return Ok(workouts);
        }

        // GET: api/workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetById(int id)
        {
            var w = await _context.Workouts
                .Include(x => x.WorkoutType)
                .Include(x => x.User)
                .Include(x => x.WorkoutRegistrations)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (w is null) return NotFound();
            return Ok(new WorkoutDto
            {
                Id = w.Id,
                WorkoutTypeId = w.WorkoutTypeId,
                WorkoutTypeName = w.WorkoutType?.TypeName ?? string.Empty,
                UserId = w.UserId,
                TrainerName = w.User?.FullName ?? string.Empty,
                WorkoutDate = w.WorkoutDate,
                MaxParticipants = w.MaxParticipants,
                CurrentParticipants = w.WorkoutRegistrations.Count
            });
        }

        // GET: api/workouts/by-trainer/3
        [HttpGet("by-trainer/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetByTrainer(int userId)
        {
            var workouts = await _context.Workouts
                .Include(w => w.WorkoutType)
                .Include(w => w.User)
                .Include(w => w.WorkoutRegistrations)
                .Where(w => w.UserId == userId)
                .Select(w => new WorkoutDto
                {
                    Id = w.Id,
                    WorkoutTypeId = w.WorkoutTypeId,
                    WorkoutTypeName = w.WorkoutType != null ? w.WorkoutType.TypeName : string.Empty,
                    UserId = w.UserId,
                    TrainerName = w.User != null ? w.User.FullName : string.Empty,
                    WorkoutDate = w.WorkoutDate,
                    MaxParticipants = w.MaxParticipants,
                    CurrentParticipants = w.WorkoutRegistrations.Count
                })
                .ToListAsync();
            return Ok(workouts);
        }

        // GET: api/workouts/upcoming
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetUpcoming()
        {
            var now = DateTime.UtcNow;
            var workouts = await _context.Workouts
                .Include(w => w.WorkoutType)
                .Include(w => w.User)
                .Include(w => w.WorkoutRegistrations)
                .Where(w => w.WorkoutDate >= now)
                .OrderBy(w => w.WorkoutDate)
                .Select(w => new WorkoutDto
                {
                    Id = w.Id,
                    WorkoutTypeId = w.WorkoutTypeId,
                    WorkoutTypeName = w.WorkoutType != null ? w.WorkoutType.TypeName : string.Empty,
                    UserId = w.UserId,
                    TrainerName = w.User != null ? w.User.FullName : string.Empty,
                    WorkoutDate = w.WorkoutDate,
                    MaxParticipants = w.MaxParticipants,
                    CurrentParticipants = w.WorkoutRegistrations.Count
                })
                .ToListAsync();
            return Ok(workouts);
        }

        // POST: api/workouts
        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create(CreateWorkoutDto dto)
        {
            var typeExists = await _context.WorkoutTypes.AnyAsync(wt => wt.Id == dto.WorkoutTypeId);
            if (!typeExists) return BadRequest($"Тип тренировки с Id={dto.WorkoutTypeId} не найден.");

            var trainerExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!trainerExists) return BadRequest($"Пользователь с Id={dto.UserId} не найден.");

            var workout = new Workout
            {
                WorkoutTypeId = dto.WorkoutTypeId,
                UserId = dto.UserId,
                WorkoutDate = dto.WorkoutDate,
                MaxParticipants = dto.MaxParticipants
            };
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = workout.Id }, await GetById(workout.Id));
        }

        // PUT: api/workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateWorkoutDto dto)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout is null) return NotFound();

            var typeExists = await _context.WorkoutTypes.AnyAsync(wt => wt.Id == dto.WorkoutTypeId);
            if (!typeExists) return BadRequest($"Тип тренировки с Id={dto.WorkoutTypeId} не найден.");

            var trainerExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!trainerExists) return BadRequest($"Пользователь с Id={dto.UserId} не найден.");

            workout.WorkoutTypeId = dto.WorkoutTypeId;
            workout.UserId = dto.UserId;
            workout.WorkoutDate = dto.WorkoutDate;
            workout.MaxParticipants = dto.MaxParticipants;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout is null) return NotFound();
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
