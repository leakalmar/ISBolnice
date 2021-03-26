/***********************************************************************
 * Module:  Hospital.cs
 * Author:  Asus
 * Purpose: Definition of the Class Hospital
 ***********************************************************************/

using System;

namespace Model
{
   public class Hospital
   {
      public List<Patient> GetAllPatients()
      {
         // TODO: implement
         return null;
      }
      
      public List<Appointment> GellAllAppointmets()
      {
         // TODO: implement
         return null;
      }
      
      public List<Equipment> GetAllEquipments()
      {
         // TODO: implement
         return null;
      }
      
      public List<Room> GetAllRooms()
      {
         // TODO: implement
         return null;
      }
      
      public List<User> GetAllEmployees()
      {
         // TODO: implement
         return null;
      }
   
      public Room[] room;
      public User[] user;
   
      private String Name;
      private String City;
      private String Country;
      private String Address;
      private List<Appointment> AllAppointments;
      private List<Patient> AllPatients;
      private List<Equiptment> AllEquiptment;
      private List<Room> AllRooms;
      private List<User> AllEmployees;
   
   }
}