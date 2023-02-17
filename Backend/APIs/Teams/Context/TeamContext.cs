using Microsoft.EntityFrameworkCore;
using Teams.Models;

namespace Teams.Context
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; }
    }
}
