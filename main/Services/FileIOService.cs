using main.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<Schedule> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<Schedule>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                FileInfo fileInf = new FileInfo(PATH);
                if (fileInf.Length == 0)
                {
                    return new BindingList<Schedule>();
                }
                return JsonConvert.DeserializeObject<BindingList<Schedule>>(fileText);
            }
        }
        public void SaveData(object _todoData)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(_todoData);
                writer.Write(output);
            }
        }
    }
}
