using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Storages
{
    public class EmployeeFileStorage
    {
        private string fileLocation;

        public EmployeeFileStorage()
        {
            this.fileLocation = "../../../FileStorage/employees.json";
        }

        public void SaveEmployees(List<Employee> allEmployees)
        {
            var file = JsonConvert.SerializeObject(allEmployees, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

        public List<Employee> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<Employee> allEmployees = JsonConvert.DeserializeObject<List<Employee>>(text);

            return allEmployees;
        }
    }
}
