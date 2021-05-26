using DTOs;
using Hospital_IS.Controllers;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital_IS.DoctorConverters
{
    public class DoctorAppointmentConverter
    {
        private AppointmentRowDTO ConvertExistingAppointmentToDTO(DoctorAppointment appointemnt)
        {
            DoctorAppointmentDTO doctorAppointmentDTO = DoctorAppointmentManagementController.Instance.GetAppointmentById(appointemnt.Id);
            AppointmentRowDTO appointmentRowDTO = new AppointmentRowDTO(doctorAppointmentDTO);

            return appointmentRowDTO;
        }

        public ObservableCollection<AppointmentRowDTO> ConvertExistingAppointmentsToDTO(List<DoctorAppointment> appointments)
        {
            ObservableCollection<AppointmentRowDTO> vmAppointments = new ObservableCollection<AppointmentRowDTO>();
            AppointmentRowDTO viewModel;
            foreach (DoctorAppointment d in appointments)
            {
                viewModel = ConvertExistingAppointmentToDTO(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }

        public AppointmentRowDTO ConvertNewAppointmentToDTO(DoctorAppointmentDTO appointemnt)
        {
            AppointmentRowDTO appointmentRowDTO = new AppointmentRowDTO(appointemnt);

            return appointmentRowDTO;
        }

        public ObservableCollection<AppointmentRowDTO> ConvertNewAppointmentsToDTO(List<DoctorAppointmentDTO> appointments)
        {
            ObservableCollection<AppointmentRowDTO> vmAppointments = new ObservableCollection<AppointmentRowDTO>();
            AppointmentRowDTO viewModel;
            foreach (DoctorAppointmentDTO d in appointments)
            {
                viewModel = ConvertNewAppointmentToDTO(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }
    }
}
