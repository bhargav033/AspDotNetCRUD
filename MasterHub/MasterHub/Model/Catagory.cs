using System.ComponentModel.DataAnnotations;

namespace MasterHub.Model
{
    public class Catagory
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string CatagoryName { get; set; }
    }
}
