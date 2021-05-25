using Enums;
using Hospital_IS.DTOs;
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

        public List<DoctorAppointment> GenerateAppointmentsForDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
        {
            List<DoctorAppointment> appointmentList = new List<DoctorAppointment>();
            DateTime lastTimeCreated = DateTime.Now;

            foreach (DateTime d in dates)
            {                
                //Set date from witch could start appointments
                if (d.Date == DateTime.Now.Date)
                {
                    if (DateTime.Now.Minute < 15)
                    {
                        lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, DateTime.Now.Hour, 15, 00);
                    }
                    else if (DateTime.Now.Minute < 30)
                    {
                        lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, DateTime.Now.Hour, 30, 00);
                    }
                    else if (DateTime.Now.Minute < 45)
                    {
                        lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, DateTime.Now.Hour, 45, 00);
                    }
                    else if (DateTime.Now.Minute >= 45)
                    {
                        lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, DateTime.Now.Hour + 1, 00, 00);
                    }

                }
                else
                {
                    lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, 8, 00, 00);
                }

                DoctorAppointment newAppointment;
                while (lastTimeCreated.TimeOfDay < new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 20, 00, 00).TimeOfDay)
                {
                    if (tempAppointment.Type == AppointmentType.CheckUp)
                    {
                        newAppointment = new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, lastTimeCreated.Hour, lastTimeCreated.Minute, 0), AppointmentType.CheckUp, false, tempAppointment.Room, tempAppointment.Doctor, tempAppointment.Patient);
                        newAppointment.IsUrgent = tempAppointment.IsUrgent;
                        appointmentList.Add(newAppointment);
                        lastTimeCreated = lastTimeCreated.AddMinutes(15);
                    }
                    else
                    {
                        int duration = (int)tempAppointment.AppointmentEnd.Subtract(tempAppointment.AppointmentStart).TotalMinutes;
                        if (duration != 0)
                        {
                            DateTime startTime = new DateTime(d.Year, d.Month, d.Day, lastTimeCreated.Hour, lastTimeCreated.Minute, 0);
                            newAppointment = new DoctorAppointment(startTime, AppointmentType.Operation, false, tempAppointment.Room, tempAppointment.Doctor, tempAppointment.Patient);
                            newAppointment.AppointmentEnd = startTime.AddMinutes(duration);
                            newAppointment.IsUrgent = tempAppointment.IsUrgent;
                            appointmentList.Add(newAppointment);
                            lastTimeCreated = lastTimeCreated.AddMinutes(15);
                        }
                        else
                        {
                            return appointmentList;
                        }
                    }
                }
            }
            return appointmentList;
        }

        private List<DoctorAppointment> GenerateAppointmentForPatient(PossibleAppointmentForPatientDTO possibleAppointment)
        {
            int slotStart = 8;
            int slotLength = 3;
            if (possibleAppointment.TimeSlot == null)
            {
                slotStart = 8;
            }
            else if (possibleAppointment.TimeSlot.Equals("0"))
            {
                slotStart = 8;
            }
            else if (possibleAppointment.TimeSlot.Equals("1"))
            {
                slotStart = 11;
            }
            else if (possibleAppointment.TimeSlot.Equals("2"))
            {
                slotStart = 14;
            }
            else if (possibleAppointment.TimeSlot.Equals("3"))
            {
                slotStart = 17;
            }
            List<DoctorAppointment> allPossibleAppointments = new List<DoctorAppointment>();
            DateTime possibleAppointmentTime = SetPossibleAppointmentTime(possibleAppointment.Date, slotStart);           

            if (possibleAppointment.Priority == true)
            {
                foreach (Doctor doc in DoctorService.Instance.AllDoctors)
                {
                    while (possibleAppointmentTime.Hour < slotStart + slotLength)
                    {
                        allPossibleAppointments.Add(new DoctorAppointment(new DateTime(possibleAppointment.Date.Year, possibleAppointment.Date.Month, possibleAppointment.Date.Day, possibleAppointmentTime.Hour, possibleAppointmentTime.Minute, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, possibleAppointment.Patient));
                        possibleAppointmentTime = possibleAppointmentTime.AddMinutes(30);
                    }
                    possibleAppointmentTime = SetPossibleAppointmentTime(possibleAppointment.Date, slotStart);
                }
            }
            else
            {
                while (possibleAppointmentTime.Hour < slotStart + slotLength)
                {
                    allPossibleAppointments.Add(new DoctorAppointment(new DateTime(possibleAppointment.Date.Year, possibleAppointment.Date.Month, possibleAppointment.Date.Day, possibleAppointmentTime.Hour, possibleAppointmentTime.Minute, 0), AppointmentType.CheckUp, false, possibleAppointment.Doctor.PrimaryRoom, possibleAppointment.Doctor, possibleAppointment.Patient));
                    possibleAppointmentTime = possibleAppointmentTime.AddMinutes(30);
                }
            }
            return allPossibleAppointments;
        }

        private DateTime SetPossibleAppointmentTime(DateTime date, int slotStart)
        {
            if (date.Date == DateTime.Now.Date)
            {
                return new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, 30, 00);
            }
            else
            {
                return new DateTime(date.Year, date.Month, date.Day, slotStart, 00, 00);
            }
        }

        public bool VerifyAppointment(DoctorAppointment doctorAppointment)
        {
            List<Appointment> docAppsByRoom = new List<Appointment>(GetAllByRoom(doctorAppointment.Room));
            List<Appointment> classicAppsByRoom = AppointmentService.Instance.GetAppByRoom(doctorAppointment.Room);
            List<Appointment> appsByDoctor = new List<Appointment>(GetAllByDoctor(doctorAppointment.Doctor.Id));

            if (!AppointmentService.Instance.CheckAppointment(docAppsByRoom, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd))
                return false;
            if (!AppointmentService.Instance.CheckAppointment(classicAppsByRoom, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd))
                return false;
            if (!AppointmentService.Instance.CheckAppointment(appsByDoctor, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd))
                return false;

            return true;
        }

        private bool VerifyAppointmentByPatient(DoctorAppointment doctorAppointment, int idPatient)
        {
            bool isFree = true;
            foreach (DoctorAppointment patientAppointment in GetAllByPatient(idPatient))
            {
                if (doctorAppointment.AppointmentStart == patientAppointment.AppointmentStart)
                {
                    isFree = false;
                    return isFree;
                }
            }
            return isFree;
        }

        public List<DoctorAppointment> SuggestAppointmentsToPatient(PossibleAppointmentForPatientDTO possibleAppointment)
        {
            List<DoctorAppointment> availableAppointments = new List<DoctorAppointment>();
            List<DoctorAppointment> allPossibleAppointments = GenerateAppointmentForPatient(possibleAppointment);
            foreach (DoctorAppointment doctorAppointment in allPossibleAppointments)
            {
                bool isFree = VerifyAppointment(doctorAppointment);
                if (isFree)
                {
                    availableAppointments.Add(doctorAppointment);
                }
            }
            return availableAppointments;
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
        {
            List<DoctorAppointment> allAppointments = new List<DoctorAppointment>();

            allAppointments = GetAvailableAppointmentsByDoctor(dates, tempAppointment);
            allAppointments.AddRange(GetAllByDoctorAndDates(tempAppointment.Doctor.Id, dates));

            return allAppointments;
        }

        private List<DoctorAppointment> GetAvailableAppointmentsByDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
        {
            List<DoctorAppointment> availableAppointments = new List<DoctorAppointment>();
            List<DoctorAppointment> allPossibleAppointments = GenerateAppointmentsForDoctor(dates, tempAppointment);
            foreach (DoctorAppointment doctorAppointment in allPossibleAppointments)
            {
                bool isFree = VerifyAppointment(doctorAppointment);
                if (isFree)
                {
                    isFree = VerifyAppointmentByPatient(doctorAppointment, tempAppointment.Patient.Id);
                }
                if (isFree)
                {
                    availableAppointments.Add(doctorAppointment);
                }
            }
            return availableAppointments;
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
