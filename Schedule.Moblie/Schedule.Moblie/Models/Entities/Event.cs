using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Moblie.Models.Entities
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Weekday { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Place { get; set; }
        public string PersonToMeet { get; set; }
        public string Note { get; set; }

        public Event ShallowCopy()
        {
            return (Event)MemberwiseClone();
        }

        public Event DeepCopy()
        {
            var clone = (Event)MemberwiseClone();
            clone.Id = new Guid();
            clone.Name = string.Copy(Name);
            clone.Note = "";
            return clone;
        }

    }
}
