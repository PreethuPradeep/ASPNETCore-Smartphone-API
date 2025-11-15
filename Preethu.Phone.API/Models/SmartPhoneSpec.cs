using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Preethu.Phone.API.Models
{
    public class SmartPhoneSpec
    {
        [Key]
        public int SpecId { get; set; }
        [Required(ErrorMessage = "Processor is required.")]
        [StringLength(100, ErrorMessage = "Processor name is too long.")]
        public string Processor { get; set; }
        [Required(ErrorMessage = "RAM is required.")]
        public string  RAM { get; set; }
        [Required(ErrorMessage = "Storage is required.")]
        public string Storage { get; set; }
        [Required(ErrorMessage = "OS is required.")]
        public string OS { get; set; }
        [JsonIgnore]
        public ICollection<SmartPhone> SmartPhones { get; set; } = new List<SmartPhone>();
    }
}
