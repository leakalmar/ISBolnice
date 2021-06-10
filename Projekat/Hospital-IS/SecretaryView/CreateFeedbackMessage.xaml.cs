using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using System;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for CreateFeedbackMessage.xaml
    /// </summary>
    public partial class CreateFeedbackMessage : Window
    {
        public FeedbackMessageDTO Message { get; set; } = new FeedbackMessageDTO();
        public CreateFeedbackMessage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMessage(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            Message.DateSent = DateTime.Now;
            FeedbackMessageController.Instance.AddFeedbackMessage(Message);
            this.Close();
        }
    }
}
