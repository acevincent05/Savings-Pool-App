using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly SavingsPoolContext _context;

        public LookupsController(SavingsPoolContext context)
        {
            _context = context;
        }

        [HttpGet("schedtypes")]
        public async Task<ActionResult<IEnumerable<SchedTypeResponseDto>>> GetSchedTypes()
        {
            var types = await _context.SchedTypes
                .Select(st => new SchedTypeResponseDto
                {
                    SchedTypeId = st.SchedTypeId,
                    Name = st.Name
                })
                .ToListAsync();

            return Ok(types);
        }

        [HttpPost("schedtypes")]
        public async Task<ActionResult<SchedTypeResponseDto>> CreateSchedType(SchedTypeCreateDto dto)
        {
            var type = new SchedTypes { Name = dto.Name };
            _context.SchedTypes.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedTypes), new { id = type.SchedTypeId }, new SchedTypeResponseDto
            {
                SchedTypeId = type.SchedTypeId,
                Name = type.Name
            });
        }

        [HttpDelete("schedtypes/{id}")]
        public async Task<IActionResult> DeleteSchedType(int id)
        {
            var type = await _context.SchedTypes.FindAsync(id);
            if (type == null) return NotFound();

            _context.SchedTypes.Remove(type);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("statuses")]
        public async Task<ActionResult<IEnumerable<StatusResponseDto>>> GetStatuses()
        {
            var statuses = await _context.StatusContributions
                .Select(s => new StatusResponseDto
                {
                    StatusId = s.StatusId,
                    StatusName = s.StatusName
                })
                .ToListAsync();

            return Ok(statuses);
        }

        [HttpPost("statuses")]
        public async Task<ActionResult<StatusResponseDto>> CreateStatus(StatusCreateDto dto)
        {
            var status = new StatusContribution { StatusName = dto.StatusName };
            _context.StatusContributions.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatuses), new { id = status.StatusId }, new StatusResponseDto
            {
                StatusId = status.StatusId,
                StatusName = status.StatusName
            });
        }

        [HttpDelete("statuses/{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.StatusContributions.FindAsync(id);
            if (status == null) return NotFound();

            _context.StatusContributions.Remove(status);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
