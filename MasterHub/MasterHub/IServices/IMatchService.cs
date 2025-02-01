using MasterHub.Model;
using MasterHub.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace MasterHub.IServices
{
    public interface IMatchService
    {
        public Task<APIResponse> AddTeam(Team team);
        public Task<APIResponse> UpdateMatch(int id, Match match);
        public Task<APIResponse> UpdateSpecificMatch(int id, MatchPatchDto matchdto);

    }
}
