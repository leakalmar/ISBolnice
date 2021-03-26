/***********************************************************************
 * Module:  MedicalHistory.cs
 * Author:  Asus
 * Purpose: Definition of the Class MedicalHistory
 ***********************************************************************/

using System;

namespace Model
{
   public class MedicalHistory
   {
      public System.Collections.ArrayList prescription;
      
      /// <summary>
      /// Property for collection of Prescription
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Prescription
      {
         get
         {
            if (prescription == null)
               prescription = new System.Collections.ArrayList();
            return prescription;
         }
         set
         {
            RemoveAllPrescription();
            if (value != null)
            {
               foreach (Prescription oPrescription in value)
                  AddPrescription(oPrescription);
            }
         }
      }
      
      /// <summary>
      /// Add a new Prescription in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddPrescription(Prescription newPrescription)
      {
         if (newPrescription == null)
            return;
         if (this.prescription == null)
            this.prescription = new System.Collections.ArrayList();
         if (!this.prescription.Contains(newPrescription))
            this.prescription.Add(newPrescription);
      }
      
      /// <summary>
      /// Remove an existing Prescription from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemovePrescription(Prescription oldPrescription)
      {
         if (oldPrescription == null)
            return;
         if (this.prescription != null)
            if (this.prescription.Contains(oldPrescription))
               this.prescription.Remove(oldPrescription);
      }
      
      /// <summary>
      /// Remove all instances of Prescription from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllPrescription()
      {
         if (prescription != null)
            prescription.Clear();
      }
      public System.Collections.ArrayList test;
      
      /// <summary>
      /// Property for collection of Test
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Test
      {
         get
         {
            if (test == null)
               test = new System.Collections.ArrayList();
            return test;
         }
         set
         {
            RemoveAllTest();
            if (value != null)
            {
               foreach (Test oTest in value)
                  AddTest(oTest);
            }
         }
      }
      
      /// <summary>
      /// Add a new Test in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddTest(Test newTest)
      {
         if (newTest == null)
            return;
         if (this.test == null)
            this.test = new System.Collections.ArrayList();
         if (!this.test.Contains(newTest))
            this.test.Add(newTest);
      }
      
      /// <summary>
      /// Remove an existing Test from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveTest(Test oldTest)
      {
         if (oldTest == null)
            return;
         if (this.test != null)
            if (this.test.Contains(oldTest))
               this.test.Remove(oldTest);
      }
      
      /// <summary>
      /// Remove all instances of Test from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllTest()
      {
         if (test != null)
            test.Clear();
      }
      public System.Collections.ArrayList hospitalization;
      
      /// <summary>
      /// Property for collection of Hospitalization
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Hospitalization
      {
         get
         {
            if (hospitalization == null)
               hospitalization = new System.Collections.ArrayList();
            return hospitalization;
         }
         set
         {
            RemoveAllHospitalization();
            if (value != null)
            {
               foreach (Hospitalization oHospitalization in value)
                  AddHospitalization(oHospitalization);
            }
         }
      }
      
      /// <summary>
      /// Add a new Hospitalization in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddHospitalization(Hospitalization newHospitalization)
      {
         if (newHospitalization == null)
            return;
         if (this.hospitalization == null)
            this.hospitalization = new System.Collections.ArrayList();
         if (!this.hospitalization.Contains(newHospitalization))
            this.hospitalization.Add(newHospitalization);
      }
      
      /// <summary>
      /// Remove an existing Hospitalization from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveHospitalization(Hospitalization oldHospitalization)
      {
         if (oldHospitalization == null)
            return;
         if (this.hospitalization != null)
            if (this.hospitalization.Contains(oldHospitalization))
               this.hospitalization.Remove(oldHospitalization);
      }
      
      /// <summary>
      /// Remove all instances of Hospitalization from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllHospitalization()
      {
         if (hospitalization != null)
            hospitalization.Clear();
      }
   
   }
}