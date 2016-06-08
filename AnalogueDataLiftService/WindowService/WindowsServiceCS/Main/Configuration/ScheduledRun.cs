using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsServiceCS.Main.Logging;
using WindowsServiceCS.Main.Repository;
using WindowsServiceCS.Main.Services;
using WindowsServiceCS.Main.Transport;

namespace WindowsServiceCS.Main.Configuration
{
    class ScheduledRun
    {
        private static readonly Logger _log = new Logger(new ScheduledRun().GetType());
        private static readonly object _syncObject = new object();
        private static readonly string WEBSERVICE_READING_METHOD = "/sendReadingData";
        private static readonly string WEBSERVICE_DEVICE_METHOD = "/sendDeviceData";
        private LocalRegistryService _registry;
        private IReadingDao _readingDao;
        private IDeviceDao _deviceDao;
        private PostJsonSender _post;

        public ScheduledRun(LocalRegistryService registry, IReadingDao readingDao, IDeviceDao deviceDao)
        {
            _log.Debug("+ScheduledRun()");
            this._registry = registry;
            this._readingDao = readingDao;
            this._deviceDao = deviceDao;
            _log.Debug("-ScheduledRun()");
        }

        public ScheduledRun()
        {
        }

        //This is called once a day and it configures all the work together 
        //(Gets the data from Device, Readings tables, transforms to Json and POSTs to Wcf Web Service) //[MethodImpl(MethodImplOptions.Synchronized)]
        public void getDataAndPostToWebService()
        {
            try
            {
                _log.Debug("+getDataAndPostToWebService()");
                string connectionString = this._registry.getLocalConnectionStringFromRegistry();
                string remoteDbName = this._registry.getRemoteDatabaseNameFromRegistry();
                string lastRunDate = this._registry.getLastRunDateFromRegistry();
                string webServiceUrl = this._registry.getWebServiceURLFromRegistry();

                string readingJson = this._readingDao.GetReadings(lastRunDate, remoteDbName, connectionString);
                string deviceJson = this._deviceDao.GetDevices(lastRunDate, remoteDbName, connectionString);

                this._post = new PostJsonSender();
                this._post.postJson(readingJson, webServiceUrl + WEBSERVICE_READING_METHOD);
                this._post.postJson(deviceJson, webServiceUrl + WEBSERVICE_DEVICE_METHOD);
            }
            catch (Exception ex)
            {
                _log.Error("-getDataAndPostToWebService() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            _log.Debug("-getDataAndPostToWebService()");
        }

       
    }
}
