/***********************************************************************
 * Module:  Specialty.cs
 * Author:  Asus
 * Purpose: Definition of the Class Specialty
 ***********************************************************************/

using System;

namespace Model
{
   public class Specialty
   {
      private String role;
      
      public Doctor doctor;
      
      /// <summary>
      /// Property for Doctor
      /// </summary>
      /// <pdGenerated>Default opposite class property</pdGenerated>
      public Doctor Doctor
      {
         get
         {
            return doctor;
         }
         set
         {
            this.doctor = value;
         }
      }
   
   }
}