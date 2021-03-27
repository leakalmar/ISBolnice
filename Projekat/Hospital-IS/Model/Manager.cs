/***********************************************************************
 * Module:  Upravnik.cs
 * Author:  Jovanovic
 * Purpose: Definition of the Class Upravnik
 ***********************************************************************/

using System;

namespace Model
{
   public class Manager : Employee
   {
      public Boolean AddRoom(Room newroom)
      {
            Hospital.AddRoom(newroom);
            return true;
        }
      
      public Boolean RemoveRoom(Room room)
      {
            Hospital.RemoveRoom(room);
         return true;
      }
      
      public void UpdateRoom(Room room)
      {
        var rooms = new System.Collections.ArrayList();
            rooms = Hospital.room;
            foreach (Room r in rooms)
            {
                if(r == room)
                {
                    r.RoomFloor = room.RoomFloor;
                    r.RoomIdFloor = room.RoomIdFloor;
                    r.IsFree = room.IsFree;
                    r.Type = room.Type;
                }
            }

        }
      
      public Boolean MakeAppointment(Room room)
      {
         throw new NotImplementedException();
      }
      
      public Boolean RemoveAppointment(Appointment appointment)
      {
         throw new NotImplementedException();
      }
   
   }
}