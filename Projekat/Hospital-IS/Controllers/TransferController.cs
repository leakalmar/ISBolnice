using DTOs;
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

        public Boolean TransferEquipmentStatic(StaticTransferAppointmentDTO staticTransfer)
        {
            return TransferService.Instance.ScheduleStaticTransfer(staticTransfer);
        }

        public void TransferEquipment(Room sourceRoom, Equipment equip, int quantity)
        {
            TransferService.Instance.ReduceEquipmentQuantity(sourceRoom, equip, quantity);
        }

        public void TransferStaticEquipment(Transfer transfer)
        {
           
            TransferService.Instance.ExecuteStaticTransfer(transfer);
        }

        public List<Transfer> getAllTransfers()
        {
            return TransferService.Instance.getAllTransfers();
        }
    }
}
