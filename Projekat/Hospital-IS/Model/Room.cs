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
      private RoomType type;
      private Boolean isFree;
      private Boolean isUsable;
      private int roomFloor;
      private int roomId;
      
      public System.Collections.ArrayList equipment;



        public int RoomFloor  
        {
            get { return roomFloor; }   
            set { roomFloor = value; }  
        }

        public int RoomIdFloor
        {
            get { return roomId; }
            set { roomId = value; }
        }

        public Boolean IsFree
        {
            get { return isFree; }
            set { isFree = value; }
        }

        public Boolean IsUsable
        {
            get { return isUsable; }
            set { isUsable = value; }
        }

        public RoomType Type
        {
            get; set;
        }

        public Room(RoomType type, bool isFree, bool isUsable, int roomFloor, int roomId)
        {
            this.type = type;
            this.isFree = isFree;
            this.isUsable = isUsable;
            this.roomFloor = roomFloor;
            this.roomId = roomId;
        }

        public System.Collections.ArrayList Equipment
      {
         get
         {
            if (equipment == null)
               equipment = new System.Collections.ArrayList();
            return equipment;
         }
         set
         {
            RemoveAllEquipment();
            if (value != null)
            {
               foreach (Equipment oEquipment in value)
                  AddEquipment(oEquipment);
            }
         }
      }
      
      /// <summary>
      /// Add a new Equipment in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddEquipment(Equipment newEquipment)
      {
         if (newEquipment == null)
            return;
         if (this.equipment == null)
            this.equipment = new System.Collections.ArrayList();
         if (!this.equipment.Contains(newEquipment))
         {
            this.equipment.Add(newEquipment);
            newEquipment.AddRoom(this);      
         }
      }
      
      /// <summary>
      /// Remove an existing Equipment from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveEquipment(Equipment oldEquipment)
      {
         if (oldEquipment == null)
            return;
         if (this.equipment != null)
            if (this.equipment.Contains(oldEquipment))
            {
               this.equipment.Remove(oldEquipment);
               oldEquipment.RemoveRoom(this);
            }
      }
      
      /// <summary>
      /// Remove all instances of Equipment from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllEquipment()
      {
         if (equipment != null)
         {
            System.Collections.ArrayList tmpEquipment = new System.Collections.ArrayList();
            foreach (Equipment oldEquipment in equipment)
               tmpEquipment.Add(oldEquipment);
            equipment.Clear();
            foreach (Equipment oldEquipment in tmpEquipment)
               oldEquipment.RemoveRoom(this);
            tmpEquipment.Clear();
         }
      }
      public System.Collections.ArrayList appointment;
      
      /// <summary>
      /// Property for collection of Appointment
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Appointment
      {
         get
         {
            if (appointment == null)
               appointment = new System.Collections.ArrayList();
            return appointment;
         }
         set
         {
            RemoveAllAppointment();
            if (value != null)
            {
               foreach (Appointment oAppointment in value)
                  AddAppointment(oAppointment);
            }
         }
      }
      
      /// <summary>
      /// Add a new Appointment in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddAppointment(Appointment newAppointment)
      {
         if (newAppointment == null)
            return;
         if (this.appointment == null)
            this.appointment = new System.Collections.ArrayList();
         if (!this.appointment.Contains(newAppointment))
         {
            this.appointment.Add(newAppointment);
            newAppointment.Room = this;
         }
      }
      
      /// <summary>
      /// Remove an existing Appointment from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveAppointment(Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
            {
               this.appointment.Remove(oldAppointment);
               oldAppointment.Room = null;
            }
      }
      
      /// <summary>
      /// Remove all instances of Appointment from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllAppointment()
      {
         if (appointment != null)
         {
            System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
            foreach (Appointment oldAppointment in appointment)
               tmpAppointment.Add(oldAppointment);
            appointment.Clear();
            foreach (Appointment oldAppointment in tmpAppointment)
               oldAppointment.Room = null;
            tmpAppointment.Clear();
         }
      }
   
   }
}