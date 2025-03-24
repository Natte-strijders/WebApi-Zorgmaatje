using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi.TrajectZorgMoment
{
    public class TrajectZorgMoment
    {
        public int TrajectZorgMomentId { get; set; }
        public int ZorgMomentId { get; set; }
        public int Volgorde { get; set; }

    }
}
