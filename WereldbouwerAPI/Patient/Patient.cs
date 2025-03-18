using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi.Patient
{
    public class Patient
    {
        public string id { get; set; }
        [Required]
        public string voornaam { get; set; }
        [Required]
        public string achternaam { get; set; }
        public int ouderVoogd_Id { get; set; }
        public int trajectId { get; set; }
        public int artsId { get; set; }
    }

}
