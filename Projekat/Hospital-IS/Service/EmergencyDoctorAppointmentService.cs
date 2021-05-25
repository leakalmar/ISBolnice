using Hospital_IS.DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Hospital_IS.Service
{
    class EmergencyDoctorAppointmentService
    {
        private static EmergencyDoctorAppointmentService instance = null;
        public static EmergencyDoctorAppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmergencyDoctorAppointmentService();
                }
                return instance;
            }
        }
        public List<SuggestedEmergencyAppDTO> GenerateEmergencyAppointmentsForDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
        {
            List<DoctorAppointment> appointments = new List<DoctorAppointment>();

            List<Doctor> doctors = DoctorService.Instance.GetDoctorsBySpecialty(tempAppointment.Doctor.Specialty.Name);

            foreach (Doctor doc in doctors)
            {
                tempAppointment.Doctor = doc;
                List<DoctorAppointment> allPossibleAppointments = DoctorAppointmentService.Instance.GenerateAppointmentsForDoctor(dates, tempAppointment);
                appointments.AddRange(allPossibleAppointments);
            }

            int durationInMinutes = (int)(tempAppointment.AppointmentEnd - tempAppointment.AppointmentStart).TotalMinutes;
            List<SuggestedEmergencyAppDTO> suggestedAppointments = FormEmergencyAppDTOs(appointments, durationInMinutes);
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
            foreach (DoctorAppointment app in DoctorAppointmentService.Instance.allAppointments)
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
                    if (appointment.NewDocAppointment.AppointmentStart.TimeOfDay >= new TimeSpan(8, 0, 0) && appointment.NewDocAppointment.AppointmentEnd.TimeOfDay < new TimeSpan(20, 0, 0)
                            && DoctorAppointmentService.Instance.VerifyAppointment(appointment.NewDocAppointment))
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
            foreach (SuggestedEmergencyAppDTO suggested in suggestedEmergencyApps)
            {
                suggested.CheckIfConflictingIsUrgent();
            }
        }

        private void CheckIfConflictingIsStarted(List<SuggestedEmergencyAppDTO> suggestedEmergencyApps)
        {
            for (int i = 0; i < suggestedEmergencyApps.Count; i++)
            {
                foreach (DoctorAppointment conflicting in suggestedEmergencyApps[i].ConflictingAppointments)
                {
                    if (conflicting.AppointmentStart < DateTime.Now)
                    {
                        suggestedEmergencyApps.Remove(suggestedEmergencyApps[i]);
                    }
                }
            }
        }
    }
}
