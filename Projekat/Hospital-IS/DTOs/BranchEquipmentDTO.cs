using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class BranchEquipmentDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }

        public BranchEquipmentDTO(string name, string type, string quantity)
        {
            Name = name;
            Type = type;
            Quantity = quantity;
        }
    }
}
