using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterHub.Model
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TeamOneId { get; set; }
        public int TeamTwoId { get; set; }
        public string Result { get; set; }
        public int? WinnerTeamId { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }
    }
}
