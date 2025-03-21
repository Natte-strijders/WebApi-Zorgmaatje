using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi.Arts
{
    public class Arts
    {
        public string id { get; set; }
        [Required]
        public string naam { get; set; }
        public string specialisatie { get; set; }
    }
}
