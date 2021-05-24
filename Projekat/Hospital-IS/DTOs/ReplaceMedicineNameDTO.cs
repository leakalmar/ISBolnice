using System;
using System.Collections.Generic;
using System.Text;

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
