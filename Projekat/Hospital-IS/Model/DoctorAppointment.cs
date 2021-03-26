/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Asus
 * Purpose: Definition of the Class Appointment
 ***********************************************************************/

using System;

namespace Model
{
   public class DoctorAppointment : Appointment
   {
      private String cause;
      private String detail;
      
      public void SetAdmitted(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Patient patient;
      
      /// <summary>
      /// Property for Patient
      /// </summary>
      /// <pdGenerated>Default opposite class property</pdGenerated>
      public Patient Patient
      {
         get
         {
            return patient;
         }
         set
         {
            if (this.patient == null || !this.patient.Equals(value))
            {
               if (this.patient != null)
               {
                  Patient oldPatient = this.patient;
                  this.patient = null;
                  oldPatient.RemoveDoctorAppointment(this);
               }
               if (value != null)
               {
                  this.patient = value;
                  this.patient.AddDoctorAppointment(this);
               }
            }
         }
      }
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
            if (this.doctor == null || !this.doctor.Equals(value))
            {
               if (this.doctor != null)
               {
                  Doctor oldDoctor = this.doctor;
                  this.doctor = null;
                  oldDoctor.RemoveDoctorAppointment(this);
               }
               if (value != null)
               {
                  this.doctor = value;
                  this.doctor.AddDoctorAppointment(this);
               }
            }
         }
      }
   
   }
}