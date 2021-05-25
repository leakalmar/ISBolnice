using Hospital_IS.ManagerConverter;

namespace Hospital_IS.ManagerViewModel
{
    public class Injector
    {
       

      

        private RoomConverter studentConverter = new RoomConverter();

        public RoomConverter RoomConverter
        {
            get { return studentConverter; }
        }

       
    }
}
