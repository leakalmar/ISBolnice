using Enums;
using System;

namespace Hospital_IS.DTOs.SecretaryDTOs
{
    public class RoomDTO
    {
        public int RoomNumber { get; set; }
        public int BedNumber { get; set; }
        public int RoomId { get; set; }
        public RoomType Type { get; set; }
        public RoomDTO(int roomNumber, int bedNumber, int roomId, RoomType type)
        {
            RoomNumber = roomNumber;
            BedNumber = bedNumber;
            RoomId = roomId;
            Type = type;
        }
    }
}
