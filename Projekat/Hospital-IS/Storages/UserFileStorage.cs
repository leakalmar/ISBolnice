using System;
using System.Collections.Generic;
using Model;

namespace Storages
{
   public class UserFileStorage
   {
      private string fileLocation;
      
      public List<User> GetAll()
      {
            List<User> all = new List<User>();

            List<WorkDay> dani = new List<WorkDay>();
            dani.Add(new WorkDay("Pon",DateTime.Now,DateTime.Now));
            Doctor doc = new Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com","123", 0.0, DateTime.Now, dani);
            Patient p1 = new Patient(000,"udata",DateTime.Now,"fds",false,new List<String>(),false,new List<DoctorAppointment>(),new MedicalHistory(), doc);
            Patient p2 = new Patient(000, "udata",DateTime.Now, "fds", false, new List<String>(), false, new List<DoctorAppointment>(), new MedicalHistory(), doc);
            Patient p3 = new Patient(000, "udata",DateTime.Now, "fds", false, new List<String>(), false, new List<DoctorAppointment>(), new MedicalHistory(), doc);
            all.Add(doc);
            all.Add(p1);
            all.Add(p2);
            all.Add(p3);
            return all;
      }
      
      public void SaveUser(Model.User user)
      {
         throw new NotImplementedException();
      }
      
      public Model.User GetByEmail(String email)
      {
         throw new NotImplementedException();
      }
   
   }
}