using Model;
using Service;

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
    }
}
