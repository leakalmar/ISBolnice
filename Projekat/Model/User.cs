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
      public Boolean LogIn()
      {
         // TODO: implement
         return null;
      }
      
      public Boolean ChangePassword()
      {
         // TODO: implement
         return null;
      }
      
      public void ViewProfile()
      {
         // TODO: implement
      }
   
      public Hospital hospital;
   
      private EducationCategory Education;
      private int Id;
      private String Name;
      private String Surname;
      private DateTime BirthDate;
      private String Phone;
      private String Email;
      private String Gender;
      private String Relationship;
      private String Password;
   
   }
}