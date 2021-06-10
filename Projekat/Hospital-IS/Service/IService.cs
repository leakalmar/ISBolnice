using Hospital_IS.Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Service
{
    public interface IService<T> where T:class
    {
        public void SetStorage(IStorageFactory<T> storageFactory);
    }
}
