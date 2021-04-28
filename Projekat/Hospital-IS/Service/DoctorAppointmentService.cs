﻿using Hospital_IS.DoctorView;
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

        private List<DoctorAppointment> GenerateAppointmentForDoctor(SelectedDatesCollection dates, int idRoom, AppointmetType type, TimeSpan duration, Patient patient)
        {
            List<DoctorAppointment> appointmentList = new List<DoctorAppointment>();

            foreach (DateTime d in dates)
            {
                DateTime lastTimeCreated;
                //Set date from witch could start appointments
                if (d.Date == DateTime.Now.Date)
                {
                    lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, DateTime.Now.Hour, 30, 00);
                }
                else
                {
                    lastTimeCreated = new DateTime(d.Year, d.Month, d.Day, 8, 00, 00);
                }

                while (lastTimeCreated.TimeOfDay < new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 20, 00, 00).TimeOfDay)
                {
                    if (type == AppointmetType.CheckUp)
                    {
                        appointmentList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, lastTimeCreated.Hour, lastTimeCreated.Minute, 0), AppointmetType.CheckUp, false, idRoom, DoctorHomePage.Instance.GetDoctor(), patient));
                        lastTimeCreated = lastTimeCreated.AddMinutes(30);
                    }
                    else
                    {
                        if(duration.TotalMinutes != 0)
                        {
                            DateTime startTime = new DateTime(d.Year, d.Month, d.Day, lastTimeCreated.Hour, lastTimeCreated.Minute, 0);
                            DoctorAppointment newAppointment = new DoctorAppointment(startTime, AppointmetType.Operation, false, idRoom, DoctorHomePage.Instance.GetDoctor(), patient);
                            newAppointment.AppointmentEnd = startTime.Add(duration);
                            appointmentList.Add(newAppointment);
                            lastTimeCreated = lastTimeCreated.AddMinutes(30);
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

        private List<DoctorAppointment> GenerateAppointmentForPatient(String timeSlot, Doctor doctor,Patient patient, DateTime date, Boolean priority)
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

            List<DoctorAppointment> allPossibleAppointments = new List<DoctorAppointment>();
            DoctorAppointment app1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app1);
            DoctorAppointment app2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app2);
            DoctorAppointment app3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app3);
            DoctorAppointment app4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app4);
            DoctorAppointment app5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app5);
            DoctorAppointment app6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, patient);
            allPossibleAppointments.Add(app6);

            if (priority == true)
            {
                foreach (Doctor doc in Hospital.Instance.Doctors)
                {
                    if (!doc.Id.Equals(doctor.Id))
                    {
                        DoctorAppointment appTime1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime1);
                        DoctorAppointment appTime2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime2);
                        DoctorAppointment appTime3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime3);
                        DoctorAppointment appTime4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime4);
                        DoctorAppointment appTime5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime5);
                        DoctorAppointment appTime6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, patient);
                        allPossibleAppointments.Add(appTime6);
                    }
                }
            }
            return allPossibleAppointments;
        }

        private bool VerifyAppointment(DoctorAppointment doctorAppointment, List<Appointment> roomAppointments)
        {
            bool isFree = true;
            foreach (DoctorAppointment hospital in allAppointments)
            {
                if (doctorAppointment.AppointmentStart == hospital.AppointmentStart && doctorAppointment.Doctor.Id.Equals(hospital.Doctor.Id))
                {
                    isFree = false;
                    return isFree;
                }
            }

            isFree = AppointmentService.Instance.CheckAppointment(roomAppointments, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd);
            return isFree;
        }

        private bool VerifyAppointmentByPatient(DoctorAppointment doctorAppointment,int idPatient)
        {
            bool isFree = true;
            foreach (DoctorAppointment patientAppointment in GetAllAppointmentsByPatient(idPatient))
            {
                if (doctorAppointment.AppointmentStart == patientAppointment.AppointmentStart)
                {
                    isFree = false;
                    return isFree;
                }
            }
            return isFree;
        }

        public List<DoctorAppointment> SuggestAppointmentsToPatient(String timeSlot, Doctor doctor,Patient patient, DateTime date, Boolean priority)
        {
            List<DoctorAppointment> availableAppointments = new List<DoctorAppointment>();
            List<DoctorAppointment> allPossibleAppointments = GenerateAppointmentForPatient(timeSlot, doctor, patient, date, priority);
            List<Appointment> roomAppointments = AppointmentService.Instance.getAppByRoom(doctor.PrimaryRoom);
            foreach (DoctorAppointment doctorAppointment in allPossibleAppointments)
            {
                bool isFree = VerifyAppointment(doctorAppointment, roomAppointments);
                if (isFree)
                {
                    availableAppointments.Add(doctorAppointment);
                }
            }
            return availableAppointments;
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(SelectedDatesCollection dates, int idRoom, AppointmetType type, TimeSpan duration, Patient patient)
        {
            List<DoctorAppointment> availableAppointments = new List<DoctorAppointment>();
            List<DoctorAppointment> allPossibleAppointments = GenerateAppointmentForDoctor(dates,idRoom,type,duration,patient);
            List<Appointment> roomAppointments = AppointmentService.Instance.getAppByRoom(idRoom);
            foreach (DoctorAppointment doctorAppointment in allPossibleAppointments)
            {
                bool isFree = VerifyAppointment(doctorAppointment, roomAppointments);
                if (isFree)
                {
                    isFree = VerifyAppointmentByPatient(doctorAppointment, patient.Id);
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
            List<DoctorAppointment> allPatientAppointmets = GetAllAppointmentsByPatient(patientId);
            foreach(DoctorAppointment doctorAppointment in allPatientAppointmets)
            {
                if (doctorAppointment.AppointmentStart.Date >= DateTime.Today)
                {
                    futurePatientAppointments.Add(doctorAppointment);
                }
            }
            return futurePatientAppointments;
        }

        public List<DoctorAppointment> GetAllAppointmentsByPatient(int patientId)
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
