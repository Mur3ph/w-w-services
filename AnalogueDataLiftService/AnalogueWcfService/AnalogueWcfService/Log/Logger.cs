using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AnalogueWindowService.Main.Log
{
    public class Logger
    {
        private static readonly object _syncObject = new object();
        private static Type _type;
        private static readonly string _fileName = HttpContext.Current.Request.MapPath("DunravenSystemsLogs.txt");

        public Logger(){}

        public Logger(Type type)
        {
            _type = type;
        }

        public void Info(string message)
        {
            lock (_syncObject)
            {
                StreamWriter fillWriter = null;
                try
                {
                    LogData logData = new LogData(message);
                    using (fillWriter = File.AppendText(_fileName))
                    {
                        fillWriter.WriteLine("[" + logData.LogDate + " " + logData.LogTime + "]  INFO - " + logData.Message + " " + _type.ToString());
                        fillWriter.WriteLine("--------------------------------------------------------------------------------------");
                    }
                }
                catch (IOException e)
                {
                    fillWriter.Close();
                }
                finally
                {
                    if (fillWriter != null)
                    {
                        fillWriter.Close();
                        fillWriter = null;
                    }
                }
            }
        }

        public void Debug(string message)
        {
            lock (_syncObject)
            {
                StreamWriter fillWriter = null;
                try
                {
                    LogData logData = new LogData(message);
                    using (fillWriter = File.AppendText(_fileName))
                    {
                        fillWriter.WriteLine("[" + logData.LogDate + " " + logData.LogTime + "]  DEBUG - " + logData.Message + " " + _type.ToString());
                        fillWriter.WriteLine("--------------------------------------------------------------------------------------");
                    }
                }
                catch (IOException e)
                {
                    fillWriter.Close();
                }
                finally
                {
                    if (fillWriter != null)
                    {
                        fillWriter.Close();
                        fillWriter = null;
                    }
                }
            }
        }

        public void Error(string message)
        {
            lock (_syncObject)
            {
                StreamWriter fillWriter = null;
                try
                {
                    LogData logData = new LogData(message);
                    using (fillWriter = File.AppendText(_fileName))
                    {
                        fillWriter.WriteLine("[" + logData.LogDate + " " + logData.LogTime + "]  ERROR - " + logData.Message + " " + _type.ToString());
                        fillWriter.WriteLine("--------------------------------------------------------------------------------------");
                    }
                }
                catch (IOException e)
                {
                    fillWriter.Close();
                }
                finally
                {
                    if (fillWriter != null)
                    {
                        fillWriter.Close();
                        fillWriter = null;
                    }
                }
            }
        }
    }
}
