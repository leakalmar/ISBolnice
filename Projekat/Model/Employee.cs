using System;

namespace Model
{
   public class Employee : User
   {
      public System.Collections.ArrayList workDay;
      
      
      public System.Collections.ArrayList GetWorkDay()
      {
         if (workDay == null)
            workDay = new System.Collections.ArrayList();
         return workDay;
      }
      
      
      public void SetWorkDay(System.Collections.ArrayList newWorkDay)
      {
         RemoveAllWorkDay();
         foreach (WorkDay oWorkDay in newWorkDay)
            AddWorkDay(oWorkDay);
      }
      
      
      public void AddWorkDay(WorkDay newWorkDay)
      {
         if (newWorkDay == null)
            return;
         if (this.workDay == null)
            this.workDay = new System.Collections.ArrayList();
         if (!this.workDay.Contains(newWorkDay))
            this.workDay.Add(newWorkDay);
      }
      
      
      public void RemoveWorkDay(WorkDay oldWorkDay)
      {
         if (oldWorkDay == null)
            return;
         if (this.workDay != null)
            if (this.workDay.Contains(oldWorkDay))
               this.workDay.Remove(oldWorkDay);
      }
      
      
      public void RemoveAllWorkDay()
      {
         if (workDay != null)
            workDay.Clear();
      }
   
      private Double Salary;
      private DateTime EmploymentDate;
   
   }
}