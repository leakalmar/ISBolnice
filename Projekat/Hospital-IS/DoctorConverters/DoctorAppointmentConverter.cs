using DTOs;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital_IS.DoctorConverters
{
    public class DoctorAppointmentConverter
    {
        public AppointmentRowDTO ConvertModelToDTO(DoctorAppointment appointemnt)
        {
            DoctorAppointmentDTO doctorAppointmentDTO = new DoctorAppointmentDTO(appointemnt.Reserved, appointemnt.AppointmentCause, appointemnt.AppointmentStart, appointemnt.AppointmentEnd, appointemnt.Type, appointemnt.Room, appointemnt.Id, appointemnt.NameSurnamePatient, appointemnt.AppTypeText, appointemnt.IsUrgent, appointemnt.Patient, appointemnt.Doctor, appointemnt.IsFinished);
            AppointmentRowDTO appointmentRowDTO = new AppointmentRowDTO(doctorAppointmentDTO, false);

            return appointmentRowDTO;
        }

        public ObservableCollection<AppointmentRowDTO> ConvertCollectionToDTO(List<DoctorAppointment> appointments)
        {
            ObservableCollection<AppointmentRowDTO> vmAppointments = new ObservableCollection<AppointmentRowDTO>();
            AppointmentRowDTO viewModel;
            foreach (DoctorAppointment d in appointments)
            {
                viewModel = ConvertModelToDTO(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }
    }
}
