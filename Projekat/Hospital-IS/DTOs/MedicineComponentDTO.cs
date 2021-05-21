using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class MedicineComponentDTO
    {
        public String Component { get; set; }

        public MedicineComponentDTO(string component)
        {
            this.Component = component;
        }
    }
}
