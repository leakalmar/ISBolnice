using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientNoteViewModel : BindableBase
    {
        public EventHandler OnRequestClose;
        public MyICommand AppointmentNote { get; set; }

        public PatientNoteViewModel()
        {
            AppointmentNote = new MyICommand(AppNote);
        }

        private void AppNote()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
