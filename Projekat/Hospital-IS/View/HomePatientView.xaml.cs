using Hospital_IS.View.PatientViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for HomePatient.xaml
    /// </summary>
    public partial class HomePatientView : UserControl
    {

        public HomePatientView()
        {
            InitializeComponent();
            this.DataContext = new HomePatientViewModel();
            //Patient = PatientMainWindowViewModel.Patient;
            //DoctorAppointment = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(Patient.Id));
            /*Medicine medicine = new Medicine("Bromazepam", "1 tableta sadrži 1,5 mg, 3 mg, odnosno 6 mg bromazepama.",
                "U osetljivih bolesnika, naročito kod većih doza, može se javiti blagi umor, pospanost i vrtoglavica," +
                " a povremeno slabost mišića i ataksija. Ova neželjena dejstva mogu se izbeći prilagođavanjem doze.", "Doziranje je individualno." +
                " Prosečna doza za ambulantne pacijente je 1,5-3 mg, do 3 puta na dan." +
                " U težim oblicima bolesti, naročito kod hospitalizovanih bolesnika, daje se 6-12 mg, 2 do 3 puta na dan.");
            Therapy t = new Therapy(medicine, 1, 2, new DateTime(2021, 4, 19), new DateTime(2021, 5, 1));
            Medicine medicine1 = new Medicine("Brufen", "1 film tableta sadrži 400 mg ibuprofena 5 ml sirupa sadrži 100 mg Ibuprofena", "Ibuprofen se dobro podnosi, a neželjena dejstva su veoma retka i nestaju prekidom terapije." +
                " Kod manjeg broja bolesnika mogu se, javiti gastrointestinalne smetnje.", "Ibuprofen se uzima posle jela." +
                " Doziranje treba individualno uskladiti tako da se sa najmanjom mogućom dozom postigne željeni terapijski efekat.");
            Therapy t1 = new Therapy(medicine1, 1, 3, new DateTime(2021, 4, 19), new DateTime(2021, 5, 1));
            t.FirstUsageTime = 11;
            t1.FirstUsageTime = 8;
            Patient.MedicalHistory.AddTherapy(t);
            Patient.MedicalHistory.AddTherapy(t1);*/
        }
    }
}
