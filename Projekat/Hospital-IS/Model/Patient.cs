/***********************************************************************
 * Module:  Patient.cs
 * Author:  Asus
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using System;

namespace Model
{
   public class Patient : User
   {
      public Boolean IsAdmitted()
      {
         // TODO: implement
         return null;
      }
      
      public Boolean ReserveAppointment()
      {
         // TODO: implement
         return null;
      }
      
      public Boolean RemoveAppointment(DoctorAppointment appointment)
      {
         // TODO: implement
         return null;
      }
      
      public Boolean UpdateAppointment(Appointment appointment)
      {
         // TODO: implement
         return null;
      }
   
      public System.Collections.ArrayList doctorAppointment;
      
      
      public System.Collections.ArrayList GetDoctorAppointment()
      {
         if (doctorAppointment == null)
            doctorAppointment = new System.Collections.ArrayList();
         return doctorAppointment;
      }
      
      
      public void SetDoctorAppointment(System.Collections.ArrayList newDoctorAppointment)
      {
         RemoveAllDoctorAppointment();
         foreach (DoctorAppointment oDoctorAppointment in newDoctorAppointment)
            AddDoctorAppointment(oDoctorAppointment);
      }
      
      
      public void AddDoctorAppointment(DoctorAppointment newDoctorAppointment)
      {
         if (newDoctorAppointment == null)
            return;
         if (this.doctorAppointment == null)
            this.doctorAppointment = new System.Collections.ArrayList();
         if (!this.doctorAppointment.Contains(newDoctorAppointment))
         {
            this.doctorAppointment.Add(newDoctorAppointment);
            newDoctorAppointment.SetPatient(this);      
         }
      }
      
      
      public void RemoveDoctorAppointment(DoctorAppointment oldDoctorAppointment)
      {
         if (oldDoctorAppointment == null)
            return;
         if (this.doctorAppointment != null)
            if (this.doctorAppointment.Contains(oldDoctorAppointment))
            {
               this.doctorAppointment.Remove(oldDoctorAppointment);
               oldDoctorAppointment.SetPatient((Patient)null);
            }
      }
      
      
      public void RemoveAllDoctorAppointment()
      {
         if (doctorAppointment != null)
         {
            System.Collections.ArrayList tmpDoctorAppointment = new System.Collections.ArrayList();
            foreach (DoctorAppointment oldDoctorAppointment in doctorAppointment)
               tmpDoctorAppointment.Add(oldDoctorAppointment);
            doctorAppointment.Clear();
            foreach (DoctorAppointment oldDoctorAppointment in tmpDoctorAppointment)
               oldDoctorAppointment.SetPatient((Patient)null);
            tmpDoctorAppointment.Clear();
         }
      }
      public MedicalHistory medicalHistory;
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
               oldDoctor.RemovePatient(this);
            }
            if (newDoctor != null)
            {
               this.doctor = newDoctor;
               this.doctor.AddPatient(this);
            }
         }
      }
   
      private int ChartId;
      private String MaritalStatus;
      private User Guarantor;
      private DateTime FileDate;
      private String Employer;
      private Boolean Admitted;
      private List<String> Alergies;
      private Boolean IsGuest = False;
   
   }
}