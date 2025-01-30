using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterHub.Model
{
    public class Auth
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress{ get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        [Compare(nameof(Password))]
        public string ConfrimPassword { get; set; }
    }
}
