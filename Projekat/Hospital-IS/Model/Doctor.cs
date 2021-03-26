/***********************************************************************
 * Module:  Doctor.cs
 * Author:  Asus
 * Purpose: Definition of the Class Doctor
 ***********************************************************************/

using System;

namespace Model
{
   public class Doctor : Employee
   {
      public void ViewPatientDocuments(Patient patient)
      {
         // TODO: implement
      }
      
      public Boolean UpadatePatient(Patient patient)
      {
         // TODO: implement
         return null;
      }
      
      public Boolean AddPatietientDocument(Patient patient)
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
            newDoctorAppointment.SetDoctor(this);      
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
               oldDoctorAppointment.SetDoctor((Doctor)null);
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
               oldDoctorAppointment.SetDoctor((Doctor)null);
            tmpDoctorAppointment.Clear();
         }
      }
      public System.Collections.ArrayList patient;
      
      
      public System.Collections.ArrayList GetPatient()
      {
         if (patient == null)
            patient = new System.Collections.ArrayList();
         return patient;
      }
      
      
      public void SetPatient(System.Collections.ArrayList newPatient)
      {
         RemoveAllPatient();
         foreach (Patient oPatient in newPatient)
            AddPatient(oPatient);
      }
      
      
      public void AddPatient(Patient newPatient)
      {
         if (newPatient == null)
            return;
         if (this.patient == null)
            this.patient = new System.Collections.ArrayList();
         if (!this.patient.Contains(newPatient))
         {
            this.patient.Add(newPatient);
            newPatient.SetDoctor(this);      
         }
      }
      
      
      public void RemovePatient(Patient oldPatient)
      {
         if (oldPatient == null)
            return;
         if (this.patient != null)
            if (this.patient.Contains(oldPatient))
            {
               this.patient.Remove(oldPatient);
               oldPatient.SetDoctor((Doctor)null);
            }
      }
      
      
      public void RemoveAllPatient()
      {
         if (patient != null)
         {
            System.Collections.ArrayList tmpPatient = new System.Collections.ArrayList();
            foreach (Patient oldPatient in patient)
               tmpPatient.Add(oldPatient);
            patient.Clear();
            foreach (Patient oldPatient in tmpPatient)
               oldPatient.SetDoctor((Doctor)null);
            tmpPatient.Clear();
         }
      }
   
      private Specialty Specialty;
   
   }
}