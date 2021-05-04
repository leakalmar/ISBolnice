using DTOs;
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

        public Boolean ScheduleStaticTransfer(StaticTransferAppointmentDTO staticTransfer)
        {
            List<Appointment> RoomsAppointment = AppointmentService.Instance.GetAllAppByTwoRooms(staticTransfer.SourceRoom.RoomId, staticTransfer.DestinationRoom.RoomId);
            bool checkRoomAppointment = AppointmentService.Instance.CheckAppointment(RoomsAppointment, staticTransfer.StartDate, staticTransfer.EndDate);

            if (checkRoomAppointment)
            {
                CreateAppointmentForEquipmentTransfer(staticTransfer);

            }
            return checkRoomAppointment;
        }

        private void CreateAppointmentForEquipmentTransfer(StaticTransferAppointmentDTO staticTransfer)
        {
            Transfer transfer = new Transfer(staticTransfer.SourceRoom.RoomId, staticTransfer.DestinationRoom.RoomId, staticTransfer.Equip, staticTransfer.Quantity, staticTransfer.EndDate, false);
            AddTransfer(transfer);

            if (staticTransfer.SourceRoom.Type != RoomType.StorageRoom)
            {
                CreateAppointment(staticTransfer,staticTransfer.SourceRoom.RoomId);
            }

            if (staticTransfer.DestinationRoom.Type != RoomType.StorageRoom)
            {
                CreateAppointment(staticTransfer,staticTransfer.DestinationRoom.RoomId);
            }
        }

        private static void CreateAppointment(StaticTransferAppointmentDTO staticTransfer,int roomId)
        {
            Appointment appointment = new Appointment(staticTransfer.StartDate, staticTransfer.EndDate, AppointmentType.EquipTransfer, roomId);
            appointment.Reserved = true;
            AppointmentService.Instance.AddAppointment(appointment);
        }



        public void ReduceEquipmentQuantity(Room sourceRoom, Equipment equip, int quantity)
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

        public void ExecuteStaticTransfer(Transfer transfer)
        {

            tfs.Save(allTransfer);
            Room sourceRoom = RoomService.Instance.GetRoomById(transfer.SourceRoomId);
            Room destinationRoom = RoomService.Instance.GetRoomById(transfer.DestinationRoomId);

            bool isEnoughEquipment = RoomService.Instance.CheckQuantity(sourceRoom, transfer.Equip, transfer.Quantity);

            if (isEnoughEquipment)
            {
                ReduceEquipmentQuantity(sourceRoom, transfer.Equip, transfer.Quantity);

                IncreaseEquipmentQuantity(transfer.Equip, transfer.Quantity, destinationRoom);
            }

            RoomService.Instance.SaveChange();
        }

        private static void IncreaseEquipmentQuantity(Equipment equip, int quantity, Room destinationRoom)
        {
            if (destinationRoom.Equipment == null)
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
        }



        public void AddTransfer(Transfer transfer)
        {
            allTransfer.Add(transfer);
            tfs.Save(allTransfer);
        }

        public List<Transfer> GetAllTransfers()
        {
            return allTransfer;
        }

    }
}
