using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class RenovationAppointmentDTO
    {
       
        public DateTime DateStart { get; set; }
        public int RoomIdSource { get; set; }
        public int RoomIdDestination { get; set; }

        public RenovationAppointmentDTO(DateTime dateStart, int roomIdSource, int roomIdDestination)
        {
            DateStart = dateStart;
            RoomIdSource = roomIdSource;
            RoomIdDestination = roomIdDestination;
        }
    }
}
