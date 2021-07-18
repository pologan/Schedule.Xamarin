using Schedule.Moblie.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Schedule.Moblie.SaveStrategy
{
    class SaveToXMLStrategy : ISaveToFileStrategy
    {
        public void Save(List<Event> list)
        {
            var xml = new XElement("Events", list.Select(x => new XElement("Event",
                                               new XAttribute("Id", x.Id.ToString()),
                                               new XAttribute("Weekday", x.Weekday.ToString()),
                                               new XAttribute("Name", x.Name),
                                               new XAttribute("StartTime", x.StartTime.ToString()),
                                               new XAttribute("EndTIme", x.EndTime.ToString()))));

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "SavedEvents.xml");

            File.WriteAllText(filename, xml.ToString());
        }
    }
}
