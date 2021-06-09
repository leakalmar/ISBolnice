using DTOs;
using Hospital_IS.DTOs;
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

        public MedicineDTO GetByName(string medicineName)
        {
            Medicine med = MedicineService.Instance.GetByName(medicineName);
            if (med != null)
            {
                return ConvertMedicineToDTO(med);
            }
            else
            {
                return null;
            }
        }

        public MedicineDTO ConvertMedicineToDTO(Medicine medicine)
        {
            List<MedicineComponentDTO> componentDTOs = new List<MedicineComponentDTO>();
            List<ReplaceMedicineNameDTO> replacmentDTOs = new List<ReplaceMedicineNameDTO>();
            foreach (MedicineComponent component in medicine.Composition)
            {
                componentDTOs.Add(new MedicineComponentDTO(component.Component));
            }
            foreach (ReplaceMedicineName replace in medicine.ReplaceMedicine)
            {
                replacmentDTOs.Add(new ReplaceMedicineNameDTO(replace.Name));
            }
            return new MedicineDTO(componentDTOs, medicine.SideEffects, medicine.Usage, replacmentDTOs, medicine.Name, false, false);
        }
        public List<MedicineDTO> ConvertMedicineToDTO(List<Medicine> medicines)
        {
            List<MedicineDTO> list = new List<MedicineDTO>();
            foreach(Medicine m in medicines)
            {
                list.Add(ConvertMedicineToDTO(m));
            }
            return list;
        }
        public Medicine ConvertDTOToMedicine(MedicineDTO medicineDTO)
        {
            List<MedicineComponent> components = new List<MedicineComponent>();
            List<ReplaceMedicineName> replacements = new List<ReplaceMedicineName>();
            foreach (MedicineComponentDTO componentDTO in medicineDTO.Composition)
            {
                components.Add(new MedicineComponent(componentDTO.Component));
            }
            foreach (ReplaceMedicineNameDTO replaceDTO in medicineDTO.ReplaceMedicine)
            {
                replacements.Add(new ReplaceMedicineName(replaceDTO.Name));
            }
            return new Medicine(medicineDTO.Name, components, medicineDTO.SideEffects, medicineDTO.Usage, replacements);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            MedicineService.Instance.UpdateMedicine(medicine);
        }

        public void UpdateMedicineWithName(string oldName,Medicine medicine)
        {
           
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

        public List<MedicineDTO> GenerateListOfMedicines(List<string> allergies, List<PrescriptionDTO> prescriptions)
        {
            List<MedicineDTO> medicineList = new List<MedicineDTO>();

            foreach (Medicine medicine in MedicineService.Instance.AllMedicines)
            {
                if (PatientService.Instance.CheckIfAllergicToMedicine(allergies, medicine.Name))
                {
                    medicineList.Add(new MedicineDTO(ConvertToMedicineComponentDTO(medicine.Composition), medicine.SideEffects,medicine.Usage, ConvertToReplaceMedicineNameDTO(medicine.ReplaceMedicine), medicine.Name, false, true));
                }
                else
                {
                    if (CheckIfInPrescriptions(medicine, prescriptions))
                    {
                        medicineList.Add(new MedicineDTO(ConvertToMedicineComponentDTO(medicine.Composition), medicine.SideEffects, medicine.Usage, ConvertToReplaceMedicineNameDTO(medicine.ReplaceMedicine), medicine.Name, true, false));
                    }
                    else
                    {
                        medicineList.Add(new MedicineDTO(ConvertToMedicineComponentDTO(medicine.Composition), medicine.SideEffects, medicine.Usage, ConvertToReplaceMedicineNameDTO(medicine.ReplaceMedicine), medicine.Name, false, false));
                    }
                }

            }
            return medicineList;
        }

        public List<MedicineComponentDTO> ConvertToMedicineComponentDTO(List<MedicineComponent> medicineComponents)
        {
            List<MedicineComponentDTO> medicineComponentDTOs = new List<MedicineComponentDTO>();
            foreach(MedicineComponent component in medicineComponents)
            {
                medicineComponentDTOs.Add(new MedicineComponentDTO(component.Component));
            }
            return medicineComponentDTOs;
        }

        public List<ReplaceMedicineNameDTO> ConvertToReplaceMedicineNameDTO(List<ReplaceMedicineName> replaceMedicineNames)
        {
            List<ReplaceMedicineNameDTO> replaceMedicineNameDTOs = new List<ReplaceMedicineNameDTO>();
            foreach (ReplaceMedicineName replace in replaceMedicineNames)
            {
                replaceMedicineNameDTOs.Add(new ReplaceMedicineNameDTO(replace.Name));
            }
            return replaceMedicineNameDTOs;
        }

        private bool CheckIfInPrescriptions(Medicine medicine, List<PrescriptionDTO> prescriptions)
        {
            bool ret = false;
            foreach (PrescriptionDTO prescription in prescriptions)
            {
                if (prescription.Medicine.Name.Equals(medicine.Name))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        public bool IsNameUnique(string name)
        {
            return MedicineService.Instance.IsNameUnique(name);
        }
    }
}
