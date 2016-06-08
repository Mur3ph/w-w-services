using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Logging
{
    class Logger
    {
        private static readonly object _syncObject = new object();
        private static System.IO.StreamWriter _file;
        private static Type _type;
        private const string PATH = "C:\\Dunraven Systems\\";
        private const string FILE_NAME = PATH + "logging-ws.txt";

        public Logger()
        {

        }

        public Logger(Type type)
        {
            _type = type;
        }

        public void Info(string message)
        {
            lock (_syncObject)
            {
                try
                {
                    LogData logData = new LogData(message);
                    using (_file = new System.IO.StreamWriter(FILE_NAME, true))
                    {
                        _file.WriteLine("[" + logData.LogDate + " " + logData.LogTime + "]  INFO - " + logData.Message + " " + _type.ToString());
                        _file.WriteLine("--------------------------------------------------------------------------------------");
                        _file.Flush();
                    }
                }
                catch (IOException e)
                {
                    _file.Close();
                    throw;
                }
                finally
                {
                    if (_file != null)
                    {
                        _file.Close();
                        _file = null;
                    }
                }
            }
        }

        public void Debug(string message)
        {
            lock (_syncObject)
            {
                try
                {
                    LogData logData = new LogData(message);
                    using (_file = new System.IO.StreamWriter(FILE_NAME, true))
                    {
                        _file.WriteLine("[" + logData.LogDate + " " + logData.LogTime + "]  DEBUG - " + logData.Message + " " + _type.ToString());
                        _file.WriteLine("--------------------------------------------------------------------------------------");
                        _file.Flush();
                    }
                }
                catch (IOException e)
                {
                    _file.Close();
                    throw;
                }
                finally
                {
                    if (_file != null)
                    {
                        _file.Close();
                        _file = null;
                    }
                }
            }
        }

        public void Error(string message)
        {
            lock (_syncObject)
            {
                try
                {
                    LogData logData = new LogData(message);
                    using (_file = new System.IO.StreamWriter(FILE_NAME, true))
                    {
                        _file.WriteLine("[" + logData.LogDate + " " + logData.LogTime + "]  ERROR - " + logData.Message + " " + _type.ToString());
                        _file.WriteLine("--------------------------------------------------------------------------------------");
                        _file.Flush();
                    }
                }
                catch (IOException e)
                {
                    _file.Close();
                    throw;
                }
                finally
                {
                    if (_file != null)
                    {
                        _file.Close();
                        _file = null;
                    }
                }
            }
        }
    }
}
