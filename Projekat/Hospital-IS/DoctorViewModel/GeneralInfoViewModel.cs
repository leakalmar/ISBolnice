using Hospital_IS.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;

namespace Hospital_IS.DoctorViewModel
{
    public class GeneralInfoViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private bool started;

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }
        #endregion
    }


}
