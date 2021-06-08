using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorConverters;
using Hospital_IS.DoctorViewModel;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Globalization;

//MVVM
namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class TherapyNewViewModel : BindableBase
    {
        #region Fields
        private string name;
        private TherapyDTO therapy;
        private DateTime date;
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

        public TherapyDTO Therapy
        {
            get { return therapy; }
            set
            {
                therapy = value;
                OnPropertyChanged("Therapy");
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                if(value < DateTime.Now)
                {
                    date = DateTime.Now;
                }
                else
                {
                    date = value;
                }
                OnPropertyChanged("Date");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Therapy.Name = value;
                OnPropertyChanged("Name");
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
                MedicineDTO med = MedicineController.Instance.GetByName(Name);
                Therapy newTherapy;
                if (med == null)
                {
                    newTherapy = new Therapy(new Medicine(Name,null,"","",null), Therapy.Pills, Therapy.Takings, DateTime.Now, (DateTime)new DateConverter().ConvertBack(Therapy.TherapyEnd, null, null, CultureInfo.CurrentCulture));
                }
                else
                {
                    newTherapy = new Therapy(MedicineController.Instance.ConvertDTOToMedicine(med), Therapy.Pills, Therapy.Takings, DateTime.Now, (DateTime)new DateConverter().ConvertBack(Therapy.TherapyEnd, null, null, CultureInfo.CurrentCulture));
                }
                ChartController.Instance.AddTherapy(newTherapy, PatientChartViewModel.Instance.Patient);
                PatientChartViewModel.Instance.ChangeCommand.Execute("4");
            }
        }

        private void Execute_CancelCommand(object obj)
        {
            PatientChartViewModel.Instance.ChangeCommand.Execute("4");
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
            this.Date = DateTime.Now;
            this.Therapy.TherapyEnd = DateTime.Now.ToString("dd.MM.yyyy.");
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_Command);
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            ;
        }
        #endregion
    }
}
