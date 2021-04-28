using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class StaticTransferAppointmentDTO
    {
        public Room SourceRoom { get; set; }
        public Room DestinationRoom { get; set; }
        public Equipment Equip { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }

        public StaticTransferAppointmentDTO(Room sourceRoom, Room destinationRoom, Equipment equip, int quantity, DateTime startDate, DateTime endDate, string description)
        {
            SourceRoom = sourceRoom;
            DestinationRoom = destinationRoom;
            Equip = equip;
            Quantity = quantity;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}
