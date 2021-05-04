using System;
using System.Collections;

namespace Model
{
    public class Equipment
    {
        public EquiptType EquipType { get; set; }
        public int EquiptId { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }

       

        public Equipment(EquiptType equipType, int equiptId, string name, int quantity)
        {
            this.EquipType = equipType;
            this.EquiptId = equiptId;
            this.Name = name;
            this.Quantity = quantity;
          
        }

        public Equipment()
        {

        }


    }
}