using Hospital_IS.DoctorView;
using Model;

namespace Hospital_IS.DoctorViewModel
{
    public class GeneralInfoViewModel : BindableBase
    {
        #region Feilds
        private Patient patient;
        private bool started;

        public Patient Patient
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
