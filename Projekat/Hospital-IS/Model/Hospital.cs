using Hospital_IS.Storages;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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
            throw new NotImplementedException();
        }

        public List<User> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public List<DoctorAppointment> GetAllAppointmentsByDoctor(Doctor doctor)
        {
            List<DoctorAppointment> ret = new List<DoctorAppointment>();
            foreach(DoctorAppointment dapp in allAppointments)
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

        public ObservableCollection<DoctorAppointment> allAppointments { get; set; }
        public ObservableCollection<Appointment> AllClassicAppointments { get; set; }

        private ClassicAppointmentStorage classicAppointment = new ClassicAppointmentStorage();


        private void getAllClassicAppointment()
        {

            AllClassicAppointments = classicAppointment.GetAll();
        }




        public void AddClassicAppointment(Appointment appointemnt)
        {
            getAllClassicAppointment();
            if (appointemnt == null)
            {
                return;
            }

            if (AllClassicAppointments == null)
            {
                AllClassicAppointments = new ObservableCollection<Appointment>();

            }

            if (!AllClassicAppointments.Contains(appointemnt))
            {
                AllClassicAppointments.Add(appointemnt);

                classicAppointment.SaveAppointment(AllClassicAppointments);
            }
        }

        public ObservableCollection<Appointment> getAppByRoom(int roomID)
        {
            return classicAppointment.GetAllByRoomId(roomID);
        }

        public ObservableCollection<Appointment> getAllAppByTwoRoom(int roomIdSource, int roomIdDestination)
        {
            ObservableCollection<Appointment> allApointemnts = new ObservableCollection<Appointment>();



            foreach(Appointment ap in getAppByRoom(roomIdSource))
            {
                allApointemnts.Add(ap);
            }

            foreach (Appointment ap in getAppByRoom(roomIdDestination))
            {
                allApointemnts.Add(ap);
            }


            return allApointemnts;
        }


        public void AddAppointment(DoctorAppointment docApp)
        {
            if (docApp == null)
            {
                return;
            }

            if (allAppointments == null)
            {
                allAppointments = new ObservableCollection<DoctorAppointment>();

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
                    if (docApp.DateAndTime.Equals(doctorAppointment.DateAndTime))
                    {
                        allAppointments.Remove(doctorAppointment);
                        break;
                    }
                }
            }
        }

        public static ObservableCollection<Room> Room { get; set; }



        public void AddRoom(Room newRoom)
        {
            if (newRoom == null)
            {
                return;
            }

            if (Room == null)
            {
                Room = new ObservableCollection<Room>();

            }

            if (!Room.Contains(newRoom))
            {
                Room.Add(newRoom);

            }
        }

        public void RemoveRoom(Room oldRoom)
        {
            foreach (Room r in Room)
            {
                if (r.RoomId == oldRoom.RoomId)
                {

                    Room.Remove(r);

                    break;
                }
            }
        }

        public void RemoveAllRoom()
        {
            if (Room != null)
                Room.Clear();
        }
        public void UpdateRoom(Room oldRoom)
        {

            foreach (Room r in Room)
            {
                if(r.RoomId == oldRoom.RoomId)
                {
                    int index = Room.IndexOf(r);
                    Room.Remove(r);
                    Room.Insert(index, oldRoom);
                    break;
                }
            }


        }


        public ObservableCollection<Doctor> Doctors { get; set; }

        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.Doctors == null)
                this.Doctors = new ObservableCollection<Doctor>();
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
                ObservableCollection<Doctor> tmpDoctor = new ObservableCollection<Doctor>();
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





        public bool TransferEquipmentStatic(Room sourceRoom, Room destinationRoom, Equipment equip, int quantity,DateTime startDate, DateTime endDate, String Description)
        {
            bool checkQuantity =  CheckQuantity(sourceRoom, equip, quantity);
            ObservableCollection<Appointment> sourceRoomAppointment = getAppByRoom(sourceRoom.RoomId);
            bool checkSourceRoomAppointment = checkAppointment(sourceRoomAppointment, startDate, endDate);
            ObservableCollection<Appointment> destinationRoomAppointment = getAppByRoom(destinationRoom.RoomId);

            bool checkDestionationRoomAppointment = checkAppointment(destinationRoomAppointment, startDate, endDate);

           if(checkSourceRoomAppointment && checkDestionationRoomAppointment)
            {

                MessageBox.Show("Uspjesno zakazivanje");
            }



            return checkQuantity;
        }

        private bool checkAppointment(ObservableCollection<Appointment> RoomAppointment,DateTime start, DateTime end)
        {
            bool isFree = true;

            foreach(Appointment appointment in RoomAppointment)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || start < appointment.AppointmentStart && end > appointment.AppointmentEnd)
                {

                    isFree = false;
                    break;
                }

            }
            return isFree;
        }

        private static bool IsBetweenDates(DateTime start, DateTime end, Appointment appointment)
        {

            return (start >= appointment.AppointmentStart && start <= appointment.AppointmentEnd) || (end >= appointment.AppointmentStart && end <= appointment.AppointmentEnd);
        }

        private static bool CheckQuantity(Room sourceRoom, Equipment equip, int quantity)
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

        public bool TransferEquipment(Room sourceRoom, Equipment equip, int quantity)
        {
            foreach (Equipment eq in sourceRoom.Equipment)
            {
                if (eq.EquiptId == equip.EquiptId)
                {
                    if (eq.Quantity > quantity)
                    {
                        eq.Quantity = eq.Quantity - quantity;
                        return true;
                    }

                }
            }
            return false;
        }
    }
}
