using Model;
using Service;
using System.Collections.Generic;

namespace Controllers
{
    public class BedController
    {
        private static BedController instance = null;
        public static BedController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BedController();
                }
                return instance;
            }
        }

        public BedController()
        {

        }

        public void AddBed(Bed bed)
        {
            BedService.Instance.AddBed(bed);
        }

        public void RemoveBed(Bed bed)
        {
            BedService.Instance.RemoveBed(bed);
        }

        public void UpdateBed(Bed bed)
        {
            BedService.Instance.UpdateBed(bed);
        }

        public List<Bed> GetBedsByRoomId(int roomId)
        {
            return BedService.Instance.GetBedsByRoomId(roomId);
        }

        public Bed GetBedById(int bedId)
        {
            return BedService.Instance.GetBedById(bedId);
        }
    }
}
