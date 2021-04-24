using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class TransferService
    {
        //private TransferFileStorage tfs = new TransferFileStorage();
        public List<Transfer> allTransfer { get; set; }

        private static TransferService instance = null;
        public static TransferService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransferService();
                }
                return instance;
            }
        }

        public TransferService()
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
