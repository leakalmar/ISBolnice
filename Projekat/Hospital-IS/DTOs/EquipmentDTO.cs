﻿using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class EquipmentDTO
    {
        public EquiptType EquipType { get; set; }
        public int EquiptId { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }
        public String ProducerName { get; set; }

        public EquipmentDTO(EquiptType equipType, int equiptId, string name, int quantity, string producerName)
        {
            EquipType = equipType;
            EquiptId = equiptId;
            Name = name;
            Quantity = quantity;
            ProducerName = producerName;
        }
    }
}