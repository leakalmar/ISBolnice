using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class FeedbackViewModel : BindableBase
    {
        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }
        private void Execute_SaveCommand(object obj)
        {
            FeedbackMessageController.Instance.AddFeedbackMessage(new DTOs.FeedbackMessageDTO(Text, DateTime.Now));
            DoctorMainWindowModel.Instance.NavigateToSettingsCommand.Execute(null);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        public FeedbackViewModel()
        {
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_Command);
        }
    }
}
