using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MasterHub.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Required]
        [MaxLength(6)]
        public string ProductCode { get; set; }
        [Required]
        public int Quentity {  get; set; }
        [Required]
        public decimal MfgPrice { get; set; }
        public decimal Profit { get; set; }
        public decimal ProfitPercentage { get; set; }
        public string Image { get; set; }
        
        public int CatagoryIdRef {  get; set; }
        [ForeignKey(nameof(CatagoryIdRef))]
        public Catagory catagory { get; set; }
        [NotMapped] public IFormFile File { get; set; }

    }
}
