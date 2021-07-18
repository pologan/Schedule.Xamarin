using Schedule.Moblie.DB;
using Schedule.Moblie.Models.Entities;
using Schedule.Moblie.Models.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Schedule.Moblie.Services
{
    public class ScheduleService : IScheduleService
    {

        private SQLiteAsyncConnection _db = DbSingleton.GetInstance();

        public async Task<bool> AddEvent(Event e)
        {
            try
            {
                await Init();
                var i = await _db.InsertAsync(e);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<Event> GetEventById(Guid id)
        {
            try
            {
                await Init();
                var ev = await _db.Table<Event>().FirstAsync(e => e.Id == id);

                if (ev != null)
                {
                    return ev;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public async Task<List<Event>> GetEventList()
        {
            try
            {
                await Init();
                var list = await _db.Table<Event>().OrderBy(p => p.StartTime).ThenBy(p => p.EndTime).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new List<Event>();
            }
        }

        private async Task Init()
        {
           _db = DbSingleton.GetInstance();

            var result = await _db.CreateTableAsync<Event>();

            var i = 12;
        }

        public async Task<bool> RemoveEvent(Guid id)
        {
            try
            {
                await Init();
                await _db.DeleteAsync<Event>(id);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> UpdateEvent(Event e)
        {
            try
            {
                await Init();
                await _db.UpdateAsync(e);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
