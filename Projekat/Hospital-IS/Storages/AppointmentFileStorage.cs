/***********************************************************************
 * Module:  AppointmentFileStorage.cs
 * Author:  Me
 * Purpose: Definition of the Class AppointmentFileStorage
 ***********************************************************************/

using System;

namespace Storages
{
   public class AppointmentFileStorage
   {
      public List<Appointment> GetAllByPatient(Patient patient)
      {
         // TODO: implement
         return null;
      }
      
      public List<Appointment> GetAllByDoctor(Doctor doctor)
      {
         // TODO: implement
         return null;
      }
      
      public void SaveAppointment(Appointment appointment)
      {
         // TODO: implement
      }
      
      public List<Appointment> GetAllByRoom(Model.Room room)
      {
         // TODO: implement
         return null;
      }
   
      private string FileLocation;
   
   }
}