using System;

namespace Hospital_IS.DTOs
{
    public class ReplaceMedicineNameDTO
    {
        public String Name { get; set; }

        public ReplaceMedicineNameDTO(string name)
        {
            Name = name;
        }
    }
}
