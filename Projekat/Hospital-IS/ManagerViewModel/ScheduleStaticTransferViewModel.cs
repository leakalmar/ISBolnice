﻿using Controllers;
using DTOs;
using Microsoft.Windows.Controls;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class ScheduleStaticTransferViewModel:ViewModel
    {
        private DateTime dateStart = DateTime.Now;
        private DateTime dateEnd = DateTime.Now;
        private Equipment selectedEquipment;
        private string note;
        private NavigationService navService;
        private ObservableCollection<Appointment> appointments;
        private RelayCommand transferStaticEquipmentCommand;
        private Room sourceRoom;
        private Room destinationRoom;
        private Equipment equipment;
        public int Quantity { get; set; }



        public Equipment Equipment
        {
            get
            {
                return equipment;
            }
            set
            {
                if (value != equipment)
                {

                    equipment = value;
                    OnPropertyChanged("Equipment");

                }
            }
        }



        public Room DestinationRoom
        {
            get
            {
                return destinationRoom;
            }
            set
            {
                if (value != destinationRoom)
                {

                    destinationRoom = value;
                    OnPropertyChanged("DestinationRoom");

                }
            }
        }

        public Room SourceRoom
        {
            get
            {
                return sourceRoom;
            }
            set
            {
                if (value != sourceRoom)
                {

                    sourceRoom = value;
                    OnPropertyChanged("SourceRoom");

                }
            }
        }



        public RelayCommand TransferStaticEquipmentCommand
        {
            get { return transferStaticEquipmentCommand; }
            set
            {
                transferStaticEquipmentCommand = value;
            }
        }
        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return appointments;
            }
            set
            {
                if (value != appointments)
                {

                    appointments = value;
                    OnPropertyChanged("Appointments");

                }
            }
        }

        public DateTime DateStart
        {
            get { return dateStart; }
            set
            {

                dateStart = value;
               
                OnPropertyChanged("DateStart");
            }
        }

        public DateTime DateEnd
        {
            get { return dateEnd; }
            set
            {
                dateEnd = value;

                OnPropertyChanged("DateEnd");
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


        public void SetAppointmentsForRoom(int roomIdSource, int roomIdDestination)
        {
           
            Appointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAllAppByTwoRooms(roomIdSource, roomIdDestination));
            List<Appointment> appointments = AppointmentController.Instance.GetAllAppByTwoRooms(roomIdSource, roomIdDestination);
           
            this.TransferStaticEquipmentCommand = new RelayCommand(Execute_TransferStaticEquipment, CanExecute_IfEveryhingIsCorecct);

        }

        private bool CanExecute_IfEveryhingIsCorecct(object obj)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)obj;
           
            
            if (DateStart < DateTime.Now || DateEnd < DateTime.Now || DateStart >= DateEnd || dateTimePicker.Text.Length == 0)
            {
                
                return false;
            }

            return true;

        }
        private void Execute_TransferStaticEquipment(object obj)
        {

            StaticTransferAppointmentDTO staticTransfer = new StaticTransferAppointmentDTO(SourceRoom.RoomId, DestinationRoom.RoomId, Equipment.EquiptId, Quantity, DateStart, DateEnd, Note);
            bool isSucces = TransferController.Instance.ScheduleStaticTransfer(staticTransfer);
            Appointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAllAppByTwoRooms(SourceRoom.RoomId, DestinationRoom.RoomId));

            if (!isSucces)
            {
                System.Windows.MessageBox.Show("Niste uspjeli da zakazete termin");
            }
            else
            {
                System.Windows.MessageBox.Show("Uspjesno zakazivanje termina");
              
            }

        }

    }
}
