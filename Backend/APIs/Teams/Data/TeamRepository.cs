using Microsoft.EntityFrameworkCore;
using Teams.Context;
using Teams.Models;

namespace Teams.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamContext _teamContext;

        public TeamRepository(TeamContext teamContext)
        {
            _teamContext = teamContext;
        }
        public async Task<Team> CreateTeam(Team team)
        {
            if(team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }
            await _teamContext.Teams.AddAsync(team);
            return team;
        }

        public async Task<Team> DeleteTeam(Guid teamId)
        {
            if(GetTeam(teamId) == null)
            {
                throw new InvalidOperationException();
            }
            Team team = await GetTeam(teamId);
            _teamContext.Teams.Remove(team); 
            return team;
        }

        public async Task<Team> GetTeam(Guid teamId)
        {
            return await _teamContext.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _teamContext.Teams.ToListAsync();
        }

        public Boolean SaveChanges()
        {
            return (_teamContext.SaveChanges() > 0);
        }

        public async Task<Team> UpdateTeam(Team team)
        {
            if(team == null)
            {
                throw new InvalidOperationException();
            }
            _teamContext.Entry(team).State = EntityState.Modified;
            return team;
        }
    }
}
