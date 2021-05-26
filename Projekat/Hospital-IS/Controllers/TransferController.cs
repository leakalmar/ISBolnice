using DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

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

        public Boolean ScheduleStaticTransfer(StaticTransferAppointmentDTO staticTransfer)
        {
            return TransferService.Instance.ScheduleStaticTransfer(staticTransfer);
        }

        public void ReduceEquipmentQuantity(Room sourceRoom, Equipment equip, int quantity)
        {
            TransferService.Instance.ReduceEquipmentQuantity(sourceRoom, equip, quantity);
        }

        public void ExecuteStaticTransfer(Transfer transfer)
        {
           
            TransferService.Instance.ExecuteStaticTransfer(transfer);
        }

        public List<Transfer> GetAllTransfers()
        {
            return TransferService.Instance.GetAllTransfers();
        }
    }
}
