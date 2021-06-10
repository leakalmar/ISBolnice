﻿using DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using Service;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.Service
{
    public class AdvancedRenovationMergeService : IAdvancedRenovationService
    {
       
        public void ExecuteAdvancedRoomRenovation(AdvancedRenovation advancedRenovation)
        {
            RoomService.Instance.RemoveRoom(advancedRenovation.RoomFirst);
            RoomService.Instance.RemoveRoom(advancedRenovation.RoomSecond);
            Room room = new Room(advancedRenovation.RenovationResultRoom.RoomNumber, advancedRenovation.RenovationResultRoom.RoomFloor, advancedRenovation.RenovationResultRoom.SurfaceArea,
                0, advancedRenovation.RenovationResultRoom.Type);
            AddEquipmentFromBothRoom(advancedRenovation, room);

            RoomService.Instance.AddRoom(room);
        }

        private static void AddEquipmentFromBothRoom(AdvancedRenovation advancedRenovation, Room room)
        {
            List<Equipment> equipment = new List<Equipment>();
            equipment.AddRange(advancedRenovation.RoomFirst.Equipment);
            equipment.AddRange(advancedRenovation.RoomFirst.Equipment);
            room.Equipment = equipment;
        }

        public void MakeAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            AdvancedRenovationService.Instance.AddNewAdvancedRenovation(advancedRenovation);

            CancelDocAppFromBothRoom(advancedRenovation);
            CancelClassicAppFromBothRoom(advancedRenovation);
        }

        private void CancelClassicAppFromBothRoom(AdvancedRenovation advancedRenovation)
        {
            CancelClassicAppAfterDateEnd(advancedRenovation.RoomFirst.RoomId, advancedRenovation.RenovationEnd);
            CancelClassicAppAfterDateEnd(advancedRenovation.RoomSecond.RoomId, advancedRenovation.RenovationEnd);

        }

        private void CancelClassicAppAfterDateEnd(int roomId, DateTime renovationEnd)
        {
            List<Appointment> appointments = new List<Appointment>();
            AppointmentService.Instance.GetAllClassicAppointments(roomId, appointments);
            for (int i = 0; i < appointments.Count; i++)
            {               
                if (DateTime.Compare(renovationEnd, appointments[i].AppointmentStart) <= 0)
                {
                   
                    AppointmentService.Instance.RemoveAppointment(appointments[i]);
                }
            }

        }

        private void CancelDocAppFromBothRoom(AdvancedRenovation advancedRenovation)
        {
            CanacelDocAppAfterDateEnd(advancedRenovation.RoomFirst.RoomId, advancedRenovation.RenovationEnd);
            CanacelDocAppAfterDateEnd(advancedRenovation.RoomSecond.RoomId, advancedRenovation.RenovationEnd);
        }

        public void CanacelDocAppAfterDateEnd(int roomId, DateTime dateEnd)
        {
            List<DoctorAppointment> doctorAppointments = DoctorAppointmentService.Instance.GetAllByRoom(roomId);

            for (int i = 0; i < doctorAppointments.Count; i++)
            {
                if (DateTime.Compare(dateEnd, doctorAppointments[i].AppointmentStart) <= 0)
                {
                    NotificationService.Instance.SendAppointmentCancelationNotification(doctorAppointments[i]);
                    DoctorAppointmentService.Instance.RemoveAppointment(doctorAppointments[i]);
                }
            }

        }    

       
    }
}