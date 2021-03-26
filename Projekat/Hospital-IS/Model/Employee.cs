// File:    Employee.cs
// Author:  Asus
// Created: Friday, March 26, 2021 5:40:08 PM
// Purpose: Definition of Class Employee

using System;

namespace Model
{
   public class Employee : User
   {
      private Double salary;
      private DateTime employmentDate;
      
      public System.Collections.ArrayList workDay;
      
      /// <summary>
      /// Property for collection of WorkDay
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList WorkDay
      {
         get
         {
            if (workDay == null)
               workDay = new System.Collections.ArrayList();
            return workDay;
         }
         set
         {
            RemoveAllWorkDay();
            if (value != null)
            {
               foreach (WorkDay oWorkDay in value)
                  AddWorkDay(oWorkDay);
            }
         }
      }
      
      /// <summary>
      /// Add a new WorkDay in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddWorkDay(WorkDay newWorkDay)
      {
         if (newWorkDay == null)
            return;
         if (this.workDay == null)
            this.workDay = new System.Collections.ArrayList();
         if (!this.workDay.Contains(newWorkDay))
            this.workDay.Add(newWorkDay);
      }
      
      /// <summary>
      /// Remove an existing WorkDay from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveWorkDay(WorkDay oldWorkDay)
      {
         if (oldWorkDay == null)
            return;
         if (this.workDay != null)
            if (this.workDay.Contains(oldWorkDay))
               this.workDay.Remove(oldWorkDay);
      }
      
      /// <summary>
      /// Remove all instances of WorkDay from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllWorkDay()
      {
         if (workDay != null)
            workDay.Clear();
      }
   
   }
}