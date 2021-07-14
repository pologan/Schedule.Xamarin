using Schedule.Moblie.Models.Entities;
using Schedule.Moblie.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Moblie.Services
{
    public interface IScheduleService
    {
        Task<bool> AddEvent(Event e);
        Task<bool> RemoveEvent(Guid id);
        Task<Event> GetEventById(Guid id);
        Task<List<Event>> GetEventList();
        Task<bool> UpdateEvent(Event e);
    }
}
