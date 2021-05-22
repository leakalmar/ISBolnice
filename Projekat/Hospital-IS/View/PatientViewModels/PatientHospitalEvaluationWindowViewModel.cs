using Hospital_IS.Controllers;
using Hospital_IS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientHospitalEvaluationWindowViewModel : BindableBase
    {
        private int grade;
        private string comment;

        public MyICommand EvaluateHospital { get; set; }
        public EventHandler OnRequestClose;

        public PatientHospitalEvaluationWindowViewModel()
        {
            EvaluateHospital = new MyICommand(EvaluateHosp);
            Grade = 0;
            Comment = "";
        }

        public int Grade
        {
            get { return grade; }
            set
            {
                if (grade != value)
                {
                    grade = value;
                    OnPropertyChanged("Grade");
                }
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private void EvaluateHosp()
        {
            PatientHospitalEvaluationController.Instance.AddAppointmentEvaluation(Grade + 1, Comment, DateTime.Today, PatientMainWindowViewModel.Patient.Id);
            OnRequestClose(this, new EventArgs());
        }
    }
}
