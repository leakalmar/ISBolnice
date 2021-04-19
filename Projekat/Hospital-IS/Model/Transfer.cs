using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Transfer
    {

        public Room sourceRoom { get; set; }
        public Room destinationRoom { get; set; }
        public Equipment equip { get; set;}
        public int quantity { get; set; }

        public Transfer(Room sourceRoom, Room destinationRoom, Equipment equip, int quantity)
        {
            this.sourceRoom = sourceRoom;
            this.destinationRoom = destinationRoom;
            this.equip = equip;
            this.quantity = quantity;
        }
    }
}
