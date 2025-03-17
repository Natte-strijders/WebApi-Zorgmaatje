namespace ZorgmaatjeWebApi
{
    public class Patient
    {
        public int id { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public int ouderVoogd_Id { get; set; }
        public int trajectId { get; set; }
        public int artsId { get; set; }
    }

}
