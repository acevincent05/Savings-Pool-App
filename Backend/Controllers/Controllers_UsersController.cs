using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SavingsPoolContext _context;

        public UsersController(SavingsPoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.PoolContributors)
                .Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    TotalContributions = u.PoolContributors.Count,
                    TotalAmountContributed = u.PoolContributors.Sum(pc => pc.Amount)
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.PoolContributors)
                    .ThenInclude(pc => pc.SavingsPool)
                .Include(u => u.PoolContributors)
                    .ThenInclude(pc => pc.StatusContribution)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            return Ok(new UserDetailDto
            {
                UserId = user.UserId,
                Name = user.Name,
                TotalContributions = user.PoolContributors.Count,
                TotalAmountContributed = user.PoolContributors.Sum(pc => pc.Amount),
                Contributions = user.PoolContributors.Select(pc => new PoolContributorResponseDto
                {
                    ContributorId = pc.ContributorId,
                    SavingsPoolId = pc.SavingsPoolId,
                    SavingsPoolTitle = pc.SavingsPool.Title,
                    UserId = pc.UserId,
                    UserName = user.Name,
                    StatusId = pc.StatusId,
                    StatusName = pc.StatusContribution.StatusName,
                    Amount = pc.Amount,
                    ContributionDate = pc.ContributionDate
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser(UserCreateDto dto)
        {
            var user = new Users { Name = dto.Name };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, new UserResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                TotalContributions = 0,
                TotalAmountContributed = 0
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
