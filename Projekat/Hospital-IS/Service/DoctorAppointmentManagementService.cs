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
            foreach (DoctorAppointment docAppointment in DoctorAppointmentService.Instance.allAppointments)
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

        public void AddAppointment(DoctorAppointmentDTO docAppointmentDTO)
        {
            if (docAppointmentDTO == null)
            {
                return;
            }

            if (AllAppointments == null)
            {
                AllAppointments = new List<DoctorAppointmentDTO>();

            }

            if (!AllAppointments.Contains(docAppointmentDTO))
            {
                Patient patient = PatientService.Instance.GetPatientByID(docAppointmentDTO.Patient.Id);
                Doctor doctor = DoctorService.Instance.GetDoctorByID(docAppointmentDTO.Doctor.Id);
                DoctorAppointmentService.Instance.AddAppointment(new DoctorAppointment(docAppointmentDTO.Reserved, docAppointmentDTO.AppointmentCause, 
                    docAppointmentDTO.AppointmentStart, docAppointmentDTO.AppointmentEnd, docAppointmentDTO.Type, docAppointmentDTO.Room, 
                    docAppointmentDTO.Id, docAppointmentDTO.IsUrgent, patient, doctor, docAppointmentDTO.IsFinished));
                ReloadAppointments();    
            }
        }

        public List<DoctorAppointmentDTO> GetAppointmentsByPatientId(int patientId)
        {
            List<DoctorAppointmentDTO> appointments = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointmentDTO docApp in AllAppointments)
            {
                if (docApp.Patient.Id.Equals(patientId))
                    appointments.Add(docApp);
            }
            return appointments;
        }

        public List<DoctorAppointmentDTO> GetFutureAppointmentsForDoctor(int doctorId)
        {
            List<DoctorAppointmentDTO> appointments = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointmentDTO appointment in AllAppointments)
            {
                if (appointment.AppointmentStart > DateTime.Now)
                    appointments.Add(appointment);
            }

            return appointments;
        }

        public List<DoctorAppointmentDTO> GetPreviousAppointmentsForDoctor(int doctorId)
        {
            List<DoctorAppointmentDTO> appointments = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointmentDTO appointment in AllAppointments)
            {
                if (appointment.AppointmentEnd < DateTime.Now)
                    appointments.Add(appointment);
            }

            return appointments;
        }

        public void RemoveAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            if (doctorAppointmentDTO == null)
            {
                return;
            }

            if (AllAppointments != null)
            {
                foreach (DoctorAppointmentDTO doctorApp in AllAppointments)
                {
                    if (doctorAppointmentDTO.AppointmentStart.Equals(doctorApp.AppointmentStart) && doctorAppointmentDTO.Doctor.Id.Equals(doctorApp.Doctor.Id))
                    {
                        AllAppointments.Remove(doctorApp);
                        Patient patient = PatientService.Instance.GetPatientByID(doctorApp.Patient.Id);
                        Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorApp.Doctor.Id);
                        DoctorAppointmentService.Instance.RemoveAppointment(new DoctorAppointment(doctorApp.Reserved, doctorApp.AppointmentCause,
                            doctorApp.AppointmentStart, doctorApp.AppointmentEnd, doctorApp.Type, doctorApp.Room,
                            doctorApp.Id, doctorApp.IsUrgent, patient, doctor, doctorApp.IsFinished));
                        break;
                    }
                }
            }

        }

        public void UpdateAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            for (int i = 0; i < AllAppointments.Count; i++)
            {
                if (newDoctorAppointmentDTO.Id == AllAppointments[i].Id)
                {
                    AllAppointments.Remove(AllAppointments[i]);
                    AllAppointments.Insert(i, newDoctorAppointmentDTO);

                    Patient patient = PatientService.Instance.GetPatientByID(newDoctorAppointmentDTO.Patient.Id);
                    Doctor doctor = DoctorService.Instance.GetDoctorByID(newDoctorAppointmentDTO.Doctor.Id);
                    DoctorAppointment oldDoctorAppointment = new DoctorAppointment(oldDoctorAppointmentDTO.Reserved, oldDoctorAppointmentDTO.AppointmentCause,
                        oldDoctorAppointmentDTO.AppointmentStart, oldDoctorAppointmentDTO.AppointmentEnd, oldDoctorAppointmentDTO.Type, oldDoctorAppointmentDTO.Room,
                        oldDoctorAppointmentDTO.Id, oldDoctorAppointmentDTO.IsUrgent, patient, doctor, oldDoctorAppointmentDTO.IsFinished);
                    DoctorAppointment newDoctorAppointment = new DoctorAppointment(oldDoctorAppointmentDTO.Reserved, oldDoctorAppointmentDTO.AppointmentCause,
                        oldDoctorAppointmentDTO.AppointmentStart, oldDoctorAppointmentDTO.AppointmentEnd, oldDoctorAppointmentDTO.Type, oldDoctorAppointmentDTO.Room,
                        oldDoctorAppointmentDTO.Id, oldDoctorAppointmentDTO.IsUrgent, patient, doctor, oldDoctorAppointmentDTO.IsFinished);
                    DoctorAppointmentService.Instance.UpdateAppointment(oldDoctorAppointment, newDoctorAppointment);

                    return;
                }
            }
        }

        public DoctorAppointmentDTO GetAppointmentById(int id)
        {
            DoctorAppointmentDTO foundAppointment = null;
            foreach (DoctorAppointmentDTO appointment in AllAppointments)
            {
                if (appointment.Id.Equals(id))
                {
                    foundAppointment = appointment;
                }
            }
            return foundAppointment;
        }

        public bool VerifyAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            Patient patient = PatientService.Instance.GetPatientByID(doctorAppointmentDTO.Patient.Id);
            Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorAppointmentDTO.Doctor.Id);
            return DoctorAppointmentService.Instance.VerifyAppointment(new DoctorAppointment(doctorAppointmentDTO.Reserved, doctorAppointmentDTO.AppointmentCause,
                        doctorAppointmentDTO.AppointmentStart, doctorAppointmentDTO.AppointmentEnd, doctorAppointmentDTO.Type, doctorAppointmentDTO.Room,
                        doctorAppointmentDTO.Id, doctorAppointmentDTO.IsUrgent, patient, doctor, doctorAppointmentDTO.IsFinished));
        }

        private void LoadRooms()
        {
            foreach (Room room in RoomService.Instance.AllRooms)
            {
                List<EquipmentDTO> equipment = LoadRoomEquipment(room);
                AllRooms.Add(new RoomDTO(room.RoomNumber, room.BedNumber, room.RoomId, room.Type, equipment, room.RoomFloor, room.isUsable));
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

        public DoctorAppointmentDTO FormDoctorAppointmentDTO(DoctorAppointment docAppointment)
        {
            PatientDTO patientDTO = SecretaryUserManagementService.Instance.GetPatientByID(docAppointment.Patient.Id);
            DoctorDTO doctorDTO = SecretaryUserManagementService.Instance.GetDoctorByID(docAppointment.Doctor.Id);
            return new DoctorAppointmentDTO(docAppointment.Reserved, docAppointment.AppointmentCause, docAppointment.AppointmentStart, docAppointment.AppointmentEnd,
                    docAppointment.Type, docAppointment.Room, docAppointment.Id, docAppointment.IsUrgent, patientDTO, doctorDTO, docAppointment.IsFinished);
        }

        public List<DoctorAppointmentDTO> GetAppointmentByDoctorId(int doctorId)
        {
            List<DoctorAppointmentDTO> doctorAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointmentDTO appointment in AllAppointments)
            {
                if (appointment.Doctor.Id.Equals(doctorId))
                {
                    doctorAppointmentDTOs.Add(appointment);
                }
            }
            return doctorAppointmentDTOs;
        }
    }
}
