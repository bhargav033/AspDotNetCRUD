using System.ComponentModel.DataAnnotations;

namespace MasterHub.Model
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TeamName { get; set; }
    }
}
