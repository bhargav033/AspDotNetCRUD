using MasterHub.Data;
using MasterHub.IServices;
using MasterHub.Model;

namespace MasterHub.Services
{
    public class MatchService(ApplicationDbContext _context) : IMatchService
    {
        public async Task<APIResponse> AddTeam(Team team)
        {
            APIResponse response = new APIResponse();
            response.Sucess= false;
            if ( _context.Team.Any(x => x.TeamName == team.TeamName))
            {
                response.ErrorMessage = "Team name must be uniqued";
                return response;
            }
            var result = await _context.Team.AddAsync(team);
            _context.SaveChangesAsync();
            return response;
        }

    }
}
