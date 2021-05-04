﻿using Hospital_IS.DoctorView;
using Hospital_IS.DTOs;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
                if (oldDoctorAppointment.AppointmentStart == allAppointments[i].AppointmentStart && oldDoctorAppointment.Room == allAppointments[i].Room && oldDoctorAppointment.Doctor.Id == allAppointments[i].Doctor.Id)
                {
                    allAppointments.Remove(allAppointments[i]);
                    allAppointments.Insert(i, newDoctorAppointment);
                    afs.SaveAppointment(allAppointments);
                    return;
                }
            }
        }

        private List<DoctorAppointment> GenerateAppointmentsForDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
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
                while (lastTimeCreated.TimeOfDay < new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 00, 00).TimeOfDay)
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

        private List<DoctorAppointment> GenerateAppointmentForPatient(String timeSlot, Doctor doctor, Patient patient, DateTime date, Boolean priority)
        {
            int slotStart = 8;
            if (timeSlot.Equals("8:00-11:00"))
            {
                slotStart = 8;
            }
            else if (timeSlot.Equals("11:00-14:00"))
            {
                slotStart = 11;
            }
            else if (timeSlot.Equals("14:00-17:00"))
            {
                slotStart = 14;
            }
            else if (timeSlot.Equals("17:00-20:00"))
            {
                slotStart = 17;
            }
            /*
            List<DoctorAppointment> allPossibleAppointments = new List<DoctorAppointment>();
            DateTime possibleAppointmentTime;
            if (date.Date == DateTime.Now.Date)
            {
                possibleAppointmentTime = new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, 30, 00);
            }
            else
            {
                possibleAppointmentTime = new DateTime(date.Year, date.Month, date.Day, slotStart, 00, 00);
            }

            while (possibleAppointmentTime.TimeOfDay < new DateTime(date.Year, date.Month, date.Day, 20, 00, 00).TimeOfDay || possibleAppointmentTime.Hour < slotStart + slotLength)
            {
                allPossibleAppointments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, possibleAppointmentTime.Hour, possibleAppointmentTime.Minute, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient));
                possibleAppointmentTime = possibleAppointmentTime.AddMinutes(30);
            }
            */
            List<DoctorAppointment> allPossibleAppointments = new List<DoctorAppointment>();
            DoctorAppointment app1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmentType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app1);
            DoctorAppointment app2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmentType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app2);
            DoctorAppointment app3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmentType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app3);
            DoctorAppointment app4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmentType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app4);
            DoctorAppointment app5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmentType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app5);
            DoctorAppointment app6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmentType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app6);

            if (priority == true)
            {
                foreach (Doctor doc in Hospital.Instance.Doctors)
                {
                    if (!doc.Id.Equals(doctor.Id))
                    {
                        DoctorAppointment appTime1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime1);
                        DoctorAppointment appTime2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime2);
                        DoctorAppointment appTime3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime3);
                        DoctorAppointment appTime4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime4);
                        DoctorAppointment appTime5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime5);
                        DoctorAppointment appTime6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmentType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime6);
                    }
                }
            }
            return allPossibleAppointments;
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

        public List<DoctorAppointment> SuggestAppointmentsToPatient(String timeSlot, Doctor doctor, Patient patient, DateTime date, Boolean priority)
        {
            List<DoctorAppointment> availableAppointments = new List<DoctorAppointment>();
            List<DoctorAppointment> allPossibleAppointments = GenerateAppointmentForPatient(timeSlot, doctor, patient, date, priority);
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
                if (doctorAppointment.AppointmentStart.Date >= DateTime.Today)
                {
                    futurePatientAppointments.Add(doctorAppointment);
                }
            }
            return futurePatientAppointments;
        }


        public List<SuggestedEmergencyAppDTO> GenerateEmergencyAppointmentsForDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
        {
            List<DoctorAppointment> appointments = new List<DoctorAppointment>();

            List<Doctor> doctors = DoctorService.Instance.GetDoctorsBySpecialty(tempAppointment.Doctor.Specialty.Name);

            foreach (Doctor doc in doctors)
            {
                tempAppointment.Doctor = doc;
                List<DoctorAppointment> allPossibleAppointments = GenerateAppointmentsForDoctor(dates, tempAppointment);
                appointments.AddRange(allPossibleAppointments);
            }

            int durationInMinutes = (int)(tempAppointment.AppointmentEnd - tempAppointment.AppointmentStart).TotalMinutes;
            List<SuggestedEmergencyAppDTO> suggestedAppointments = FormEmergencyAppDTOs(appointments, durationInMinutes);
            suggestedAppointments.Sort((x, y) => x.ConflictingAppointments.Count.CompareTo(y.ConflictingAppointments.Count));
            SetConflictingIsUrgent(suggestedAppointments);
            CheckIfConflictingIsStarted(suggestedAppointments);

            return suggestedAppointments;
        }

        public List<SuggestedEmergencyAppDTO> GenerateEmergencyAppointmentsForSecretary(EmergencyAppointmentDTO emerAppointmentDTO)
        {
            List<DoctorAppointment> appointments = new List<DoctorAppointment>();

            List<Doctor> doctors = DoctorService.Instance.GetDoctorsBySpecialty(emerAppointmentDTO.Specialty.Name);

            foreach (Doctor doc in doctors)
            {
                DateTime appointmentStart = RoundUp(DateTime.Now, TimeSpan.FromMinutes(15));
                for (int i = 0; i < 4; i++)
                {
                    appointments.Add(new DoctorAppointment(appointmentStart, appointmentStart.AddMinutes(emerAppointmentDTO.DurationInMinutes), emerAppointmentDTO.AppointmetType,
                        emerAppointmentDTO.Room.RoomId, doc, emerAppointmentDTO.Patient));
                    appointmentStart = appointmentStart.AddMinutes(15);
                }
            }

            List<SuggestedEmergencyAppDTO> suggestedAppointments = FormEmergencyAppDTOs(appointments, emerAppointmentDTO.DurationInMinutes);
            suggestedAppointments.Sort((x, y) => x.TotalReshedulePeriodInHours.CompareTo(y.TotalReshedulePeriodInHours));
            suggestedAppointments = CheckIfConflictingIsUrgent(suggestedAppointments);
            CheckIfConflictingIsStarted(suggestedAppointments);

            return suggestedAppointments;
        }

        private List<SuggestedEmergencyAppDTO> FormEmergencyAppDTOs(List<DoctorAppointment> appointments, int durationInMinutes)
        {
            List<SuggestedEmergencyAppDTO> suggestedAppointments = new List<SuggestedEmergencyAppDTO>();
            foreach (DoctorAppointment appointment in appointments)
            {
                SuggestedEmergencyAppDTO appDTO = new SuggestedEmergencyAppDTO(appointment);

                List<DoctorAppointment> ca = FindConflictingAppointments(appointment);
                foreach (DoctorAppointment da in ca)
                    appDTO.ConflictingAppointments.Add(da);
                List<RescheduledAppointmentDTO> ra = FindNextFreeAppointments(FindConflictingAppointments(appointment), durationInMinutes);
                foreach (RescheduledAppointmentDTO da in ra)
                    appDTO.RescheduledAppointments.Add(da);
                appDTO.CalculateTotalReschedulePeriod();

                suggestedAppointments.Add(appDTO);

            }

            return suggestedAppointments;
        }


        private DateTime RoundUp(DateTime date, TimeSpan time)
        {
            return new DateTime((date.Ticks + time.Ticks - 1) / time.Ticks * time.Ticks, date.Kind);
        }

        private List<DoctorAppointment> FindConflictingAppointments(DoctorAppointment appointment)
        {
            List<DoctorAppointment> conflictingAppointments = new List<DoctorAppointment>();
            foreach (DoctorAppointment app in allAppointments)
            {
                if ((appointment.Doctor.Id.Equals(app.Doctor.Id) || appointment.Room.Equals(app.Room)))
                {
                    if (CheckAppointmentDates(appointment, app))
                        conflictingAppointments.Add(app);
                }
            }

            return conflictingAppointments;
        }

        private bool CheckAppointmentDates(DoctorAppointment newAppointment, DoctorAppointment appointment)
        {
            if (AreAppointmentsOverlaping(newAppointment, appointment))
            {
                return true;
            }
            return false;
        }
        private bool AreAppointmentsOverlaping(DoctorAppointment newAppointment, DoctorAppointment appointment)
        {
            return (newAppointment.AppointmentStart >= appointment.AppointmentStart && newAppointment.AppointmentStart < appointment.AppointmentEnd)
                || (newAppointment.AppointmentEnd > appointment.AppointmentStart && newAppointment.AppointmentEnd <= appointment.AppointmentEnd)
                || (newAppointment.AppointmentStart <= appointment.AppointmentStart && newAppointment.AppointmentEnd >= appointment.AppointmentEnd);
        }

        private List<RescheduledAppointmentDTO> FindNextFreeAppointments(List<DoctorAppointment> oldAppointments, double emergencyAppDuration)
        {
            DateTime appointmentStart = DateTime.Now.AddMinutes(emergencyAppDuration);
            appointmentStart = RoundUp(appointmentStart, TimeSpan.FromMinutes(30));
            appointmentStart = appointmentStart.AddMinutes(60);
            List<RescheduledAppointmentDTO> newAppointments = new List<RescheduledAppointmentDTO>();

            while (oldAppointments.Count > 0)
            {
                bool rescheduled = false;
                for (int i = 0; i < oldAppointments.Count; i++)
                {
                    TimeSpan appointmentDuration = oldAppointments[i].AppointmentEnd - oldAppointments[i].AppointmentStart;
                    RescheduledAppointmentDTO appointment = new RescheduledAppointmentDTO(oldAppointments[i]);
                    appointment.NewDocAppointment.AppointmentStart = appointmentStart;
                    appointment.NewDocAppointment.AppointmentEnd = appointmentStart.Add(appointmentDuration);
                    if (appointment.NewDocAppointment.AppointmentStart.TimeOfDay >= new TimeSpan(8, 0, 0) && appointment.NewDocAppointment.AppointmentEnd.TimeOfDay < new TimeSpan(23, 0, 0)
                            && VerifyAppointment(appointment.NewDocAppointment))
                    {
                        newAppointments.Add(appointment);
                        oldAppointments.Remove(oldAppointments[i]);
                        appointmentStart = appointmentStart.Add(appointmentDuration);
                        rescheduled = true;
                    }
                }
                if (!rescheduled)
                {
                    appointmentStart = appointmentStart.AddMinutes(30);
                }
            }

            return newAppointments;

        }

        private List<SuggestedEmergencyAppDTO> CheckIfConflictingIsUrgent(List<SuggestedEmergencyAppDTO> suggestedEmergencyApps)
        {
            List<SuggestedEmergencyAppDTO> validSuggestedApp = new List<SuggestedEmergencyAppDTO>();
            foreach (SuggestedEmergencyAppDTO suggested in suggestedEmergencyApps)
            {
                suggested.CheckIfConflictingIsUrgent();
                if (!suggested.ConflictingIsUrgent)
                {
                    validSuggestedApp.Add(suggested);
                }
            }
            return validSuggestedApp;
        }

        private void SetConflictingIsUrgent(List<SuggestedEmergencyAppDTO> suggestedEmergencyApps)
        {
            foreach(SuggestedEmergencyAppDTO suggested in suggestedEmergencyApps)
            {
                suggested.CheckIfConflictingIsUrgent();
            }
        }

        private void CheckIfConflictingIsStarted(List<SuggestedEmergencyAppDTO> suggestedEmergencyApps)
        { 
            for (int i = 0; i < suggestedEmergencyApps.Count; i++)
            {
                foreach(DoctorAppointment conflicting in suggestedEmergencyApps[i].ConflictingAppointments)
                {
                    if(conflicting.AppointmentStart < DateTime.Now)
                    {
                        suggestedEmergencyApps.Remove(suggestedEmergencyApps[i]);
                    }
                }
            }
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
    }
}
