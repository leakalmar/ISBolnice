using System;

namespace Model
{
   public class Appointment
   {
      private DateTime date;
      private double time;
      private AppointmetType type;
      private Boolean reserved;
      
      public Boolean StartAppointment()
      {
         throw new NotImplementedException();
      }
      
      public Boolean EndAppointment()
      {
         throw new NotImplementedException();
      }
      
      public Room room;
      
      /// <summary>
      /// Property for Room
      /// </summary>
      /// <pdGenerated>Default opposite class property</pdGenerated>
      public Room Room
      {
         get
         {
            return room;
         }
         set
         {
            if (this.room == null || !this.room.Equals(value))
            {
               if (this.room != null)
               {
                  Room oldRoom = this.room;
                  this.room = null;
                  oldRoom.RemoveAppointment(this);
               }
               if (value != null)
               {
                  this.room = value;
                  this.room.AddAppointment(this);
               }
            }
         }
      }
   
   }
}