using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public List<Prescription> Prescription { get; set; }
        public ObservableCollection<Test> Test { get; set; }
        public List<Report> Reports { get; set; }
        public ObservableCollection<Hospitalization> Hospitalization { get; set; }
        public ObservableCollection<Therapy> Therapies { get; set; }

        public MedicalHistory()
        {
            Prescription = new List<Prescription>();
            Test = new ObservableCollection<Test>();
            Reports = new List<Report>();
            Hospitalization = new ObservableCollection<Hospitalization>();
            Therapies = new ObservableCollection<Therapy>();
        }

        public void AddPrescription(Prescription newPrescription)
        {
            if (newPrescription == null)
                return;
            if (this.Prescription == null)
                this.Prescription = new List<Prescription>();
            if (!this.Prescription.Contains(newPrescription))
                this.Prescription.Add(newPrescription);
        }

        public void RemovePrescription(Prescription oldPrescription)
        {
            if (oldPrescription == null)
                return;
            if (this.Prescription != null)
                if (this.Prescription.Contains(oldPrescription))
                    this.Prescription.Remove(oldPrescription);
        }

        public void RemoveAllPrescription()
        {
            if (Prescription != null)
                Prescription.Clear();
        }



        public void AddTest(Test newTest)
        {
            if (newTest == null)
                return;
            if (this.Test == null)
                this.Test = new ObservableCollection<Test>();
            if (!this.Test.Contains(newTest))
                this.Test.Add(newTest);
        }

        public void RemoveTest(Test oldTest)
        {
            if (oldTest == null)
                return;
            if (this.Test != null)
                if (this.Test.Contains(oldTest))
                    this.Test.Remove(oldTest);
        }

        public void RemoveAllTest()
        {
            if (Test != null)
                Test.Clear();
        }



        public void AddReport(Report newReport)
        {
            if (newReport == null)
                return;
            if (this.Reports == null)
                this.Reports = new List<Report>();
            if (!this.Reports.Contains(newReport))
                this.Reports.Add(newReport);
        }


        public void RemoveReport(Report oldReport)
        {
            if (oldReport == null)
                return;
            if (this.Reports != null)
                if (this.Reports.Contains(oldReport))
                    this.Reports.Remove(oldReport);
        }

        public void RemoveAllReports()
        {
            if (Reports != null)
                Reports.Clear();
        }



        public void AddHospitalization(Hospitalization newHospitalization)
        {
            if (newHospitalization == null)
                return;
            if (this.Hospitalization == null)
                this.Hospitalization = new ObservableCollection<Hospitalization>();
            if (!this.Hospitalization.Contains(newHospitalization))
                this.Hospitalization.Add(newHospitalization);
        }


        public void RemoveHospitalization(Hospitalization oldHospitalization)
        {
            if (oldHospitalization == null)
                return;
            if (this.Hospitalization != null)
                if (this.Hospitalization.Contains(oldHospitalization))
                    this.Hospitalization.Remove(oldHospitalization);
        }

        public void RemoveAllHospitalization()
        {
            if (Hospitalization != null)
                Hospitalization.Clear();
        }



        public void AddTherapy(Therapy newTherapy)
        {
            if (newTherapy == null)
                return;
            if (this.Therapies == null)
                this.Therapies = new ObservableCollection<Therapy>();
            if (!this.Therapies.Contains(newTherapy))
            {
                this.Therapies.Add(newTherapy);
            }
        }

        public void RemoveTherapy(Therapy oldTherapy)
        {
            if (oldTherapy == null)
                return;
            if (this.Therapies != null)
                if (this.Therapies.Contains(oldTherapy))
                {
                    this.Therapies.Remove(oldTherapy);
                }
        }

        public void RemoveAllTherapies()
        {
            if (Therapies != null)
            {
                Therapies.Clear();
            }
        }

        public List<Prescription> GetByAppointment(DoctorAppointment docApp)
        {
            List<Prescription> ret = new List<Prescription>();
            foreach (Prescription p in Prescription)
            {
                if (p.DatePrescribed.Date.Equals(docApp.AppointmentStart.Date))
                {
                    ret.Add(p);
                }
            }
            return ret;
        }

        public ObservableCollection<Prescription> GetPresciptionByReport(DateTime report)
        {
            ObservableCollection<Prescription> ret = new ObservableCollection<Prescription>();
            foreach (Prescription p in Prescription)
            {
                if (p.DatePrescribed == report)
                {
                    ret.Add(p);
                }
            }
            return ret;
        }
    }
}
