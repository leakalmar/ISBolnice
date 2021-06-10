using DTOs;
using Enums;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Hospital_IS.Service
{
    class DoctorAppointmentManagementService
    {
        public List<DoctorAppointmentDTO> AllAppointments { get; set; } = new List<DoctorAppointmentDTO>();
        public List<RoomDTO> AllRooms { get; set; } = new List<RoomDTO>();

        private static DoctorAppointmentManagementService instance = null;
        public static DoctorAppointmentManagementService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorAppointmentManagementService();
                }
                return instance;
            }
        }

        private DoctorAppointmentManagementService()
        {
            LoadAppointments();
            LoadRooms();
        }

        private void LoadAppointments()
        {
            foreach (DoctorAppointment docAppointment in DoctorAppointmentService.Instance.AllAppointments)
            {
                PatientDTO patientDTO = SecretaryUserManagementService.Instance.GetPatientByID(docAppointment.Patient.Id);
                DoctorDTO doctorDTO = SecretaryUserManagementService.Instance.GetDoctorByID(docAppointment.Doctor.Id);
                AllAppointments.Add(new DoctorAppointmentDTO(docAppointment.Reserved, docAppointment.AppointmentCause, docAppointment.AppointmentStart, docAppointment.AppointmentEnd,
                    docAppointment.Type, docAppointment.Room, docAppointment.Id, docAppointment.IsUrgent, patientDTO, doctorDTO, docAppointment.IsFinished));
            }
        }
        public void ReloadAppointments()
        {
            AllAppointments.Clear();
            LoadAppointments();

        }

        public bool VerifyAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            ReloadAppointments();
            Patient patient = PatientService.Instance.GetPatientByID(doctorAppointmentDTO.Patient.Id);
            Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorAppointmentDTO.Doctor.Id);
            return VerifyAppointmentService.Instance.VerifyAppointment(new DoctorAppointment(doctorAppointmentDTO.Reserved, doctorAppointmentDTO.AppointmentCause,
                        doctorAppointmentDTO.AppointmentStart, doctorAppointmentDTO.AppointmentEnd, doctorAppointmentDTO.Type, doctorAppointmentDTO.Room,
                        doctorAppointmentDTO.Id, doctorAppointmentDTO.IsUrgent, patient, doctor, doctorAppointmentDTO.IsFinished));
        }

        private void LoadRooms()
        {
            foreach (Room room in RoomService.Instance.AllRooms)
            {
                List<EquipmentDTO> equipment = LoadRoomEquipment(room);
                AllRooms.Add(new RoomDTO(room.RoomNumber, room.BedNumber, room.Id, room.Type, equipment, room.RoomFloor, room.isUsable));
            }
        }

        private List<EquipmentDTO> LoadRoomEquipment(Room room)
        {
            List<EquipmentDTO> equipment = new List<EquipmentDTO>();
            foreach (Equipment equip in room.Equipment)
                equipment.Add(new EquipmentDTO(equip.EquipType, equip.EquiptId, equip.Name, equip.Quantity, equip.ProducerName));

            return equipment;
        }

        public List<RoomDTO> GetRoomByType(RoomType type)
        {
            List<RoomDTO> allRoomByType = new List<RoomDTO>();

            foreach (RoomDTO roomDTO in AllRooms)
            {
                if (roomDTO.Type == type)
                {
                    allRoomByType.Add(roomDTO);
                }
            }
            return allRoomByType;
        }

        public RoomDTO GetRoomById(int id)
        {
            RoomDTO room = null;
            foreach (RoomDTO roomDTO in AllRooms)
            {
                if (roomDTO.RoomId == id)
                {
                    room = roomDTO;
                }
            }
            return room;
        }

        public DoctorAppointmentDTO FormDoctorAppointmentDTO(DoctorAppointment docAppointment)
        {
            PatientDTO patientDTO = SecretaryUserManagementService.Instance.GetPatientByID(docAppointment.Patient.Id);
            DoctorDTO doctorDTO = SecretaryUserManagementService.Instance.GetDoctorByID(docAppointment.Doctor.Id);
            return new DoctorAppointmentDTO(docAppointment.Reserved, docAppointment.AppointmentCause, docAppointment.AppointmentStart, docAppointment.AppointmentEnd,
                    docAppointment.Type, docAppointment.Room, docAppointment.Id, docAppointment.IsUrgent, patientDTO, doctorDTO, docAppointment.IsFinished);
        }
    }
}
