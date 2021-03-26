using System;
using Model;
using System.Collections.Generic;


namespace Model
{
   public class Patient : User
   {
      private int chartId;
      private String maritalStatus;
      private User guarantor;
      private DateTime fileDate;
      private String employer;
      private Boolean admitted;
      private List<String> alergies;
      private Boolean isGuest = false;
      
      public Boolean IsAdmitted()
      {
         throw new NotImplementedException();
      }
      
      public Boolean ReserveAppointment()
      {
         throw new NotImplementedException();
      }
      
      public Boolean RemoveAppointment(DoctorAppointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public Boolean UpdateAppointment(Appointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList doctorAppointment;

      public System.Collections.ArrayList DoctorAppointment
      {
         get
         {
            if (doctorAppointment == null)
               doctorAppointment = new System.Collections.ArrayList();
            return doctorAppointment;
         }
         set
         {
            RemoveAllDoctorAppointment();
            if (value != null)
            {
               foreach (DoctorAppointment oDoctorAppointment in value)
                  AddDoctorAppointment(oDoctorAppointment);
            }
         }
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
            newDoctorAppointment.Patient = this;
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
               oldDoctorAppointment.Patient = null;
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
               oldDoctorAppointment.Patient = null;
            tmpDoctorAppointment.Clear();
         }
      }
      public MedicalHistory medicalHistory;
      public Doctor doctor;

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
                  oldDoctor.RemovePatient(this);
               }
               if (value != null)
               {
                  this.doctor = value;
                  this.doctor.AddPatient(this);
               }
            }
         }
      }
   
   }
}