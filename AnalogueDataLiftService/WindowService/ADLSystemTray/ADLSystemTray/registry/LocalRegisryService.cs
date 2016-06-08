using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsServiceCS.Main.Logging;

namespace ADLSystemTray.registry
{
    class LocalRegisryService
    {
        private static readonly Logger _log = new Logger(new LocalRegisryService().GetType());

        //This gets "HKEY_LOCAL_MACHINE"
        private static readonly RegistryKey BASE_REGISTRY_KEY = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

        //Keys..
        private const string REGISTRY_SUBKEY_URL = @"SOFTWARE\Dunraven\AnalogueDataLift";
        private const string REMOTE_DATABASE_NAME = "RemoteDBName";
        private const string LOCAL_CONNECTION_STRING = "LocalConnectionString";
        private const string WEBSERVICE_URL = "WebserviceURL";
        private const string LAST_RUN_DATE = "LastRunDate";
        private const string POLL_INTERVAL = "PollInterval";
        private const string RUN_NOW = "RunNow";

        //Set the url of the web service that we are POSTing the data too
        public void setRegistryValueWebServiceURL(string urlOfWebService)
        {
            try
            {
                _log.Debug("+setRegistryValueWebServiceURL()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(WEBSERVICE_URL, urlOfWebService);
                }
            }
            catch (Exception ex)
            {
                _log.Error("-setRegistryValueWebServiceURL() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-setRegistryValueWebServiceURL()");
        }

        //Get the url of the web service that we are POSTing the analogue data too
        public string getWebServiceURLFromRegistry()
        {
            try
            {
                _log.Debug("+getWebServiceURLFromRegistry()");
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                    {
                        if (isValueNameInRegistry(WEBSERVICE_URL))
                        {
                            _log.Debug("-getWebServiceURLFromRegistry()");
                            return (string)key.GetValue(WEBSERVICE_URL);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-getWebServiceURLFromRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("+getWebServiceURLFromRegistry()");
            return null;
        }

        //Set the last run date in the registry after POST to web service
        public void setRegistryValueLastRunDateToNow(DateTime dateWindowServiceAppWasLastRun)
        {
            try
            {
                _log.Debug("+setRegistryValueLastRunDateToNow()");
                // If the RegistrySubKey doesn't exist -> (null) Doesn't exist, so create..
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                    {
                        _log.Debug("-setRegistryValueLastRunDateToNow()");
                        key.SetValue(LAST_RUN_DATE, formatDate(dateWindowServiceAppWasLastRun));
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-setRegistryValueLastRunDateToNow() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
        }

        //Method to format the date chosen from the date time picker text box
        private string formatDate(DateTime todaysDateTime)
        {
            return ((DateTime)todaysDateTime).ToString("yyyy-MM-dd h:mm:ss tt");
        }

        //Set the name of the databse we need for the Web Service needs to Insert/Update analogue data
        public void setRegistryValueRemoteDatabaseName(string nameOfDatabaseOnApplicationInstallation)
        {
            try
            {
                _log.Debug("+setRegistryValueRemoteDatabaseName()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(REMOTE_DATABASE_NAME, nameOfDatabaseOnApplicationInstallation);
                }
            }
            catch (Exception ex)
            {
                _log.Error("-setRegistryValueRemoteDatabaseName() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-setRegistryValueRemoteDatabaseName()");
        }

        //Get the name of the databse that the Web Service needs to Insert/Update analogue data (Posting with this with data)
        public string getRemoteDatabaseNameFromRegistry()
        {
            try
            {
                _log.Debug("+getRemoteDatabaseNameFromRegistry()");
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                    {
                        if (isValueNameInRegistry(REMOTE_DATABASE_NAME))
                        {
                            _log.Debug("-getRemoteDatabaseNameFromRegistry()");
                            return (string)key.GetValue(REMOTE_DATABASE_NAME);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-getRemoteDatabaseNameFromRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("+getRemoteDatabaseNameFromRegistry()");
            return null;
        }

        //Set the connection string in the registry that we need for this application needs to set to a particular server/database
        public void setRegistryValueLocalConnectionString(string applicationConnectionStringForDatabase)
        {
            try
            {
                _log.Debug("+setRegistryValueLocalConnectionString()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(LOCAL_CONNECTION_STRING, applicationConnectionStringForDatabase);
                }
            }
            catch (Exception ex)
            {
                _log.Error("-setRegistryValueLocalConnectionString() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-setRegistryValueLocalConnectionString()");
        }

        //Get the connection string in the registry that we need for this application needs to set to a particular server/database
        public string getLocalConnectionStringFromRegistry()
        {
            try
            {
                _log.Debug("+getLocalConnectionStringFromRegistry()");
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                    {
                        if (isValueNameInRegistry(LOCAL_CONNECTION_STRING))
                        {
                            _log.Debug("-getLocalConnectionStringFromRegistry()");
                            return ((string)key.GetValue(LOCAL_CONNECTION_STRING));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error("-getLocalConnectionStringFromRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-getLocalConnectionStringFromRegistry()");
            return null;
        }

        //Set the time that we want for the windows application to run in cycles (i.e. Run every 10 minutes)
        public void setRegistryValueTimePollInterval(string pollIntervalTimeInSeconds)
        {
            string pollIntervalTimeInMilliseconds = this.convertSecondsToMilliseconds(pollIntervalTimeInSeconds);
            try
            {
                _log.Debug("+setRegistryValueTimePollInterval()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(POLL_INTERVAL, pollIntervalTimeInMilliseconds);
                }
            }
            catch (Exception ex)
            {
                _log.Error("-setRegistryValueTimePollInterval() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-setRegistryValueTimePollInterval()");
        }

        //Get the time that was set in the registry or default time set by this application (i.e. Run every 10 minutes)
        public string getTimePollIntervalFromRegistry()
        {
            try
            {
                _log.Debug("+getTimePollIntervalFromRegistry()");
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                    {
                        if (isValueNameInRegistry(POLL_INTERVAL))
                        {
                            _log.Debug("-getTimePollIntervalFromRegistry()");
                            return (string)key.GetValue(POLL_INTERVAL);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-getTimePollIntervalFromRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            //_log.Debug("+getTimePollIntervalFromRegistry()");
            return null;
        }

        //Convert user input (string) to a integer to calculate milliseconds, then..
        //Convert back to string to store in the Registry
        private string convertSecondsToMilliseconds(string pollIntervalTimeInSeconds)
        {
            int x = Int32.Parse(pollIntervalTimeInSeconds);
            return (x * 1000).ToString();
        }

        //Set the run now value to false after run (i.e. Run service when the user wants)
        public void setRegistryValueRunNowBoolean(string runNowValue)//runNowValue can be either true or false
        {
            try
            {
                _log.Debug("+setRegistryValueRunNowBoolean()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(RUN_NOW, runNowValue);
                }
            }
            catch (Exception ex)
            {
                _log.Error("-setRegistryValueRunNowBoolean() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-setRegistryValueRunNowBoolean()");
        }

        //Get the date of the last time the application was run from the registry to check if it's greater than a day
        public string getLastRunDateFromRegistry()
        {
            try
            {
                _log.Debug("+getLastRunDateFromRegistry()");
                DateTime date;
                string dateString = "";
                //var formats = new[] { "MM/dd/yyyy", "MM-dd-yyyy", "yyyy-MM-dd h:mm:ss tt", "yyyy-MM-dd" };
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                    {
                        if (isValueNameInRegistry(LAST_RUN_DATE))
                        {
                            dateString = (string)key.GetValue(LAST_RUN_DATE);
                        }
                    }
                    // If the Registry date is incorrect format -> (null) Doesn't exist, so create.
                    if (DateTime.TryParse(dateString, out date))
                    {
                        if (dateString != null || dateString != "")
                        {
                            _log.Debug("-getLastRunDateFromRegistry()");
                            return dateString;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-getLastRunDateFromRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-getLastRunDateFromRegistry()");
            return null;
        }

        //Check to see if the registry directory was created
        public bool isRegistry()
        {
            try
            {
                _log.Debug("+isRegistry()");
                // Setting..
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                {
                    if (key == null)
                    {
                        _log.Debug("++isRegistry()");
                        //CreateRegistryPathAndAllValues();
                        return false;
                    }
                    else
                    {
                        _log.Debug("-isRegistry()");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-isRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
                throw;
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
        }

        //Check to see if the registry key has Value Name
        public bool isValueNameInRegistry(string valueName)
        {
            try
            {
                _log.Debug("+isValueNameInRegistry()");
                // Setting..
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                {
                    if (key != null)
                    {
                        // If the RegistrySubKey doesn't exist -> (null) Doesn't exist, so create..
                        if (key.GetValue(valueName) == null)
                        {
                            return false;
                        }
                        _log.Debug("-isValueNameInRegistry()");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _log.Error("-isValueNameInRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
                throw;
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
        }
    }
}
