using FitnessClub.Data;
using FitnessClub.DTOs;
using FitnessClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipPlansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MembershipPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/membershipplans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipPlanDto>>> GetAll()
        {
            var plans = await _context.MembershipPlans
                .Select(mp => new MembershipPlanDto
                {
                    Id = mp.Id,
                    PlanName = mp.PlanName,
                    Price = mp.Price,
                    ValidityDays = mp.ValidityDays,
                    AllowedVisits = mp.AllowedVisits
                })
                .ToListAsync();
            return Ok(plans);
        }

        // GET: api/membershipplans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipPlanDto>> GetById(int id)
        {
            var mp = await _context.MembershipPlans.FindAsync(id);
            if (mp is null) return NotFound();
            return Ok(new MembershipPlanDto
            {
                Id = mp.Id,
                PlanName = mp.PlanName,
                Price = mp.Price,
                ValidityDays = mp.ValidityDays,
                AllowedVisits = mp.AllowedVisits
            });
        }

        // POST: api/membershipplans
        [HttpPost]
        public async Task<ActionResult<MembershipPlanDto>> Create(CreateMembershipPlanDto dto)
        {
            var plan = new MembershipPlan
            {
                PlanName = dto.PlanName,
                Price = dto.Price,
                ValidityDays = dto.ValidityDays,
                AllowedVisits = dto.AllowedVisits
            };
            _context.MembershipPlans.Add(plan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, new MembershipPlanDto
            {
                Id = plan.Id,
                PlanName = plan.PlanName,
                Price = plan.Price,
                ValidityDays = plan.ValidityDays,
                AllowedVisits = plan.AllowedVisits
            });
        }

        // PUT: api/membershipplans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMembershipPlanDto dto)
        {
            var plan = await _context.MembershipPlans.FindAsync(id);
            if (plan is null) return NotFound();
            plan.PlanName = dto.PlanName;
            plan.Price = dto.Price;
            plan.ValidityDays = dto.ValidityDays;
            plan.AllowedVisits = dto.AllowedVisits;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/membershipplans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _context.MembershipPlans.FindAsync(id);
            if (plan is null) return NotFound();
            _context.MembershipPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
