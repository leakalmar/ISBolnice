using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Storages
{
    interface IGenericStorage<T> where T:class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T newObject);
        void Update(T selectedObject);
        void Delete(T selectedObject);
    }
}
