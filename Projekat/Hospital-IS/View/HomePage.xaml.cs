﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;

namespace Hospital_IS.View
{
    public partial class HomePage : Window
    {

        
        private static readonly Lazy<HomePage>
        lazy = new Lazy<HomePage> (() => new HomePage()); 
        private Doctor Doctor { get; set; }

        public static HomePage Instance { get { return lazy.Value; } }

        private HomePage()
        {
            InitializeComponent();
            List<WorkDay> dani = new List<WorkDay>
            {
                new WorkDay("Pon", DateTime.Now, DateTime.Now)
            };
            Specialty spec = new Specialty("Dermatolog");
            Doctor doc = new Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani, spec, new Room(RoomType.ConsultingRoom, true, true, 1, 1));
            ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Patient p1 = new Model.Patient(001, "Simona", "Vukmanov Simokov", DateTime.Now.Date, "Petra Drapsina 8", "simona@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            Room r = new Room(RoomType.ConsultingRoom, false, true, 2, 25);

            DoctorAppointment a1 = new DoctorAppointment(new DateTime(2020,05,05), AppointmetType.CheckUp, true, new Room(RoomType.ConsultingRoom, true, true, 1, 1), doc, p1);
            DoctorAppointment a2 = new DoctorAppointment(new DateTime(2021, 07, 05), AppointmetType.Operation, true, new Room(RoomType.ConsultingRoom, true, true, 1, 1), doc, p1);
            DoctorAppointment a3 = new DoctorAppointment(DateTime.Now.AddHours(1), AppointmetType.CheckUp, true, new Room(RoomType.ConsultingRoom, true, true, 1, 1), doc, p1);
            doc.AddDoctorAppointment(a1);
            doc.AddDoctorAppointment(a2);
            doc.AddDoctorAppointment(a3);
            this.Doctor = doc;

            ObservableCollection<DoctorAppointment> collection = new ObservableCollection<Model.DoctorAppointment>();
            foreach (DoctorAppointment ap in doc.DoctorAppointment)
            {

                DateTime temp = ap.DateAndTime;
                if (temp.AddMinutes(60) > DateTime.Now)
                {
                    collection.Add(ap);
                }
            }

            docotrAppointments.DataContext = collection;
        }
    }
}
