using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi
{
    public class TrajectZorgMoment
    {
        public int trajectId { get; set; }
        public int zorgMomentId { get; set; }
        public int volgorde { get; set; }
    }
}
