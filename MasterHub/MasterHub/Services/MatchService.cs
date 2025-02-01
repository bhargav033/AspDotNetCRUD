using MasterHub.Data;
using MasterHub.IServices;
using MasterHub.Model;
using MasterHub.ModelDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<APIResponse> UpdateMatch(int id, [FromBody] Match match)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse.Sucess= false;
            match.Team = null;
            if(await _context.Match.AnyAsync(m=>m.Id != id))
            {
                apiResponse.ErrorMessage = "Match are not available!!";
                return apiResponse;
            }
            var updateData =  _context.Match.Update(match);
            _context.SaveChangesAsync();
            apiResponse.Sucess = true;
            apiResponse.Data = updateData;
            return apiResponse;
        }
        public async Task<APIResponse> UpdateSpecificMatch(int id, MatchPatchDto matchdto)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse.Sucess = false;
            var match = await _context.Match.FindAsync(id);
            if (await _context.Match.AnyAsync(m => m.Id != id))
            {
                apiResponse.ErrorMessage = "Match are not available!!";
                return apiResponse;
            }
            // Only update fields that are provided
            if (matchdto.Date != null)
            {
                match.Date = matchdto.Date.Value;
            }

            if (matchdto.TeamOneId.HasValue)
            {
                match.TeamOneId = matchdto.TeamOneId.Value;
            }

            if (matchdto.TeamTwoId.HasValue)
            {
                match.TeamTwoId = matchdto.TeamTwoId.Value;
            }

            if (!string.IsNullOrEmpty(matchdto.Result))
            {
                match.Result = matchdto.Result;
            }

            if (matchdto.WinnerTeamId.HasValue)
            {
                match.WinnerTeamId = matchdto.WinnerTeamId.Value;
            }

            if (!string.IsNullOrEmpty(matchdto.Description))
            {
                match.Description = matchdto.Description;
            }

            // Save the updated match record to the database
            await _context.SaveChangesAsync();
            apiResponse.Sucess = true;
            return apiResponse;
        }
    

    }
}
