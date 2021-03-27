/***********************************************************************
 * Module:  Person.cs
 * Author:  Asus
 * Purpose: Definition of the Class Person
 ***********************************************************************/

using System;

namespace Model
{
   public class User
   {
        public EducationCategory education { get; set; }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Gender { get; set; }
        public String Relationship { get; set; }
        public String Password { get; set; }

        public User(int id, string name, string surname, DateTime birthDate,string email, string password)
       {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Email = email;
            this.Password = password;
       }

        public Boolean LogIn()
      {
         throw new NotImplementedException();
      }
      
      public Boolean ChangePassword()
      {
         throw new NotImplementedException();
      }
      
      public void ViewProfile()
      {
         throw new NotImplementedException();
      }
      
      public Hospital hospital;
     
      public Hospital Hospital
      {
         get
         {
            return hospital;
         }
         set
         {
            if (this.hospital == null || !this.hospital.Equals(value))
            {
               if (this.hospital != null)
               {
                  Hospital oldHospital = this.hospital;
                  this.hospital = null;
                  oldHospital.RemoveUser(this);
               }
               if (value != null)
               {
                  this.hospital = value;
                  this.hospital.AddUser(this);
               }
            }
         }
      }
   
   }
}