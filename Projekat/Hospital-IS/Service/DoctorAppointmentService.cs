using Model;
using Storages;
using System;
using System.Collections.Generic;

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

        public List<DoctorAppointment> GetAllByPatient(int patientId)
        {
            List<DoctorAppointment> patientAppointments = new List<DoctorAppointment>();
            foreach (DoctorAppointment docApp in allAppointments)
            {
                if (docApp.Patient.Id == patientId)
                {
                    patientAppointments.Add(docApp);
                }
            }
            return patientAppointments;
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
                doctorAppointment.Id = AppointmentService.Instance.GenerateAppointmentID();
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
                    if (doctorAppointment.AppointmentStart.Equals(doctorApp.AppointmentStart) && doctorAppointment.Doctor.Id.Equals(doctorApp.Doctor.Id))
                    {
                        allAppointments.Remove(doctorApp);
                        afs.SaveAppointment(allAppointments);
                        break;
                    }
                }
            }

        }

        public void UpdateAppointment(DoctorAppointment oldDoctorAppointment, DoctorAppointment newDoctorAppointment)
        {
            for (int i = 0; i < allAppointments.Count; i++)
            {
                if (newDoctorAppointment.Id == allAppointments[i].Id)
                {
                    allAppointments.Remove(allAppointments[i]);
                    allAppointments.Insert(i, newDoctorAppointment);
                    afs.SaveAppointment(allAppointments);
                    return;
                }
            }
        }

        public List<DoctorAppointment> GetFutureAppointmentsByPatient(int patientId)
        {
            List<DoctorAppointment> futurePatientAppointments = new List<DoctorAppointment>();
            List<DoctorAppointment> allPatientAppointmets = GetAllByPatient(patientId);
            foreach (DoctorAppointment doctorAppointment in allPatientAppointmets)
            {
                if (doctorAppointment.AppointmentStart >= DateTime.Now)
                {
                    futurePatientAppointments.Add(doctorAppointment);
                }
            }
            return futurePatientAppointments;
        }


        public void ReloadDoctorAppointments()
        {
            allAppointments = afs.GetAll();
        }

        public List<DoctorAppointment> GetAllByRoom(int roomId)
        {
            List<DoctorAppointment> roomAppointments = new List<DoctorAppointment>();
            foreach (DoctorAppointment roomApp in allAppointments)
            {
                if (roomApp.Room == roomId)
                {
                    roomAppointments.Add(roomApp);
                }
            }
            return roomAppointments;
        }

        public List<DoctorAppointment> GetAllByDoctorAndDates(int idDoctor, List<DateTime> dates)
        {
            List<DoctorAppointment> reservedAppointments = new List<DoctorAppointment>();
            List<DateTime> datesWithoutTime = new List<DateTime>();
            foreach (DateTime date in dates)
            {
                datesWithoutTime.Add(date.Date);
            }
            foreach (DoctorAppointment docApp in allAppointments)
            {
                if (docApp.Doctor.Id == idDoctor && datesWithoutTime.Contains(docApp.AppointmentStart.Date))
                {
                    reservedAppointments.Add(docApp);
                }
            }
            return reservedAppointments;
        }

        public int GetNumberOfAppointmentsInTimeRange(int patientId, DateTime timeRangeStart, DateTime timeRangeEnd)
        {
            int numberOfAppointments = 0;
            foreach (DoctorAppointment docApp in allAppointments)
            {
                if (docApp.Patient.Id == patientId && docApp.AppointmentStart >= timeRangeStart && docApp.AppointmentStart <= timeRangeEnd)
                {
                    numberOfAppointments++;
                }
            }
            return numberOfAppointments;
        }

        public DoctorAppointment GetAppointmentById(int appointmentId)
        {
            foreach (DoctorAppointment doctorAppointment in allAppointments)
            {
                if (doctorAppointment.Id == appointmentId)
                {
                    return doctorAppointment;
                }
            }
            return null;
        }

        public int GetNumberOfAppointmentsByMonth(int patientId, string month)
        {
            int numberOfAppointmentsForMonth = 0;
            switch (month)
            {
                case "Jan":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 1);
                    break;
                case "Feb":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 2);
                    break;
                case "Mar":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 3);
                    break;
                case "Apr":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 4);
                    break;
                case "May":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 5);
                    break;
                case "June":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 6);
                    break;
                case "July":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 7);
                    break;
                case "Aug":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 8);
                    break;
                case "Sep":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 9);
                    break;
                case "Oct":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 10);
                    break;
                case "Nov":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 11);
                    break;
                case "Dec":
                    numberOfAppointmentsForMonth = CheckMonth(patientId, 12);
                    break;
            }           
            return numberOfAppointmentsForMonth;
        }

        private int CheckMonth(int patientId,int month)
        {
            int counter = 0;
            foreach (DoctorAppointment appointment in GetAllByPatient(patientId))
            {
                if (appointment.AppointmentStart.Month == month)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}
