
using System;

namespace Model
{
    public class Hospitalization
    {
        public DateTime AdmissionDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Detail { get; set; }
        public Room Room { get; set; }

        public Hospitalization(DateTime admissionDate, DateTime releaseDate, string detail, Room room)
        {
            AdmissionDate = admissionDate;
            ReleaseDate = releaseDate;
            Detail = detail;
            Room = room;
        }
    }
}