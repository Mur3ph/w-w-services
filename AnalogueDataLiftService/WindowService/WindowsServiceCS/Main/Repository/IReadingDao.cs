using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Repository
{
    interface IReadingDao
    {
        string GetReadings(string lastRunDate, string remoteDbName, string connectionString);
    }
}
