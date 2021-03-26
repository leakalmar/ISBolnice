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
      public void SetAdmitted(Patient patient)
      {
         // TODO: implement
      }
   
      public Patient patient;
      
      
      public Patient GetPatient()
      {
         return patient;
      }
      
      
      
      public void SetPatient(Patient newPatient)
      {
         if (this.patient != newPatient)
         {
            if (this.patient != null)
            {
               Patient oldPatient = this.patient;
               this.patient = null;
               oldPatient.RemoveDoctorAppointment(this);
            }
            if (newPatient != null)
            {
               this.patient = newPatient;
               this.patient.AddDoctorAppointment(this);
            }
         }
      }
      public Doctor doctor;
      
      
      public Doctor GetDoctor()
      {
         return doctor;
      }
      
      
      
      public void SetDoctor(Doctor newDoctor)
      {
         if (this.doctor != newDoctor)
         {
            if (this.doctor != null)
            {
               Doctor oldDoctor = this.doctor;
               this.doctor = null;
               oldDoctor.RemoveDoctorAppointment(this);
            }
            if (newDoctor != null)
            {
               this.doctor = newDoctor;
               this.doctor.AddDoctorAppointment(this);
            }
         }
      }
   
      private String Cause;
      private String Detail;
   
   }
}