using Controllers;
using Model;
using System;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientAppointmentEvaluationWindowViewModel : BindableBase
    {
        private int grade;
        private string comment;
        public PatientAppointmentEvaluation AppointmentEvaluation { get; set; }

        public MyICommand EvaluateAppointment { get; set; }
        public MyICommand CloseEvaluate { get; set; }
        public EventHandler OnRequestClose;
        private readonly MyWindowFactory windowFactory;

        public PatientAppointmentEvaluationWindowViewModel(int doctorAppointmentId)
        {
            AppointmentEvaluation = new PatientAppointmentEvaluation(doctorAppointmentId);
            EvaluateAppointment = new MyICommand(EvaluateApp);
            CloseEvaluate = new MyICommand(Close);
            windowFactory = new WindowProductionFactory();
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
                if(comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private void EvaluateApp()
        {
            AppointmentEvaluation.Grade = Grade + 1;
            AppointmentEvaluation.Comment = Comment;
            PatientAppointmentEvaluationController.Instance.AddAppointmentEvaluation(AppointmentEvaluation);
            if (PatientAppointmentEvaluationController.Instance.ShowHospitalEvaluation(PatientMainWindowViewModel.Patient.Id))
            {
                //Napraviti prozor za pitanje da li zeli da oceni bolnicu,ako zeli napravi prozor sa anketom/ocenjivanjem bolnice
                windowFactory.CreateYesNo("Da li želite da ocenite bolnicu?");
                OnRequestClose(this, new EventArgs());
            }
            OnRequestClose(this, new EventArgs());
        }

        private void Close()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
