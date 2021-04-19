using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for ExitMess.xaml
    /// </summary>
    public partial class ExitMess : Window
    {
        public ExitMess(string question)
        {
            InitializeComponent();
            lblQuestion.Content = question;
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
