﻿namespace ZorgmaatjeWebApi.Patient
{
    public class Patient
    {
        public string id { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public int ouderVoogd_Id { get; set; }
        public int trajectId { get; set; }
        public int artsId { get; set; }
    }

}
