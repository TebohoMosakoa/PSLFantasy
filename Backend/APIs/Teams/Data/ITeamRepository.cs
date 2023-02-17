using Teams.Models;
using System.Linq;

namespace Teams.Data
{
    public interface ITeamRepository
    {
        Task<Team> CreateTeam(Team team);
        Boolean SaveChanges();
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(Guid teamId);
        Task<Team> UpdateTeam (Team team);
        Task<Team> DeleteTeam(Guid teamId);
    }
}
