/***********************************************************************
 * Module:  PatientFileStorage.cs
 * Author:  Me
 * Purpose: Definition of the Class PatientFileStorage
 ***********************************************************************/

using System;

namespace Storages
{
   public class UserFileStorage
   {
      public List<User> GetAll()
      {
         // TODO: implement
         return null;
      }
      
      public void SaveUser(Model.User user)
      {
         // TODO: implement
      }
      
      public Model.User GetByEmail(String email)
      {
         // TODO: implement
         return null;
      }
   
      private string FileLocation;
   
   }
}