using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    class DoctorAppointmentController
    {
        private static DoctorAppointmentController instance = null;
        public static DoctorAppointmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorAppointmentController();
                }
                return instance;
            }
        }

        private DoctorAppointmentController()
        {

        }

        public void AddAppointment(DoctorAppointment doctorAppointment)
        {
            DoctorAppointmentService.Instance.AddAppointment(doctorAppointment);
        }

        public void RemoveAppointment(DoctorAppointment doctorAppointment)
        {
            DoctorAppointmentService.Instance.RemoveAppointment(doctorAppointment);
        }

        public void UpdateAppointment(DoctorAppointment doctorAppointment)
        {
            DoctorAppointmentService.Instance.UpdateAppointment(doctorAppointment);
        }

        public List<DoctorAppointment> SuggestAppointmetsToPatient(String timeSlot,Doctor doctor,Patient patient, DateTime date, Boolean priority)
        {
            return DoctorAppointmentService.Instance.SuggestAppointmetsToPatient(timeSlot, doctor, patient, date, priority);
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(DateTime date, int idRoom, AppointmetType type, TimeSpan duration)
        {
            DoctorAppointmentService.Instance.SuggestAppointmetsToDoctor(date, idRoom, type, duration);
            throw new NotImplementedException();
        }

        public List<DoctorAppointment> GetFutureAppointmentsByPatient(int patientId)
        {
            return DoctorAppointmentService.Instance.GetFutureAppointmentsByPatient(patientId);
        }

        public List<DoctorAppointment> GetAllAppointmentsByPatient(int patientId)
        {
            return DoctorAppointmentService.Instance.GetAllAppointmentsByPatient(patientId);
        }
    }
}
