using System;

namespace Schedule.Moblie.Models
{
    public interface IEventBuilder
    {
        void BuildBase(string name, DayOfWeek weekday, TimeSpan startTime, TimeSpan endTime);
        void BuildAdditionalInfo(string place, string personToMeet);
        void BuildNote(string note);
    }
}