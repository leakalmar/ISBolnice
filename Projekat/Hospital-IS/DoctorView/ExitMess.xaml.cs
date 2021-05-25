using System.Windows;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for ExitMess.xaml
    /// </summary>
    public partial class ExitMess : Window
    {
        public ExitMess(string question)
        {
            InitializeComponent();
            lblQuestion.Text = question;
        }

        public void BtnClickPotvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public void BtnClickOdustani(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
