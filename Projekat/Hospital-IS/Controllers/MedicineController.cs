using DoctorView;
using DTOs;
using Hospital_IS.DoctorView;
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controllers
{
    public class MedicineController
    {
        private static MedicineController instance = null;
        public static MedicineController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineController();
                }
                return instance;
            }
        }

        private MedicineController()
        {
        }

        public List<Medicine> GetAll()
        {
            return MedicineService.Instance.AllMedicines;
        }

        public Medicine GetByName(string medicineName)
        {
            return MedicineService.Instance.GetByName(medicineName);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            MedicineService.Instance.UpdateMedicine(medicine);
        }

        public void UpdateMedicineWithName(string oldName, string name, List<MedicineComponent> composition, string sideEffects, string usage, List<ReplaceMedicineName> replaceMedicine)
        {
            Medicine medicine = new Medicine(name, composition, sideEffects, usage, replaceMedicine);
            MedicineService.Instance.UpdateMedicineWithName(oldName, medicine);

        }

        public void AddNewMedicine(Medicine medicine)
        {
            MedicineService.Instance.AddNewMedicine(medicine);
        }


        public void DeleteMedicine(Medicine medicine)
        {
            MedicineService.Instance.DeleteMedicine(medicine);
        }

        public Medicine FindMedicineByName(String name)
        {
            return MedicineService.Instance.FindMedicineByName(name);

        }

        public List<MedicineDTO> GenerateListOfMedicines(Patient patient, List<Prescription> prescriptions)
        {
            List<MedicineDTO> medicineList = new List<MedicineDTO>();

            foreach (Medicine medicine in MedicineService.Instance.AllMedicines)
            {
                if (PatientService.Instance.CheckIfAllergicToMedicine(patient.Alergies, medicine.Name))
                {
                    medicineList.Add(new MedicineDTO(medicine.Composition,medicine.SideEffects,medicine.Usage,medicine.ReplaceMedicine, medicine.Name, false, true));
                }
                else
                {
                    if (CheckIfInPrescriptions(medicine, prescriptions))
                    {
                        medicineList.Add(new MedicineDTO(medicine.Composition, medicine.SideEffects, medicine.Usage, medicine.ReplaceMedicine, medicine.Name, true, false));
                    }
                    else
                    {
                        medicineList.Add(new MedicineDTO(medicine.Composition, medicine.SideEffects, medicine.Usage, medicine.ReplaceMedicine, medicine.Name, false, false));
                    }
                }

            }
            return medicineList;
        }

        private bool CheckIfInPrescriptions(Medicine medicine, List<Prescription> prescriptions)
        {
            bool ret = false;
            foreach (Prescription prescription in prescriptions)
            {
                if (prescription.Medicine.Name.Equals(medicine.Name))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
    }
}
