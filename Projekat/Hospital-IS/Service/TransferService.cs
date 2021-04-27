using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class TransferService
    {
        private TransferStorage tfs = new TransferStorage();
        private List<Transfer> allTransfer { get; set; }

        private static TransferService instance = null;
        public static TransferService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransferService();
                }
                return instance;
            }
        }

        public TransferService()
        {
           allTransfer = tfs.GetAll();
           
        }

        public Boolean TransferEquipmentStatic(Room sourceRoom, Room destinationRoom,Equipment equip, int quantity, DateTime startDate, DateTime endDate, String description)
        {
            List<Appointment> RoomsAppointment = AppointmentService.Instance.getAllAppByTwoRooms(sourceRoom.RoomId, destinationRoom.RoomId);
            bool checkRoomAppointment = AppointmentService.Instance.CheckAppointment(RoomsAppointment, startDate, endDate);

            if (checkRoomAppointment)
            {
                Transfer transfer = new Transfer(sourceRoom.RoomId, destinationRoom.RoomId, equip, quantity, endDate, false);
                AddTransfer(transfer);
              
                if (sourceRoom.Type != RoomType.StorageRoom)
                {
                    Appointment appointment = new Appointment(startDate, endDate, AppointmetType.EquipTransfer, sourceRoom.RoomId);
                    appointment.Reserved = true;
                    AppointmentService.Instance.AddAppointment(appointment);
                }

                if (destinationRoom.Type != RoomType.StorageRoom)
                {
                    Appointment appointment = new Appointment(startDate, endDate, AppointmetType.EquipTransfer, destinationRoom.RoomId);
                    appointment.Reserved = true;
                    AppointmentService.Instance.AddAppointment(appointment);
                }

            }
            return checkRoomAppointment;
        }

        public void TransferEquipment(Model.Room sourceRoom, Model.Equipment equip, int quantity)
        {
            foreach (Equipment eq in sourceRoom.Equipment)
            {
                if (eq.EquiptId == equip.EquiptId)
                {
                    if (eq.Quantity > quantity)
                    {
                        eq.Quantity = eq.Quantity - quantity;

                    }

                }
            }
            RoomService.Instance.SaveChange();

        }

        public void TransferStaticEquipment(int sourceRoomId, int destinationRoomId, Model.Equipment equip, int quantity)
        {

            tfs.Save(allTransfer);
            Room sourceRoom = RoomService.Instance.getRoomById(sourceRoomId);
            Room destinationRoom = RoomService.Instance.getRoomById(destinationRoomId);

            bool isEnoughEquipment = RoomService.Instance.CheckQuantity(sourceRoom, equip, quantity);


            TransferEquipment(sourceRoom, equip, quantity);

            if(destinationRoom.Equipment == null)
            {
                destinationRoom.Equipment = new List<Equipment>();
            }
            bool exist = false;
            foreach (Equipment eq in destinationRoom.Equipment)
            {
                if (eq.EquiptId == equip.EquiptId)
                {

                    eq.Quantity = eq.Quantity + quantity;
                    exist = true;
                }
            }

            if (!exist)
            {
                Equipment equipment = new Equipment(equip.EquipType, equip.EquiptId, equip.Name, quantity);
                destinationRoom.Equipment.Add(equipment);
            }

            RoomService.Instance.SaveChange();
        }
    
        public void AddTransfer(Transfer transfer)
        {
            allTransfer.Add(transfer);
            tfs.Save(allTransfer);
        }

        public List<Transfer> getAllTransfers()
        {
            return allTransfer;
        }

    }
}
