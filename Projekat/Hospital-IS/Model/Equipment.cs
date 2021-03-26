/***********************************************************************
 * Module:  Oprema.cs
 * Author:  Jovanovic
 * Purpose: Definition of the Class Oprema
 ***********************************************************************/

using System;

namespace Model
{
   public class Equipment
   {
      private EquiptType equipType;
      private int equiptId;
      private String name;
      private int quantity;
      
      public System.Collections.ArrayList room;
      
      /// <summary>
      /// Property for collection of Room
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
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
      
      /// <summary>
      /// Add a new Room in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
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
      
      /// <summary>
      /// Remove an existing Room from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
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
      
      /// <summary>
      /// Remove all instances of Room from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
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