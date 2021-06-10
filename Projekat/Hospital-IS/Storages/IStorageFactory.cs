using Hospital_IS.Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Storages
{
    public interface IStorageFactory<T> where T:class
    {
        T GetStorage();
    }
}
