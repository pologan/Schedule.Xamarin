using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Schedule.Moblie.DB
{
    class DbSingleton
    {
        private DbSingleton() { }

        private static SQLiteAsyncConnection _instance;

        public static SQLiteAsyncConnection GetInstance()
        {
            if (_instance == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Schedule.db");

                _instance = new SQLiteAsyncConnection(databasePath);
            }
            return _instance;
        }
    }
}
