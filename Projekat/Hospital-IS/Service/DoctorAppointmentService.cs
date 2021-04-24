using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

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
                        break;
                    }
                }
            }

        }

        public void UpdateAppointment(DoctorAppointment doctorAppointment)
        {

        }

        private List<DoctorAppointment> GenerateAppointment()
        {
            throw new NotImplementedException();
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

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(DateTime date, int idRoom, AppointmetType type, TimeSpan duration)
        {
            throw new NotImplementedException();
        }
    }
}
