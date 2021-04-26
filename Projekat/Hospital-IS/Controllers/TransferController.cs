using Model;
using Service;
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

        public Boolean TransferEquipmentStatic(Model.Room sourceRoom, Model.Room destiantionRoom, Model.Equipment equip, int quantity, DateTime startDate, DateTime endDate, String description)
        {
            return TransferService.Instance.TransferEquipmentStatic(sourceRoom, destiantionRoom, equip, quantity, startDate, endDate, description);
        }

        public void TransferEquipment(Model.Room sourceRoom, Model.Equipment equip, int quantity)
        {
            TransferService.Instance.TransferEquipment(sourceRoom, equip, quantity);
        }

        public void TransferStaticEquipment(int sourceRoomId, int destinationRoomId, Model.Equipment equip, int quantity)
        {
            TransferService.Instance.TransferStaticEquipment(sourceRoomId, destinationRoomId, equip, quantity);
        }

        public List<Transfer> getAllTransfers()
        {
            return TransferService.Instance.getAllTransfers();
        }
    }
}
