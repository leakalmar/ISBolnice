using Hospital_IS.View.PatientViewModels;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindowView : Window
    {
        private static PatientMainWindowView instance = null;
        public static PatientMainWindowView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientMainWindowView();
                }
                return instance;
            }
        }
        public PatientMainWindowViewModel PatientMainView { get; set; }
        private PatientMainWindowView()
        {
            PatientMainView = new PatientMainWindowViewModel();
            this.DataContext = PatientMainView;
            InitializeComponent();
        }

        private void LogoutPatient(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
