using Hospital_IS.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Service
{
    class SecretaryAppointmentManagementService
    {
        public List<DoctorAppointmentDTO> AllAppointments { get; set; } = new List<DoctorAppointmentDTO>();

        private static SecretaryAppointmentManagementService instance = null;
        public static SecretaryAppointmentManagementService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryAppointmentManagementService();
                }
                return instance;
            }
        }

        private SecretaryAppointmentManagementService()
        {

        }
    }
}
