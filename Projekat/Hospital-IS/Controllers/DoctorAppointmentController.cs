using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

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
        public List<DoctorAppointment> GetAllByDoctor(int doctorId)
        {
            return DoctorAppointmentService.Instance.GetAllByDoctor(doctorId);
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

        public List<DoctorAppointment> SuggestAppointmentsToPatient(String timeSlot,Doctor doctor,Patient patient, DateTime date, Boolean priority)
        {
            return DoctorAppointmentService.Instance.SuggestAppointmentsToPatient(timeSlot, doctor, patient, date, priority);
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(DateTime date, int idRoom, AppointmetType type, String duration)
        {
            DoctorAppointmentService.Instance.SuggestAppointmetsToDoctor(date, idRoom, type, duration);
            throw new NotImplementedException();
        }


    }
}
