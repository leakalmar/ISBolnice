using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorView;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital_IS.DoctorConverters
{
    public class DoctorAppointmentConverter
    {
        private AppointmentRowDTO ConvertExistingAppointmentToDTO(DoctorAppointment appointemnt)
        {
            PatientDTO patientDTO = DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient;
            DoctorDTO doctorDTO = DoctorMainWindow.Instance._ViewModel.Doctor;
            DoctorAppointmentDTO doctorAppointmentDTO = new DoctorAppointmentDTO(appointemnt.Reserved, appointemnt.AppointmentCause, appointemnt.AppointmentStart, appointemnt.AppointmentEnd,
                    appointemnt.Type, appointemnt.Room, appointemnt.Id, appointemnt.IsUrgent, patientDTO, doctorDTO, appointemnt.IsFinished);
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
            ObservableCollection<AppointmentRowDTO> dtoAppointments = new ObservableCollection<AppointmentRowDTO>();
            AppointmentRowDTO dto;
            foreach (DoctorAppointmentDTO d in appointments)
            {
                dto = ConvertNewAppointmentToDTO(d);
                dtoAppointments.Add(dto);
            }
            return dtoAppointments;
        }

        public ObservableCollection<DoctorAppointmentDTO> ConvertAppointmentsToDTO(List<DoctorAppointment> appointments)
        {
            ObservableCollection<DoctorAppointmentDTO> dtoAppointments = new ObservableCollection<DoctorAppointmentDTO>();
            foreach (DoctorAppointment d in appointments)
            {
                dtoAppointments.Add(DoctorAppointmentManagementController.Instance.GetAppointmentById(d.Id));
            }
            return dtoAppointments;
        }
    }
}
