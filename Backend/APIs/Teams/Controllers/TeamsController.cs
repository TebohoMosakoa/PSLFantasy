using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Context;
using Teams.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamContext _teamContext;

        public TeamsController(TeamContext teamContext)
        {
            _teamContext = teamContext;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<IActionResult> Get() =>  Ok(await _teamContext.Teams.ToListAsync());

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> Get(Guid id)
        {
            var item = await _teamContext.Teams.FirstOrDefaultAsync(x => x.TeamId == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Team item)
        {
            if (id != item.TeamId)
            {
                return BadRequest();
            }
            _teamContext.Teams.Update(item);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<IActionResult> AddContact(Team item)
        {
            await _teamContext.Teams.AddAsync(item);
            await _teamContext.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> Delete(Guid id)
        {
            var item = await _teamContext.Teams.FirstOrDefaultAsync(x => x.TeamId == id);
            if (item == null)
            {
                return NotFound();
            }
            else{
               _teamContext.Teams.Remove(item);
            }
            return item;
        }
    }
}
