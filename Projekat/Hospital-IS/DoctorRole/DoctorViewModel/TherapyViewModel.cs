using Hospital_IS.DoctorRole.Commands;

namespace Hospital_IS.DoctorViewModel
{
    public class TherapyViewModel : BindableBase
    {
        #region Fields
        private bool started;
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

        #region Commands

        #endregion

        #region Actions

        #endregion

        #region Constructor

        #endregion
    }
}
