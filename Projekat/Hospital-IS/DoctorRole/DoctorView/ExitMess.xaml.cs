using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ExitMess : Window
    {
        public ExitMess(string question, string type)
        {
            InitializeComponent();
            lblQuestion.Text = question;

            if (type == "info")
            {
                this.btnOk.Content = "U redu";
                this.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                this.btnOk.Focus();
                this.btnCancle.Visibility = Visibility.Collapsed;
                DispatcherTimer dispatcherTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(5)
                };
                dispatcherTimer.Start();
                dispatcherTimer.Tick += (sender, args) => { dispatcherTimer.Stop(); this.Close(); };
            }
            else if (type == "yesNo")
            {
                this.btnOk.Content = "Da";
                this.btnOk.Focus();
                this.btnCancle.Content = "Ne";
            }
            else if (type == "ok")
            {
                this.btnOk.Content = "U redu";
                this.btnOk.Focus();
                this.btnCancle.Visibility = Visibility.Collapsed;
            }
        }


        public void BtnClickPotvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public void BtnClickOdustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
