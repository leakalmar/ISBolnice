using Hospital_IS.DoctorRole.Commands;

namespace Hospital_IS.DoctorViewModel
{
    public class TestsViewModel : BindableBase
    {
        #region Feilds
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
    }
}
