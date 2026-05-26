using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoolContributorsController : ControllerBase
    {
        private readonly SavingsPoolContext _context;

        public PoolContributorsController(SavingsPoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoolContributorResponseDto>>> GetPoolContributors()
        {
            var contributors = await _context.PoolContributors
                .Include(pc => pc.SavingsPool)
                .Include(pc => pc.User)
                .Include(pc => pc.StatusContribution)
                .Select(pc => new PoolContributorResponseDto
                {
                    ContributorId = pc.ContributorId,
                    SavingsPoolId = pc.SavingsPoolId,
                    SavingsPoolTitle = pc.SavingsPool.Title,
                    UserId = pc.UserId,
                    UserName = pc.User.Name,
                    StatusId = pc.StatusId,
                    StatusName = pc.StatusContribution.StatusName,
                    Amount = pc.Amount,
                    ContributionDate = pc.ContributionDate
                })
                .ToListAsync();

            return Ok(contributors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PoolContributorResponseDto>> GetPoolContributor(int id)
        {
            var pc = await _context.PoolContributors
                .Include(pc => pc.SavingsPool)
                .Include(pc => pc.User)
                .Include(pc => pc.StatusContribution)
                .FirstOrDefaultAsync(pc => pc.ContributorId == id);

            if (pc == null)
                return NotFound();

            return Ok(new PoolContributorResponseDto
            {
                ContributorId = pc.ContributorId,
                SavingsPoolId = pc.SavingsPoolId,
                SavingsPoolTitle = pc.SavingsPool.Title,
                UserId = pc.UserId,
                UserName = pc.User.Name,
                StatusId = pc.StatusId,
                StatusName = pc.StatusContribution.StatusName,
                Amount = pc.Amount,
                ContributionDate = pc.ContributionDate
            });
        }

        [HttpPost]
        public async Task<ActionResult<PoolContributorResponseDto>> CreatePoolContributor(PoolContributorCreateDto dto)
        {
            if (!await _context.SavingsPools.AnyAsync(sp => sp.SavingsPoolsId == dto.SavingsPoolId))
                return BadRequest("Invalid SavingsPoolId");
            if (!await _context.Users.AnyAsync(u => u.UserId == dto.UserId))
                return BadRequest("Invalid UserId");
            if (!await _context.StatusContributions.AnyAsync(s => s.StatusId == dto.StatusId))
                return BadRequest("Invalid StatusId");

            var contributor = new PoolContributors
            {
                SavingsPoolId = dto.SavingsPoolId,
                UserId = dto.UserId,
                StatusId = dto.StatusId,
                Amount = dto.Amount,
                ContributionDate = DateTime.UtcNow
            };

            _context.PoolContributors.Add(contributor);

            var pool = await _context.SavingsPools.FindAsync(dto.SavingsPoolId);
            pool!.CurrentAmount += dto.Amount;

            await _context.SaveChangesAsync();

            var created = await _context.PoolContributors
                .Include(pc => pc.SavingsPool)
                .Include(pc => pc.User)
                .Include(pc => pc.StatusContribution)
                .FirstAsync(pc => pc.ContributorId == contributor.ContributorId);

            return CreatedAtAction(nameof(GetPoolContributor), new { id = contributor.ContributorId }, new PoolContributorResponseDto
            {
                ContributorId = created.ContributorId,
                SavingsPoolId = created.SavingsPoolId,
                SavingsPoolTitle = created.SavingsPool.Title,
                UserId = created.UserId,
                UserName = created.User.Name,
                StatusId = created.StatusId,
                StatusName = created.StatusContribution.StatusName,
                Amount = created.Amount,
                ContributionDate = created.ContributionDate
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoolContributor(int id, PoolContributorUpdateDto dto)
        {
            var contributor = await _context.PoolContributors.FindAsync(id);
            if (contributor == null)
                return NotFound();

            var pool = await _context.SavingsPools.FindAsync(contributor.SavingsPoolId);
            if (pool != null)
            {
                pool.CurrentAmount -= contributor.Amount;
                pool.CurrentAmount += dto.Amount;
            }

            contributor.StatusId = dto.StatusId;
            contributor.Amount = dto.Amount;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoolContributor(int id)
        {
            var contributor = await _context.PoolContributors.FindAsync(id);
            if (contributor == null)
                return NotFound();

            var pool = await _context.SavingsPools.FindAsync(contributor.SavingsPoolId);
            if (pool != null)
                pool.CurrentAmount -= contributor.Amount;

            _context.PoolContributors.Remove(contributor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
