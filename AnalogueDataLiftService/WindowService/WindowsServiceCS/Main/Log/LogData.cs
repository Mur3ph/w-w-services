﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Logging
{
    class LogData
    {
        public string Message { get; set; }
        public string LogTime { get; set; }
        public string LogDate { get; set; }

        public LogData(string message)
        {
            Message = message;
            LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            LogTime = DateTime.Now.ToString("HH:mm:ss.fff tt");
        }
    }
}
