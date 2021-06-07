using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorConverters;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class TherapyNewViewModel : BindableBase
    {
        #region Fields
        private List<MedicineDTO> medicines;
        private MedicineDTO selectedMedicine;
        private TherapyDTO therapy; 
        private bool focused;

        public bool Focused
        {
            get { return focused; }
            set
            {
                focused = value;
                OnPropertyChanged("Focused");
            }
        }

        public List<MedicineDTO> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
                OnPropertyChanged("Medicines");
            }
        }
        public TherapyDTO Therapy
        {
            get { return therapy; }
            set
            {
                therapy = value;
                OnPropertyChanged("Therapy");
            }
        }
        #endregion

        #region Commands
        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
                OnPropertyChanged("SaveCommand");
            }
        }

        public RelayCommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                cancelCommand = value;
                OnPropertyChanged("CancelCommand");
            }
        }
        #endregion

        #region Actions
        private void Execute_SaveCommand(object obj)
        {
            Therapy.Validate();
            if (Therapy.IsValid)
            {
                Therapy newTherapy = new Therapy(MedicineController.Instance.ConvertDTOToMedicine(Therapy.SelectedMedicine), Therapy.Pills, Therapy.Takings, DateTime.Now, (DateTime)new DateConverter().ConvertBack(Therapy.TherapyEnd, null, null, CultureInfo.CurrentCulture));
                ChartController.Instance.AddTherapy(newTherapy, DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient);
                DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("4");
            }
        }

        private void Execute_CancelCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("4");
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public TherapyNewViewModel()
        {
            this.Therapy = new TherapyDTO();
            this.Therapy.Takings = 1;
            this.Therapy.Pills = 1;
            this.Focused = true;
            this.Therapy.TherapyEnd = DateTime.Now.ToString("dd.MM.yyyy.");
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_Command);
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            this.Medicines = MedicineController.Instance.ConvertMedicineToDTO(MedicineController.Instance.GetAll());
            ;
        }
        #endregion
    }
}
