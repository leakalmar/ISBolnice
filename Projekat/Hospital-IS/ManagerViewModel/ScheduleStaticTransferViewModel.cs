using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class ScheduleStaticTransferViewModel:ViewModel
    {
        private string dateStart;
        private string dateEnd;
        private Equipment selectedEquipment;
        private string note;
        private NavigationService navService;

        public String DateStart
        {
            get { return dateStart; }
            set
            {
                dateStart = value;
               
                OnPropertyChanged("DateStart");
            }
        }

        public String DateEnd
        {
            get { return dateEnd; }
            set
            {
                dateEnd = value;

                OnPropertyChanged("DateStart");
            }
        }

        public String Note
        {
            get { return note; }
            set
            {
                note = value;

                OnPropertyChanged("Note");
            }
        }

        public Equipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                selectedEquipment = value;

                OnPropertyChanged("Note");
            }
        }

        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }

        private static ScheduleStaticTransferViewModel instance = null;
        public static ScheduleStaticTransferViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScheduleStaticTransferViewModel();
                }
                return instance;
            }
        }
        private ScheduleStaticTransferViewModel()
        {

        }



    }
}
