using Schedule.Moblie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Moblie.SaveStrategy
{
    public interface ISaveToFileStrategy
    {
        void Save(List<Event> list);
    }
}
