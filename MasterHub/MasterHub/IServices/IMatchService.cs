using MasterHub.Model;

namespace MasterHub.IServices
{
    public interface IMatchService
    {
        public Task<APIResponse> AddTeam(Team team);
    }
}
