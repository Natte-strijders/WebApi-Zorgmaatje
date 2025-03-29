using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi.ZorgMoment
{
    public class ZorgMoment
    {

            public int id { get; set; }
            public string naam { get; set; }
            public DateTime? datumTijd { get; set; }
            public string patientId { get; set; }
        
    }
}
