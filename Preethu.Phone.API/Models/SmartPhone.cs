using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preethu.Phone.API.Models
{
    public class SmartPhone
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20,ErrorMessage="Name length is too long")]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "Description length is too long")]
        public string Description { get; set; }
        [Range(1000,100000)]
        [Required(ErrorMessage ="Price must be listed")]
        public double Price { get; set; }
        public int MId { get; set; }
        [ForeignKey("MId")]
        public Manufacturer? Manufacturer { get; set; }
        public int SpecId { get; set; }
        [ForeignKey("SpecId")]
        public SmartPhoneSpec? Specification { get; set; }
    }
}
