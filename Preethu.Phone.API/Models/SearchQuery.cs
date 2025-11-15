using System.ComponentModel.DataAnnotations;

namespace Preethu.Phone.API.Models
{
    public class SearchQuery
    {
        [Key]
        public int QueryId { get; set; }
        public string? Processor { get; set; }
        public string? Storage { get; set; }
        public string? RAM { get; set; }
        public string? OS { get; set; }
    }
}
