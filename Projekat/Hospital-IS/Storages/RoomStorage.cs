using Hospital_IS.Storages;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Storages
{
    public class RoomStorage: GenericFileStorage<Room>
    {
       
        public RoomStorage()
        {
            this.fileLocation = "../../../FileStorage/rooms.json";
        }

       

    }
}