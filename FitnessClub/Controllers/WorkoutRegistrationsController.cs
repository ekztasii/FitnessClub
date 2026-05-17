using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutRegistrationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/workoutregistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutRegistrationDto>>> GetAll()
        {
            var registrations = await _context.WorkoutRegistrations
                .Include(wr => wr.Workout).ThenInclude(w => w!.WorkoutType)
                .Include(wr => wr.User)
                .Select(wr => new WorkoutRegistrationDto
                {
                    Id = wr.Id,
                    WorkoutId = wr.WorkoutId,
                    WorkoutDate = wr.Workout != null ? wr.Workout.WorkoutDate : default,
                    WorkoutTypeName = wr.Workout != null && wr.Workout.WorkoutType != null
                        ? wr.Workout.WorkoutType.TypeName : string.Empty,
                    UserId = wr.UserId,
                    ClientName = wr.User != null ? wr.User.FullName : string.Empty,
                    RegistrationDate = wr.RegistrationDate,
                    AttendanceMarked = wr.AttendanceMarked
                })
                .ToListAsync();
            return Ok(registrations);
        }

        // GET: api/workoutregistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutRegistrationDto>> GetById(int id)
        {
            var wr = await _context.WorkoutRegistrations
                .Include(r => r.Workout).ThenInclude(w => w!.WorkoutType)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (wr is null) return NotFound();
            return Ok(MapToDto(wr));
        }

        // GET: api/workoutregistrations/by-workout/5
        [HttpGet("by-workout/{workoutId}")]
        public async Task<ActionResult<IEnumerable<WorkoutRegistrationDto>>> GetByWorkout(int workoutId)
        {
            var registrations = await _context.WorkoutRegistrations
                .Include(wr => wr.Workout).ThenInclude(w => w!.WorkoutType)
                .Include(wr => wr.User)
                .Where(wr => wr.WorkoutId == workoutId)
                .Select(wr => new WorkoutRegistrationDto
                {
                    Id = wr.Id,
                    WorkoutId = wr.WorkoutId,
                    WorkoutDate = wr.Workout != null ? wr.Workout.WorkoutDate : default,
                    WorkoutTypeName = wr.Workout != null && wr.Workout.WorkoutType != null
                        ? wr.Workout.WorkoutType.TypeName : string.Empty,
                    UserId = wr.UserId,
                    ClientName = wr.User != null ? wr.User.FullName : string.Empty,
                    RegistrationDate = wr.RegistrationDate,
                    AttendanceMarked = wr.AttendanceMarked
                })
                .ToListAsync();
            return Ok(registrations);
        }

        // GET: api/workoutregistrations/by-client/3
        [HttpGet("by-client/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutRegistrationDto>>> GetByClient(int userId)
        {
            var registrations = await _context.WorkoutRegistrations
                .Include(wr => wr.Workout).ThenInclude(w => w!.WorkoutType)
                .Include(wr => wr.User)
                .Where(wr => wr.UserId == userId)
                .Select(wr => new WorkoutRegistrationDto
                {
                    Id = wr.Id,
                    WorkoutId = wr.WorkoutId,
                    WorkoutDate = wr.Workout != null ? wr.Workout.WorkoutDate : default,
                    WorkoutTypeName = wr.Workout != null && wr.Workout.WorkoutType != null
                        ? wr.Workout.WorkoutType.TypeName : string.Empty,
                    UserId = wr.UserId,
                    ClientName = wr.User != null ? wr.User.FullName : string.Empty,
                    RegistrationDate = wr.RegistrationDate,
                    AttendanceMarked = wr.AttendanceMarked
                })
                .ToListAsync();
            return Ok(registrations);
        }

        // POST: api/workoutregistrations
        [HttpPost]
        public async Task<ActionResult<WorkoutRegistrationDto>> Create(CreateWorkoutRegistrationDto dto)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutRegistrations)
                .FirstOrDefaultAsync(w => w.Id == dto.WorkoutId);
            if (workout is null) return BadRequest($"Тренировка с Id={dto.WorkoutId} не найдена.");

            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists) return BadRequest($"Пользователь с Id={dto.UserId} не найден.");

            // Проверка на уже существующую запись
            var alreadyRegistered = await _context.WorkoutRegistrations
                .AnyAsync(wr => wr.WorkoutId == dto.WorkoutId && wr.UserId == dto.UserId);
            if (alreadyRegistered) return Conflict("Клиент уже записан на эту тренировку.");

            // Проверка доступных мест
            if (workout.WorkoutRegistrations.Count >= workout.MaxParticipants)
                return BadRequest("Нет свободных мест на тренировку.");

            var registration = new WorkoutRegistration
            {
                WorkoutId = dto.WorkoutId,
                UserId = dto.UserId,
                RegistrationDate = DateTime.UtcNow,
                AttendanceMarked = false
            };
            _context.WorkoutRegistrations.Add(registration);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = registration.Id }, await GetById(registration.Id));
        }

        // PATCH: api/workoutregistrations/5/attendance
        [HttpPatch("{id}/attendance")]
        public async Task<IActionResult> MarkAttendance(int id, MarkAttendanceDto dto)
        {
            var registration = await _context.WorkoutRegistrations.FindAsync(id);
            if (registration is null) return NotFound();
            registration.AttendanceMarked = dto.AttendanceMarked;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/workoutregistrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registration = await _context.WorkoutRegistrations.FindAsync(id);
            if (registration is null) return NotFound();
            _context.WorkoutRegistrations.Remove(registration);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private static WorkoutRegistrationDto MapToDto(WorkoutRegistration wr) => new()
        {
            Id = wr.Id,
            WorkoutId = wr.WorkoutId,
            WorkoutDate = wr.Workout?.WorkoutDate ?? default,
            WorkoutTypeName = wr.Workout?.WorkoutType?.TypeName ?? string.Empty,
            UserId = wr.UserId,
            ClientName = wr.User?.FullName ?? string.Empty,
            RegistrationDate = wr.RegistrationDate,
            AttendanceMarked = wr.AttendanceMarked
        };
    }
}
