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
      private Specialty specialty;
      
      public void ViewPatientDocuments(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Boolean UpadatePatient(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Boolean AddPatietientDocument(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList doctorAppointment;
      
      /// <summary>
      /// Property for collection of DoctorAppointment
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
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
      
      /// <summary>
      /// Add a new DoctorAppointment in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddDoctorAppointment(DoctorAppointment newDoctorAppointment)
      {
         if (newDoctorAppointment == null)
            return;
         if (this.doctorAppointment == null)
            this.doctorAppointment = new System.Collections.ArrayList();
         if (!this.doctorAppointment.Contains(newDoctorAppointment))
         {
            this.doctorAppointment.Add(newDoctorAppointment);
            newDoctorAppointment.Doctor = this;
         }
      }
      
      /// <summary>
      /// Remove an existing DoctorAppointment from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveDoctorAppointment(DoctorAppointment oldDoctorAppointment)
      {
         if (oldDoctorAppointment == null)
            return;
         if (this.doctorAppointment != null)
            if (this.doctorAppointment.Contains(oldDoctorAppointment))
            {
               this.doctorAppointment.Remove(oldDoctorAppointment);
               oldDoctorAppointment.Doctor = null;
            }
      }
      
      /// <summary>
      /// Remove all instances of DoctorAppointment from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllDoctorAppointment()
      {
         if (doctorAppointment != null)
         {
            System.Collections.ArrayList tmpDoctorAppointment = new System.Collections.ArrayList();
            foreach (DoctorAppointment oldDoctorAppointment in doctorAppointment)
               tmpDoctorAppointment.Add(oldDoctorAppointment);
            doctorAppointment.Clear();
            foreach (DoctorAppointment oldDoctorAppointment in tmpDoctorAppointment)
               oldDoctorAppointment.Doctor = null;
            tmpDoctorAppointment.Clear();
         }
      }
      public System.Collections.ArrayList patient;
      
      /// <summary>
      /// Property for collection of Patient
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Patient
      {
         get
         {
            if (patient == null)
               patient = new System.Collections.ArrayList();
            return patient;
         }
         set
         {
            RemoveAllPatient();
            if (value != null)
            {
               foreach (Patient oPatient in value)
                  AddPatient(oPatient);
            }
         }
      }
      
      /// <summary>
      /// Add a new Patient in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddPatient(Patient newPatient)
      {
         if (newPatient == null)
            return;
         if (this.patient == null)
            this.patient = new System.Collections.ArrayList();
         if (!this.patient.Contains(newPatient))
         {
            this.patient.Add(newPatient);
            newPatient.Doctor = this;
         }
      }
      
      /// <summary>
      /// Remove an existing Patient from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemovePatient(Patient oldPatient)
      {
         if (oldPatient == null)
            return;
         if (this.patient != null)
            if (this.patient.Contains(oldPatient))
            {
               this.patient.Remove(oldPatient);
               oldPatient.Doctor = null;
            }
      }
      
      /// <summary>
      /// Remove all instances of Patient from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllPatient()
      {
         if (patient != null)
         {
            System.Collections.ArrayList tmpPatient = new System.Collections.ArrayList();
            foreach (Patient oldPatient in patient)
               tmpPatient.Add(oldPatient);
            patient.Clear();
            foreach (Patient oldPatient in tmpPatient)
               oldPatient.Doctor = null;
            tmpPatient.Clear();
         }
      }
   
   }
}