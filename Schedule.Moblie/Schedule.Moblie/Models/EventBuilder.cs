using Schedule.Moblie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Moblie.Models
{
    public class EventBuilder : IEventBuilder
    {
        private Event _event;

        public EventBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _event = new Event();
        }

        public void BuildAdditionalInfo(string place, string personToMeet)
        {
            _event.Place = place;
            _event.PersonToMeet = personToMeet;
        }

        public void BuildBase(string name, DayOfWeek weekday, TimeSpan startTime, TimeSpan endTime)
        {
            _event.Name = name;
            _event.Weekday = (int)weekday;
            _event.StartTime = startTime;
            _event.EndTime = endTime;
        }

        public void BuildNote(string note)
        {
            _event.Note = note;
        }

        public Event GetEvent()
        {
            Event result = _event;

            Reset();

            return result;
        }
    }
}
