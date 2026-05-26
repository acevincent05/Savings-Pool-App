using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavingsPoolsController : ControllerBase
    {
        private readonly SavingsPoolContext _context;

        public SavingsPoolsController(SavingsPoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavingsPoolResponseDto>>> GetSavingsPools()
        {
            var pools = await _context.SavingsPools
                .Include(sp => sp.SchedType)
                .Include(sp => sp.Contributors)
                .Select(sp => new SavingsPoolResponseDto
                {
                    SavingsPoolsId = sp.SavingsPoolsId,
                    Title = sp.Title,
                    TargetAmount = sp.TargetAmount,
                    CurrentAmount = sp.CurrentAmount,
                    SchedTypeId = sp.SchedTypeId,
                    SchedTypeName = sp.SchedType.Name,
                    ContributorCount = sp.Contributors.Count,
                    TotalContributed = sp.Contributors.Sum(c => c.Amount)
                })
                .ToListAsync();

            return Ok(pools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SavingsPoolDetailDto>> GetSavingsPool(int id)
        {
            var pool = await _context.SavingsPools
                .Include(sp => sp.SchedType)
                .Include(sp => sp.Contributors)
                    .ThenInclude(c => c.User)
                .Include(sp => sp.Contributors)
                    .ThenInclude(c => c.StatusContribution)
                .FirstOrDefaultAsync(sp => sp.SavingsPoolsId == id);

            if (pool == null)
                return NotFound();

            var dto = new SavingsPoolDetailDto
            {
                SavingsPoolsId = pool.SavingsPoolsId,
                Title = pool.Title,
                TargetAmount = pool.TargetAmount,
                CurrentAmount = pool.CurrentAmount,
                SchedTypeId = pool.SchedTypeId,
                SchedTypeName = pool.SchedType.Name,
                ContributorCount = pool.Contributors.Count,
                TotalContributed = pool.Contributors.Sum(c => c.Amount),
                Contributors = pool.Contributors.Select(c => new PoolContributorResponseDto
                {
                    ContributorId = c.ContributorId,
                    SavingsPoolId = c.SavingsPoolId,
                    SavingsPoolTitle = pool.Title,
                    UserId = c.UserId,
                    UserName = c.User.Name,
                    StatusId = c.StatusId,
                    StatusName = c.StatusContribution.StatusName,
                    Amount = c.Amount,
                    ContributionDate = c.ContributionDate
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<SavingsPoolResponseDto>> CreateSavingsPool(SavingsPoolCreateDto dto)
        {
            var pool = new SavingsPool
            {
                Title = dto.Title,
                TargetAmount = dto.TargetAmount,
                CurrentAmount = 0,
                SchedTypeId = dto.SchedTypeId
            };

            _context.SavingsPools.Add(pool);
            await _context.SaveChangesAsync();

            var created = await _context.SavingsPools
                .Include(sp => sp.SchedType)
                .Include(sp => sp.Contributors)
                .FirstAsync(sp => sp.SavingsPoolsId == pool.SavingsPoolsId);

            return CreatedAtAction(nameof(GetSavingsPool), new { id = pool.SavingsPoolsId }, new SavingsPoolResponseDto
            {
                SavingsPoolsId = created.SavingsPoolsId,
                Title = created.Title,
                TargetAmount = created.TargetAmount,
                CurrentAmount = created.CurrentAmount,
                SchedTypeId = created.SchedTypeId,
                SchedTypeName = created.SchedType.Name,
                ContributorCount = 0,
                TotalContributed = 0
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSavingsPool(int id, SavingsPoolUpdateDto dto)
        {
            var pool = await _context.SavingsPools.FindAsync(id);
            if (pool == null)
                return NotFound();

            pool.Title = dto.Title;
            pool.TargetAmount = dto.TargetAmount;
            pool.CurrentAmount = dto.CurrentAmount;
            pool.SchedTypeId = dto.SchedTypeId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavingsPool(int id)
        {
            var pool = await _context.SavingsPools.FindAsync(id);
            if (pool == null)
                return NotFound();

            _context.SavingsPools.Remove(pool);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
