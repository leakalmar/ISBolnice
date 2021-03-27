// File:    WorkDay.cs
// Author:  Asus
// Created: Friday, March 26, 2021 5:40:08 PM
// Purpose: Definition of Class WorkDay

using System;

namespace Model
{
   public class WorkDay
   {
        public String Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public WorkDay(string day, DateTime startTime, DateTime endTime)
        {
            this.Day = day;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}