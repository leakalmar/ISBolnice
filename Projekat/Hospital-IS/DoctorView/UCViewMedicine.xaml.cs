using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    public partial class UCViewMedicine : UserControl
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _note;

        public String Note
        {
            get { return _note; }
            set
            {
                if (value != _note)
                {
                    _note = value;
                    if(value != "")
                    {
                        send.IsEnabled = true;
                    }
                    else
                    {
                        send.IsEnabled = false;
                    }

                }
                else
                {
                    send.IsEnabled = false;
                }
                
                OnPropertyChanged("Note");
            }
        }
        public MedicineNotification ReviewdNotification { get; set; }
        public UCViewMedicine(MedicineNotification notification)
        {
            InitializeComponent();
            text.DataContext = this;
            ReviewdNotification = notification;
            medInfo.DataContext = notification.Medicine;
            composition.ItemsSource = notification.Medicine.Composition;
            change.ItemsSource = notification.Medicine.ReplaceMedicine;
        }

        private void disapprove_Click(object sender, RoutedEventArgs e)
        {
            lblNotes.Visibility = Visibility.Visible;
            notes.Visibility = Visibility.Visible;
            borderDisapprove.Visibility = Visibility.Collapsed;
            borderApprove.Visibility = Visibility.Collapsed;
            column.Height = new GridLength(txtNotes.Height + 30);
            this.UpdateLayout();
            text.Focus();
        }

        private void approve_Click(object sender, RoutedEventArgs e)
        {
            MedicineNotificationController.Instance.ApproveMedicine(ReviewdNotification);

            //DoctorHomePage.Instance.Home.Children.Remove(this);
          //  DoctorMainWindow.Instance.ApproveMedicine.Visibility = Visibility.Visible;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtnote = sender as TextBox;
            txtnote.Text = "";
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            MedicineNotificationController.Instance.DisapproveMedicine(ReviewdNotification, text.Text);

           // DoctorHomePage.Instance.Home.Children.Remove(this);
          //  DoctorMainWindow.Instance.ApproveMedicine.Visibility = Visibility.Visible;
        }
    }
}
