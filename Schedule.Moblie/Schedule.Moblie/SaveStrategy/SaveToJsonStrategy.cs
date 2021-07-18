using Newtonsoft.Json;
using Schedule.Moblie.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Schedule.Moblie.SaveStrategy
{
    class SaveToJsonStrategy : ISaveToFileStrategy
    {
        public void Save(List<Event> list)
        {
            var json = JsonConvert.SerializeObject(list);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "SavedEvents.json");

            File.WriteAllText(filename, json);
        }
    }
}
