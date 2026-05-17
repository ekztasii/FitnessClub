using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientMembershipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientMembershipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clientmemberships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientMembershipDto>>> GetAll()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var list = await _context.ClientMemberships
                .Include(cm => cm.User)
                .Include(cm => cm.MembershipPlan)
                .Select(cm => new ClientMembershipDto
                {
                    Id = cm.Id,
                    UserId = cm.UserId,
                    ClientName = cm.User != null ? cm.User.FullName : string.Empty,
                    MembershipPlanId = cm.MembershipPlanId,
                    PlanName = cm.MembershipPlan != null ? cm.MembershipPlan.PlanName : string.Empty,
                    PurchaseDate = cm.PurchaseDate,
                    ExpiryDate = cm.ExpiryDate,
                    VisitsUsed = cm.VisitsUsed,
                    AllowedVisits = cm.MembershipPlan != null ? cm.MembershipPlan.AllowedVisits : 0,
                    IsActive = cm.ExpiryDate >= today &&
                               (cm.MembershipPlan == null || cm.VisitsUsed < cm.MembershipPlan.AllowedVisits)
                })
                .ToListAsync();
            return Ok(list);
        }

        // GET: api/clientmemberships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientMembershipDto>> GetById(int id)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var cm = await _context.ClientMemberships
                .Include(c => c.User)
                .Include(c => c.MembershipPlan)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cm is null) return NotFound();
            return Ok(MapToDto(cm, today));
        }

        // GET: api/clientmemberships/by-client/3
        [HttpGet("by-client/{userId}")]
        public async Task<ActionResult<IEnumerable<ClientMembershipDto>>> GetByClient(int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var list = await _context.ClientMemberships
                .Include(cm => cm.User)
                .Include(cm => cm.MembershipPlan)
                .Where(cm => cm.UserId == userId)
                .ToListAsync();
            return Ok(list.Select(cm => MapToDto(cm, today)));
        }

        // GET: api/clientmemberships/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ClientMembershipDto>>> GetActive()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var list = await _context.ClientMemberships
                .Include(cm => cm.User)
                .Include(cm => cm.MembershipPlan)
                .Where(cm => cm.ExpiryDate >= today)
                .ToListAsync();
            // Дополнительная фильтрация по лимиту посещений в памяти
            var active = list
                .Where(cm => cm.MembershipPlan == null || cm.VisitsUsed < cm.MembershipPlan.AllowedVisits)
                .Select(cm => MapToDto(cm, today));
            return Ok(active);
        }

        // POST: api/clientmemberships
        [HttpPost]
        public async Task<ActionResult<ClientMembershipDto>> Create(CreateClientMembershipDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists) return BadRequest($"Пользователь с Id={dto.UserId} не найден.");

            var plan = await _context.MembershipPlans.FindAsync(dto.MembershipPlanId);
            if (plan is null) return BadRequest($"План абонемента с Id={dto.MembershipPlanId} не найден.");

            var expiryDate = dto.PurchaseDate.AddDays(plan.ValidityDays);

            var membership = new ClientMembership
            {
                UserId = dto.UserId,
                MembershipPlanId = dto.MembershipPlanId,
                PurchaseDate = dto.PurchaseDate,
                ExpiryDate = expiryDate,
                VisitsUsed = 0
            };
            _context.ClientMemberships.Add(membership);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = membership.Id }, await GetById(membership.Id));
        }

        // PATCH: api/clientmemberships/5/increment-visit
        [HttpPatch("{id}/increment-visit")]
        public async Task<IActionResult> IncrementVisit(int id)
        {
            var cm = await _context.ClientMemberships
                .Include(c => c.MembershipPlan)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cm is null) return NotFound();

            var today = DateOnly.FromDateTime(DateTime.Today);
            if (cm.ExpiryDate < today)
                return BadRequest("Абонемент истёк.");
            if (cm.MembershipPlan != null && cm.VisitsUsed >= cm.MembershipPlan.AllowedVisits)
                return BadRequest("Лимит посещений исчерпан.");

            cm.VisitsUsed++;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/clientmemberships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cm = await _context.ClientMemberships.FindAsync(id);
            if (cm is null) return NotFound();
            _context.ClientMemberships.Remove(cm);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private static ClientMembershipDto MapToDto(ClientMembership cm, DateOnly today) => new()
        {
            Id = cm.Id,
            UserId = cm.UserId,
            ClientName = cm.User?.FullName ?? string.Empty,
            MembershipPlanId = cm.MembershipPlanId,
            PlanName = cm.MembershipPlan?.PlanName ?? string.Empty,
            PurchaseDate = cm.PurchaseDate,
            ExpiryDate = cm.ExpiryDate,
            VisitsUsed = cm.VisitsUsed,
            AllowedVisits = cm.MembershipPlan?.AllowedVisits ?? 0,
            IsActive = cm.ExpiryDate >= today &&
                       (cm.MembershipPlan == null || cm.VisitsUsed < cm.MembershipPlan.AllowedVisits)
        };
    }
}
