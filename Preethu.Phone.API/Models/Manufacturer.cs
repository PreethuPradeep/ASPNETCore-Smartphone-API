using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Preethu.Phone.API.Database;

namespace Preethu.Phone.API.Models
{
    public class Manufacturer
    {
        [Key]
        public int MId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<SmartPhone> SmartPhones { get; set; } = new List<SmartPhone>();

    }
}
