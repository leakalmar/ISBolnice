using Hospital_IS.DoctorView;
using Hospital_IS.Storages;
using Service;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Model
{
    public class Hospital
    {
        public String Name { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Address { get; set; }
        public List<Patient> allPatients { get; set; }
        public List<Equipment> allEquipment { get; set; }

        public List<Room> allRooms { get; set; }
        public List<User> allEmployees { get; set; }

        public RoomStorage roomStorage = new RoomStorage();

        private static Hospital instance = null;

        private Hospital()
        {

        }

        public static Hospital Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Hospital();
                    FSDoctor dfs = new FSDoctor();
                    PatientFileStorage pfs = new PatientFileStorage();
                    instance.Doctors = dfs.GetAll();
                    instance.allPatients = pfs.GetAll();
                }
                return instance;
            }
        }



        public Hospital(string name, string city, string country, string address)
        {
            Name = name;
            City = city;
            Country = country;
            Address = address;
        }

        public List<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GellAllAppointmets()
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetAllEquipments()
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAllRooms()
        {
            return roomStorage.GetAll();
        }

        public List<User> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllAppointmentsByDoctor(Doctor doctor)
        {
            List<Appointment> ret = new List<Appointment>();
            foreach (DoctorAppointment dapp in allAppointments)
            {
                if (dapp.Doctor.Id.Equals(doctor.Id))
                {
                    ret.Add(dapp);
                }
            }
            return ret;
        }

        public List<Appointment> GetAllAppByRoom(Room room)
        {
            List<Appointment> ret = new List<Appointment>();
            foreach (Appointment app in allAppointments)
            {
                if (app.Room.Equals(room.RoomId))
                {
                    ret.Add(app);
                }
            }
            return ret;
        }

        public List<DoctorAppointment> allAppointments { get; set; }
        public List<Appointment> AllClassicAppointments { get; set; }

        private ClassicAppointmentStorage classicAppointment = new ClassicAppointmentStorage();

        private AppointmentFileStorage appointmentStorage = new AppointmentFileStorage();

     


        public List<DoctorAppointment> getDocAppByRoom(int roomID)
        {
            return appointmentStorage.GetAllByRoom(roomID);
        }

      

        public void AddAppointment(DoctorAppointment docApp)
        {
            if (docApp == null)
            {
                return;
            }

            if (allAppointments == null)
            {
                allAppointments = new List<DoctorAppointment>();

            }

            if (!allAppointments.Contains(docApp))
            {
                allAppointments.Add(docApp);

            }
        }

        public void RemoveAppointment(DoctorAppointment docApp)
        {
            if (docApp == null)
            {
                return;
            }

            if (allAppointments != null)
            {
                foreach (DoctorAppointment doctorAppointment in allAppointments)
                {
                    if (docApp.AppointmentStart.Equals(doctorAppointment.AppointmentStart))
                    {
                        allAppointments.Remove(doctorAppointment);
                        break;
                    }
                }
            }
        }

       
 



       


        public List<Doctor> Doctors { get; set; }

        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.Doctors == null)
                this.Doctors = new List<Doctor>();
            if (!this.Doctors.Contains(newDoctor))
            {
                this.Doctors.Add(newDoctor);
                newDoctor.Hospital = this;
            }
        }

        public void RemoveDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.Doctors != null)
                if (this.Doctors.Contains(oldDoctor))
                {
                    this.Doctors.Remove(oldDoctor);
                    oldDoctor.Hospital = null;
                }
        }

        public void RemoveAllDoctors()
        {
            if (Doctors != null)
            {
                List<Doctor> tmpDoctor = new List<Doctor>();
                foreach (Doctor oldDoctor in Doctors)
                    tmpDoctor.Add(oldDoctor);
                Doctors.Clear();
                foreach (Doctor oldDoctor in tmpDoctor)
                    oldDoctor.Hospital = null;
                tmpDoctor.Clear();
            }
        }

        public List<User> User { get; set; }

        public void AddUser(User newUser)
        {
            if (newUser == null)
                return;
            if (this.User == null)
                this.User = new List<User>();
            if (!this.User.Contains(newUser))
            {
                this.User.Add(newUser);
                newUser.Hospital = this;
            }
        }

        public void RemoveUser(User oldUser)
        {
            if (oldUser == null)
                return;
            if (this.User != null)
                if (this.User.Contains(oldUser))
                {
                    this.User.Remove(oldUser);
                    oldUser.Hospital = null;
                }
        }

        public void RemoveAllUser()
        {
            if (User != null)
            {
                List<User> tmpUser = new List<User>();
                foreach (User oldUser in User)
                    tmpUser.Add(oldUser);
                User.Clear();
                foreach (User oldUser in tmpUser)
                    oldUser.Hospital = null;
                tmpUser.Clear();
            }
        }





     
        public List<DoctorAppointment> CheckDoctorAppointments(List<DoctorAppointment> newAppointments, int roomId, SelectedDatesCollection dates)
        {
            List<DoctorAppointment> ret = new List<DoctorAppointment>();
            foreach (DateTime d in dates)
            {
                foreach (DoctorAppointment ap in newAppointments)
                {
                    if (ap.AppointmentStart.Date.Equals(d.Date) && ap.Room.Equals(roomId))
                    {
                        if (Hospital.Instance.CheckAppointment(Hospital.Instance.GetAllAppointmentsByDoctor(DoctorHomePage.Instance.Doctor), ap.AppointmentStart, ap.AppointmentEnd) && 
                            Hospital.Instance.CheckAppointment(AppointmentService.Instance.getAppByRoom(roomId), ap.AppointmentStart,ap.AppointmentEnd))
                        {
                            ret.Add(ap);
                        }
                    }
                }
            }
            return ret;
        }



        public bool CheckAppointment(List<Appointment> roomAppointment, DateTime start, DateTime end)
        {
            bool isFree = true;

            foreach (Appointment appointment in roomAppointment)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || (start <= appointment.AppointmentStart && end >= appointment.AppointmentEnd))
                {

                    isFree = false;
                    break;
                }

            }
            return isFree;
        }

        public bool IsBetweenDates(DateTime end, DateTime start, Appointment appointment)
        {
            return (start > appointment.AppointmentStart && start < appointment.AppointmentEnd) || (end > appointment.AppointmentStart && end < appointment.AppointmentEnd);
        }


        public bool CheckQuantity(Room sourceRoom, Equipment equip, int quantity)
        {
            foreach (Equipment eq in sourceRoom.Equipment)
            {
                if (eq.EquiptId == equip.EquiptId)
                {
                    if (equip.Quantity - quantity >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

       

       

       
    }
}
