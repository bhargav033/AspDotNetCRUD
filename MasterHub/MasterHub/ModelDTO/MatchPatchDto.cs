namespace MasterHub.ModelDTO
{
    public class MatchPatchDto
    {
        public DateTime? Date { get; set; }
        public int? TeamOneId { get; set; }
        public int? TeamTwoId { get; set; }
        public string Result { get; set; }
        public int? WinnerTeamId { get; set; }
        public string Description { get; set; }
    }
}
