
using System;

namespace Model
{
    public class Hospitalization
    {
        public DateTime AdmissionDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Detail { get; set; }
        public Room Room { get; set; }
        public Bed Bed { get; set; }
        public string Doctor { get; set; }
        public bool Released { get; set; }

        public Hospitalization(DateTime admissionDate, DateTime releaseDate, string doctor, Room room, Bed bed, string details)
        {
            Released = false;
            Doctor = doctor;
            AdmissionDate = admissionDate;
            ReleaseDate = releaseDate;
            Detail = details;
            Room = room;
            this.Bed = bed;
        }
    }
}