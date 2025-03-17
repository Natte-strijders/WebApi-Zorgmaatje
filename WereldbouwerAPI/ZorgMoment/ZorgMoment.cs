using System.ComponentModel.DataAnnotations;

namespace ZorgmaatjeWebApi.ZorgMoment
{
    public class ZorgMoment
    {
        public int id { get; set; }
        public string naam { get; set; }
        public string url { get; set; }
        public byte[] plaatje { get; set; }
        public int tijdsDuurInMin { get; set; }
    }
}
