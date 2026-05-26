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

        [HttpGet("statustypes")]
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
    }
}
