using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hospital_IS.Storages
{
    public class GenericFileStorage<T> : IGenericStorage<T> where T : class
    {
        protected string fileLocation;
        public List<T> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<T> objects = JsonConvert.DeserializeObject<List<T>>(text);
            return objects;
        }
        public void Add(T newObject)
        {
            List<T> objects = GetAll();

            objects.Add(newObject);
            SaveAll(objects);
        }

        public void Delete(T selectedObject)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T selectedObject)
        {
            throw new NotImplementedException();
        }
        public void SaveAll(List<T> objects)
        {
            var file = JsonConvert.SerializeObject(objects, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

    }
}
