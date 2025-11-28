using System.ComponentModel.DataAnnotations;

namespace Preethu.Phone.API.Models
{
    public class SearchQuery
    {
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public string? Processor { get; set; }
        public string? Storage { get; set; }
        public string? Ram { get; set; }
        public string? Os { get; set; }
    }
}
