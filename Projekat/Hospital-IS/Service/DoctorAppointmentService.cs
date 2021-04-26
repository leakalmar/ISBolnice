using Hospital_IS.DoctorView;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Service
{
    class DoctorAppointmentService
    {
        private AppointmentFileStorage afs = new AppointmentFileStorage();
        public List<DoctorAppointment> allAppointments { get; set; }

        private static DoctorAppointmentService instance = null;
        public static DoctorAppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorAppointmentService();
                }
                return instance;
            }
        }

        private DoctorAppointmentService()
        {
            allAppointments = afs.GetAll();
        }

        public List<DoctorAppointment> GetAllByDoctor(int doctorId)
        {
            List<DoctorAppointment> doctorAppointments = new List<DoctorAppointment>();
            foreach (DoctorAppointment docApp in allAppointments)
            {
                if (docApp.Doctor.Id == doctorId)
                {
                    doctorAppointments.Add(docApp);
                }
            }
            return doctorAppointments;
        }

        public void AddAppointment(DoctorAppointment doctorAppointment)
        {
            if (doctorAppointment == null)
            {
                return;
            }

            if (allAppointments == null)
            {
                allAppointments = new List<DoctorAppointment>();

            }

            if (!allAppointments.Contains(doctorAppointment))
            {
                allAppointments.Add(doctorAppointment);
                afs.SaveAppointment(allAppointments);
            }
        }

        public void RemoveAppointment(DoctorAppointment doctorAppointment)
        {
            if (doctorAppointment == null)
            {
                return;
            }

            if (allAppointments != null)
            {
                foreach (DoctorAppointment doctorApp in allAppointments)
                {
                    if (doctorAppointment.AppointmentStart.Equals(doctorApp.AppointmentStart))
                    {
                        allAppointments.Remove(doctorApp);
                        afs.SaveAppointment(allAppointments);
                        break;
                    }
                }
            }

        }

        public void UpdateAppointment(DoctorAppointment doctorAppointment)
        {

        }

        private List<DoctorAppointment> GenerateAppointmentForDoctor(SelectedDatesCollection dates, int idRoom, AppointmetType type, String duration, Patient patient)
        {
            List<DoctorAppointment> appList = new List<DoctorAppointment>();
            String[] parts = duration.Split(".");



            foreach (DateTime d in dates)
            {
                DateTime last;
                if (d.Date == DateTime.Now.Date)
                {
                    last = new DateTime(d.Year, d.Month, d.Day, DateTime.Now.Hour, 00, 00);
                }
                else
                {
                    last = new DateTime(d.Year, d.Month, d.Day, 8, 00, 00);

                }

                while (last.TimeOfDay < new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 20, 00, 00).TimeOfDay)
                {
                    if (type == AppointmetType.CheckUp)
                    {
                        DoctorAppointment dt = new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, last.Hour, last.Minute, 0), AppointmetType.CheckUp, true, idRoom, DoctorHomePage.Instance.GetDoctor(), patient);
                        appList.Add(dt);
                        last = last.AddMinutes(30);
                    }
                    else
                    {
                        int hours = 0;
                        int minutes = 0;
                        if (parts.GetValue(0) != "")
                        {
                            hours = int.Parse((string)parts.GetValue(0));
                            minutes = 0;
                            if (parts.Length == 2)
                            {
                                minutes = 30;
                            }
                            DoctorAppointment dt = new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, last.Hour, last.Minute, 0), AppointmetType.Operation, true, idRoom, DoctorHomePage.Instance.GetDoctor(), patient);
                            dt.AppointmentEnd = new DateTime(d.Year, d.Month, d.Day, last.AddHours(hours).Hour, last.AddMinutes(minutes).Minute, 0);
                            last = last.AddMinutes(30);
                        }
                        else
                        {
                            return appList;
                        }
                    }
                }
            }
            return appList;
        }

        private bool VerifyAppointment(DoctorAppointment doctorAppointment, List<Appointment> roomAppointments)
        {
            bool isReserved = false;
            foreach (DoctorAppointment hospital in Hospital.Instance.allAppointments)
            {
                if (doctorAppointment.AppointmentStart == hospital.AppointmentStart && doctorAppointment.Doctor.Id.Equals(hospital.Doctor.Id))
                {
                    isReserved = true;
                    return isReserved;
                }
            }
            //Dodati proveru za sobe
            throw new NotImplementedException();
        }

        public List<DoctorAppointment> SuggestAppointmetsToPatient(String timeSlot, Doctor doctor, DateTime da, Boolean priority)
        {
            throw new NotImplementedException();
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(DateTime date, int idRoom, AppointmetType type, String duration)
        {
            throw new NotImplementedException();
        }
    }
}
