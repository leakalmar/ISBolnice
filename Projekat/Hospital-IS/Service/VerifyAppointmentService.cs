using Hospital_IS.Enums;
using Model;
using Service;
using System;
using System.Collections.Generic;


namespace Hospital_IS.Service
{
    public class VerifyAppointmentService
    {
        private static VerifyAppointmentService instance = null;
        public static VerifyAppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VerifyAppointmentService();
                }
                return instance;
            }
        }

        public bool VerifyAppointment(DoctorAppointment doctorAppointment)
        {
            List<Appointment> docAppsByRoom = new List<Appointment>(DoctorAppointmentService.Instance.GetAllByRoom(doctorAppointment.Room));
            List<Appointment> classicAppsByRoom = AppointmentService.Instance.GetAppByRoom(doctorAppointment.Room);
            List<Appointment> appsByDoctor = new List<Appointment>(DoctorAppointmentService.Instance.GetAllByDoctor(doctorAppointment.Doctor.Id));
            Boolean isVerified = true;

            if (!AppointmentService.Instance.CheckAppointment(docAppsByRoom, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd))
                isVerified = false;
            if (!AppointmentService.Instance.CheckAppointment(classicAppsByRoom, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd) && isVerified == true)
                isVerified = false;
            if (!AppointmentService.Instance.CheckAppointment(appsByDoctor, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd) && isVerified == true)
                isVerified = false;
            if (!IsDoctorWorking(doctorAppointment.Doctor, doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd) && isVerified == true)
                isVerified = false;

            return isVerified;
        }

        private bool IsDoctorWorking(Doctor doctor, DateTime appointmentStart, DateTime appointmentEnd)
        {
            if (IsDoctorOnVacation(doctor, appointmentStart, appointmentEnd))
                return false;

            if (doctor.WorkShift.Equals(WorkDayShift.FirstShift))
            {
                if (appointmentStart.TimeOfDay >= new TimeSpan(8, 0, 0) && appointmentEnd.TimeOfDay <= new TimeSpan(14, 0, 0))
                    return true;
            }
            else
            {
                if (appointmentStart.TimeOfDay >= new TimeSpan(14, 0, 0) && appointmentEnd.TimeOfDay <= new TimeSpan(20, 0, 0))
                    return true;
            }
            return false;
        }

        private bool IsDoctorOnVacation(Doctor doctor, DateTime appointmentStart, DateTime appointmentEnd)
        {
            DateTime VacationTimeEnd = doctor.VacationTimeStart.AddDays(14);
            if (appointmentStart > doctor.VacationTimeStart && appointmentStart < VacationTimeEnd)
                return true;
            if (appointmentEnd > doctor.VacationTimeStart && appointmentEnd < VacationTimeEnd)
                return true;
            if (appointmentStart < doctor.VacationTimeStart && appointmentEnd > VacationTimeEnd)
                return true;

            return false;
        }

        public bool VerifyAppointmentByPatient(DoctorAppointment doctorAppointment, int idPatient)
        {
            bool isFree = true;
            foreach (DoctorAppointment patientAppointment in DoctorAppointmentService.Instance.GetAllByPatient(idPatient))
            {
                if (doctorAppointment.AppointmentStart == patientAppointment.AppointmentStart)
                {
                    isFree = false;
                    return isFree;
                }
            }
            return isFree;
        }
    }
}
