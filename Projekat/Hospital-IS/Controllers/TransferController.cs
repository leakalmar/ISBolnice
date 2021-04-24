using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    class TransferController
    {
        private static TransferController instance = null;
        public static TransferController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransferController();
                }
                return instance;
            }
        }

        public TransferController()
        {

        }

        public Boolean TransferEquipmentStatic(Model.Room sourceRoom, Model.Room destiantionRoom, Model.Equipment equip, int quantity, DateTime startDate, int endDate, String description)
        {
            throw new NotImplementedException();
        }

        public void TransferEquipment(Model.Room sourceRoom, Model.Equipment equip, int quantity)
        {
            throw new NotImplementedException();
        }

        public void TransferStaticEquipment(int sourceRoomId, int destinationRoomId, Model.Equipment equip, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
