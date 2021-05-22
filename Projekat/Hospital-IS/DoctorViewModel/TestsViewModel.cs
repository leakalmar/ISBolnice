using Hospital_IS.DoctorView;

namespace Hospital_IS.DoctorViewModel
{
    public class TestsViewModel : BindableBase
    {
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
    }
}
