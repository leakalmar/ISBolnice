using Controllers;
using Hospital_IS.ManagerConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
