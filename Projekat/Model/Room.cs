/***********************************************************************
 * Module:  Sobe.cs
 * Author:  Jovanovic
 * Purpose: Definition of the Class Sobe
 ***********************************************************************/

using System;

namespace Model
{
   public class Room
   {
      public System.Collections.ArrayList equipment;
      
      
      public System.Collections.ArrayList GetEquipment()
      {
         if (equipment == null)
            equipment = new System.Collections.ArrayList();
         return equipment;
      }
      
      
      public void SetEquipment(System.Collections.ArrayList newEquipment)
      {
         RemoveAllEquipment();
         foreach (Equipment oEquipment in newEquipment)
            AddEquipment(oEquipment);
      }
      
      
      public void AddEquipment(Equipment newEquipment)
      {
         if (newEquipment == null)
            return;
         if (this.equipment == null)
            this.equipment = new System.Collections.ArrayList();
         if (!this.equipment.Contains(newEquipment))
            this.equipment.Add(newEquipment);
      }
      
      
      public void RemoveEquipment(Equipment oldEquipment)
      {
         if (oldEquipment == null)
            return;
         if (this.equipment != null)
            if (this.equipment.Contains(oldEquipment))
               this.equipment.Remove(oldEquipment);
      }
      
      
      public void RemoveAllEquipment()
      {
         if (equipment != null)
            equipment.Clear();
      }
      public Appointment[] appointment;
   
      private RoomType vrsta;
      private Boolean IsFree;
      private Boolean IsUsable;
      private int RoomFloor;
      private int RoomId;
   
   }
}