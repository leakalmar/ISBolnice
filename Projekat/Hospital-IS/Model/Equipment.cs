/***********************************************************************
 * Module:  Oprema.cs
 * Author:  Jovanovic
 * Purpose: Definition of the Class Oprema
 ***********************************************************************/

using System;
using System.Collections;

namespace Model
{
   public class Equipment
   {
      private EquiptType equipType;
      private int equiptId;
      private String name;
      private int quantity;
      
      public System.Collections.ArrayList room;

        public Equipment(EquiptType equipType, int equiptId, string name, int quantity, ArrayList room)
        {
            this.equipType = equipType;
            this.equiptId = equiptId;
            this.name = name;
            this.quantity = quantity;
            this.room = room;
        }

        public EquiptType EquiptType { get; set; }
        public int EquiptId { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }


        
        public System.Collections.ArrayList Room
      {
         get
         {
            if (room == null)
               room = new System.Collections.ArrayList();
            return room;
         }
         set
         {
            RemoveAllRoom();
            if (value != null)
            {
               foreach (Room oRoom in value)
                  AddRoom(oRoom);
            }
         }
      }
      
      
      public void AddRoom(Room newRoom)
      {
         if (newRoom == null)
            return;
         if (this.room == null)
            this.room = new System.Collections.ArrayList();
         if (!this.room.Contains(newRoom))
         {
            this.room.Add(newRoom);
            newRoom.AddEquipment(this);      
         }
      }
      
      
      public void RemoveRoom(Room oldRoom)
      {
         if (oldRoom == null)
            return;
         if (this.room != null)
            if (this.room.Contains(oldRoom))
            {
               this.room.Remove(oldRoom);
               oldRoom.RemoveEquipment(this);
            }
      }
      
     
      public void RemoveAllRoom()
      {
         if (room != null)
         {
            System.Collections.ArrayList tmpRoom = new System.Collections.ArrayList();
            foreach (Room oldRoom in room)
               tmpRoom.Add(oldRoom);
            room.Clear();
            foreach (Room oldRoom in tmpRoom)
               oldRoom.RemoveEquipment(this);
            tmpRoom.Clear();
         }
      }
   
   }
}