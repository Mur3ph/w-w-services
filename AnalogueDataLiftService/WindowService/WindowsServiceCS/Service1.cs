using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Configuration;
using Microsoft.Win32;
using WindowsServiceCS.Main.Services;
using WindowsServiceCS.Main.Repository;
using WindowsServiceCS.Main.Configuration;
using WindowsServiceCS.Main.Logging;


namespace WindowsServiceCS
{
    public partial class Service1 : ServiceBase
    {
        private static readonly Logger _log = new Logger(new Service1().GetType());
        private static System.Timers.Timer _timer;
        private static int _interval; // 300000 MILLISECONDS = 5 MINUTES, 30000 MILLISECONDS = 30 SECONDS, 10 * 60 * 1000 EVERY 10 MINUTES
        private LocalRegistryService _registry;
        private IReadingDao _readingDao;
        private IDeviceDao _deviceDao;
        private ScheduledRun _run;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _log.Debug("+OnStart()");
            this.setTimer();
            this.doDataLiftWork();
            _log.Debug("-OnStart()");
        }

        protected override void OnStop()
        {
            _log.Debug("-OnStop()");
            //this._registry.DeleteKey();
            _timer.Dispose();
            _log.Debug("-OnStop()");
        }

        //Application wakes up: Setting the timer through the poll interval key from the registry. 
        private void setTimer()
        {
            try
            {
                _log.Debug("+setTimer()");
                //Create and Start the Timer
                _timer = new System.Timers.Timer();
                this.setInterval();
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerElapsed);
                _timer.AutoReset = true;
                _timer.Enabled = true;
                _timer.Start();
                _log.Debug("-setTimer()");
            }
            catch (Exception ex)
            {
                _log.Error("-setTimer() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _log.Debug("+TimerElapsed()");
                _timer.Stop();
                this.doDataLiftWork();
                this._registry = new LocalRegistryService();
                this.setInterval();
                _timer.Start();
                _log.Debug("-TimerElapsed()");
            }
            catch (Exception ex)
            {
                _log.Error("-TimerElapsed() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
        }

        //Poll interval key from the registry to set the timer
        private void setInterval()
        {
            _log.Debug("+setInterval()");
            this._registry = new LocalRegistryService();
            if (this._registry.isRegistry())
            {
                Int32.TryParse(this._registry.getTimePollIntervalFromRegistry(), out _interval);
                _timer.Interval = _interval;
            }
            else
            {
                _interval = 60000; //60000 = 60 seconds, 900000 = 15 minutes
            }
            _log.Debug("-setInterval()");
        }

        //Checks whether "Last Run Date" is greater than or equal to a day
        private bool isGreaterThanADay(string lastRundate)
        {
            try
            {
                _log.Debug("+isGreaterThanADay()");
                DateTime previousDateTime;
                if (lastRundate != null || lastRundate != "")
                {
                    previousDateTime = DateTime.Parse(lastRundate);
                }
                else
                {
                    previousDateTime = DateTime.Now;
                }

                //Find the difference between the two dates.. 
                TimeSpan timeSpan = DateTime.Now.Subtract(previousDateTime);
                int NumberOfDays = timeSpan.Days; //..in days
                if (NumberOfDays >= 1)
                {
                    _log.Debug("-isGreaterThanADay()");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("-isGreaterThanADay() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            _log.Debug("-isGreaterThanADay()");
            return false;
        }

        private void doDataLiftWork()
        {
            try
            {
                _log.Debug("+doDataLiftWork()");
                this._registry = new LocalRegistryService();
                if (this._registry.isRegistry())
                {
                    if (this.isGreaterThanADay(_registry.getLastRunDateFromRegistry()) || this._registry.getRunNowBooleanFromRegistry())
                    {
                        this._readingDao = new ReadingDao();
                        this._deviceDao = new DeviceDao();
                        this._run = new ScheduledRun(this._registry, this._readingDao, this._deviceDao);
                        this._run.getDataAndPostToWebService();

                        //After use, set date to now and run now to false
                        this._registry.setRegistryValueLastRunDateToNow();
                        this._registry.setRegistryValueRunNowBoolean();
                    }
                }
                else
                {
                    this._registry.CreateRegistryPathAndAllValues();
                }
                _log.Debug("-doDataLiftWork()");
            }
            catch (Exception ex)
            {
                _log.Error("-doDataLiftWork() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
        }

    }
}
