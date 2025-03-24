using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi.ZorgMoment
{
    public class ZorgMoment
    {

            public int Id { get; set; }
            public string Naam { get; set; }
            public DateTime DatumTijd { get; set; }
            public string PatientId { get; set; }
        
    }
}
