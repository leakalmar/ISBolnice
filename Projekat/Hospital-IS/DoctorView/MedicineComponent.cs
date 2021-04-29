using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorView
{
    public class MedicineComponent
    {
        public String Component { get; set; }

        public MedicineComponent(string component)
        {
            this.Component = component;
        }
    }
}
