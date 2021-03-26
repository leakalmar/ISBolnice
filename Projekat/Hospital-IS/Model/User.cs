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
      private EducationCategory education;
      private int id;
      private String name;
      private String surname;
      private DateTime birthDate;
      private String phone;
      private String email;
      private String gender;
      private String relationship;
      private String password;
      
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
      
      /// <summary>
      /// Property for Hospital
      /// </summary>
      /// <pdGenerated>Default opposite class property</pdGenerated>
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