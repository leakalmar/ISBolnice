using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital_IS.Converters
{
    public class DoctorAppointmentConverter
    {
        public DoctorAppointmentViewModel ConvertModelToViewModel(DoctorAppointment appointemnt)
        {
            DoctorAppointmentViewModel viewModel = new DoctorAppointmentViewModel();

            viewModel.DoctorAppointment = appointemnt;

            return viewModel;
        }

        public ObservableCollection<DoctorAppointmentViewModel> ConvertCollectionToViewModel(List<DoctorAppointment> appointments)
        {
            ObservableCollection<DoctorAppointmentViewModel> vmAppointments = new ObservableCollection<DoctorAppointmentViewModel>();
            DoctorAppointmentViewModel viewModel;
            foreach (DoctorAppointment d in appointments)
            {
                viewModel = ConvertModelToViewModel(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }
    }
}
