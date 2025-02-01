using MasterHub.Data;
using MasterHub.IServices;
using MasterHub.Model;
using MasterHub.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearderboardController(ApplicationDbContext _context,IMatchService _matchService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardItem>>> GetLeaderboard()
        {
            try
            {
                var teams = await _context.Team.ToListAsync();
                var matches = await _context.Match.ToListAsync();

                var leaderboard = teams.Select(team => {
                    var wins = matches.Count(m => m.WinnerTeamId == team.Id);

                    var draws = matches.Count(m =>
                        (m.TeamOneId == team.Id || m.TeamTwoId == team.Id) &&
                        m.Result.Equals("Draw", StringComparison.OrdinalIgnoreCase));

                    var points = (wins * 2) + draws;

                    return new LeaderboardItem
                    {
                        TeamId = team.Id,
                        TeamName = team.TeamName,
                        Points = points
                    };
                })
                .OrderByDescending(t => t.Points)
                .ToList();

                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllMatch")]
        public async Task<IActionResult> GetMatches(int id)
        {
            var data = await _context.Match.ToListAsync();
            var teamone = await _context.Match.Where(m => m.Id == id).FirstOrDefaultAsync();  // For single record
            return Ok(data);
        }
        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetByDate(DateTime startdate, DateTime enddate)
        {
            var data = await _context.Match.Where(m => m.Date > startdate && m.Date < enddate).ToListAsync();
            return Ok(data);
        }
        [HttpGet("GetMatchByWinTeamName")]
        public async Task<IActionResult> GetMatchByWinTeamName()
        {
            var match = await _context.Match.Where(x => x.Result == "Win" && x.WinnerTeamId != null)
                 .Select(m => new
                 {
                     MatchId = m.Id,
                     MatchDate = m.Date,
                     MatchTeamOneId = m.TeamOneId,
                     MatchTeamTwoId = m.TeamTwoId,
                     MatchResult = m.Result,
                     MatchDescription = m.Description,
                     MatchTeamName = _context.Team.Where(x => x.Id == m.WinnerTeamId).Select(x => x.TeamName).FirstOrDefault()
                 }).ToListAsync();
            return Ok(match);
        }
        [HttpGet("GetTeamNameWithTotalMatch")]
        public async Task<IActionResult> GetTeamNameWithTotalMatch()
        {
            var matches = await _context.Match.ToListAsync();
            var teams = await _context.Team.ToListAsync();
            var result = teams.Select(
                t => new TeamNameDTO
                {
                    TeamName = t.TeamName,
                    TotalMatches = matches.Count(m => m.TeamOneId == t.Id || m.TeamTwoId == t.Id),
                    Score = matches.Where(m => m.TeamId == t.Id || m.TeamTwoId == t.Id)
                                          .Sum(m => m.WinnerTeamId == t.Id ? 2 : (m.Result == "Draw" ? 1 : 0))

                }).ToList();
            return Ok(result);
        }
        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam([FromBody]Team team)
        {
            var apiresponse = await _matchService.AddTeam(team);
            if (apiresponse.Sucess)
                return Ok("Insert Sucessfully");
            return BadRequest(apiresponse);
        }
        [HttpPost("UpdateMatch")]
        public async Task<IActionResult> UpdateMatch(int id, [FromBody] Match match)
        {
            var apiresponse = await _matchService.UpdateMatch(id,match);
            if (apiresponse.Sucess)
                return Ok("Update Sucessfully");
            return BadRequest(apiresponse);
        }
        [HttpPost("UpdateSpecificMatch")]
        public async Task<IActionResult> UpdateSpecificMatch(int id, [FromBody] MatchPatchDto matchdto)
        {
            var apiresponse = await _matchService.UpdateSpecificMatch(id, matchdto);
            if (apiresponse.Sucess)
                return Ok("Update Field Sucessfully");
            return BadRequest(apiresponse);
        }
    }
}
