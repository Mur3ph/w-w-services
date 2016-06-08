using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsServiceCS.Main.Logging;

namespace WindowsServiceCS.Main.Services
{
    class LocalRegistryService
    {
         //This gets "HKEY_LOCAL_MACHINE"
        private static readonly RegistryKey BASE_REGISTRY_KEY = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

        //Keys..
        private const string REGISTRY_SUBKEY_URL = @"SOFTWARE\Dunraven\AnalogueDataLift";
        private const string LAST_RUN_DATE = "LastRunDate";
        private const string REMOTE_DATABASE_NAME = "RemoteDBName";
        private const string LOCAL_CONNECTION_STRING = "LocalConnectionString";
        private const string WEBSERVICE_URL = "WebserviceURL";
        private const string POLL_INTERVAL = "PollInterval";
        private const string RUN_NOW = "RunNow";

        //Values of the keys..
        private static readonly DateTime LAST_RUN_DATE_VALUE = DateTime.Now; //Date Format: Year-Month-Day:12.00:00
        private static readonly string REMOTE_DATABASE_NAME_VALUE = "CertasClientDB"; //cnc-to-test-analog //CertasClientDB
        private static readonly string LOCAL_CONNECTION_STRING_VALUE = @"data source=USER-PC; initial catalog=CertasClientDB; integrated security=False; User ID=sa; Password=password!;";
        private static readonly string WEBSERVICE_URL_TEMP_VALUE = "http://localhost:62060/AnalogueService.svc";
        private static readonly string POLL_INTERVAL_VALUE = "60000";  //Poll Interval: 60000 (60 seconds), 300000 (5 minutes)
        private static readonly string RUN_NOW_VALUE = "false"; //Can be either true or false

        private static readonly Logger _log = new Logger(new LocalRegistryService().GetType());

        public LocalRegistryService()
        {
           
        }

        //Create ALL the Registry keys and values..
        public void CreateRegistryPathAndAllValues()
        {
            try
            {
                _log.Debug("+CreateRegistryPathAndValues()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    //Date Format: Year-Month-Day:12.00:00, Poll Interval: 300000 (5 minutes)
                    key.SetValue(LAST_RUN_DATE, getTodaysRegistryDateTimeToString());
                    key.SetValue(REMOTE_DATABASE_NAME, REMOTE_DATABASE_NAME_VALUE);
                    key.SetValue(LOCAL_CONNECTION_STRING, LOCAL_CONNECTION_STRING_VALUE);
                    key.SetValue(WEBSERVICE_URL, WEBSERVICE_URL_TEMP_VALUE);
                    key.SetValue(POLL_INTERVAL, POLL_INTERVAL_VALUE);
                    key.SetValue(RUN_NOW, RUN_NOW_VALUE);
                }
            }
            catch (Exception ex)
            {
                _log.Error("-CreateRegistryPathAndValues() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-CreateRegistryPathAndValues()");
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
                        else
                        {
                            setRegistryValueLastRunDateToNow();
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
                        else
                        {
                            _log.Debug("-getLastRunDateFromRegistry()");
                            return getTodaysRegistryDateTimeToString();
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
            return getLastRunDateFromRegistry();
        }

        //Set the last run date in the registry after POST to web service
        public void setRegistryValueLastRunDateToNow()
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
                        key.SetValue(LAST_RUN_DATE, getTodaysRegistryDateTimeToString());
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

        //Method to get todays date after daily POST and cast to string (Registry only accepts string values)
        private string getTodaysRegistryDateTimeToString()
        {
            _log.Debug("+getTodaysRegistryDateTimeToString()");
            DateTime todaysDateTime = DateTime.Now;
            return ((DateTime)todaysDateTime).ToString("yyyy-MM-dd h:mm:ss tt");
        }

        //TODO DELETE (NOT BEING USED ANY MORE)
        private DateTime getPreviousRegistryDateStringToDateTime()
        {
            _log.Debug("+getPreviousRegistryDateStringToDateTime()");
            string prevoiusRegistryDateTime = getLastRunDateFromRegistry();
            return DateTime.ParseExact(prevoiusRegistryDateTime, "yyyy-MM-dd HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
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
                        else
                        {
                            setRegistryValueRemoteDatabaseName();
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
            return getRemoteDatabaseNameFromRegistry();
        }

        //Set the name of the databse we need for the Web Service needs to Insert/Update analogue data
        public void setRegistryValueRemoteDatabaseName()
        {
            try
            {
                _log.Debug("+setRegistryValueRemoteDatabaseName()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(REMOTE_DATABASE_NAME, REMOTE_DATABASE_NAME_VALUE);
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
                        else
                        {
                            setRegistryValueLocalConnectionString();
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
            return getLocalConnectionStringFromRegistry();
        }

        //Set the connection string in the registry that we need for this application needs to set to a particular server/database
        public void setRegistryValueLocalConnectionString()
        {
            try
            {
                _log.Debug("+setRegistryValueLocalConnectionString()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(LOCAL_CONNECTION_STRING, LOCAL_CONNECTION_STRING_VALUE);
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
                        else
                        {
                            setRegistryValueWebServiceURL();
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
            return getWebServiceURLFromRegistry();
        }

        //Set the url of the web service that we are POSTing the data too
        public void setRegistryValueWebServiceURL()
        {
            try
            {
                _log.Debug("+setRegistryValueWebServiceURL()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(WEBSERVICE_URL, WEBSERVICE_URL_TEMP_VALUE);
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
                        else
                        {
                            setRegistryValueTimePollInterval();
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
            return getTimePollIntervalFromRegistry();
        }

        //Set the time that we want for the windows application to run in cycles (i.e. Run every 10 minutes)
        public void setRegistryValueTimePollInterval()
        {
            try
            {
                _log.Debug("+setRegistryValueTimePollInterval()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(POLL_INTERVAL, POLL_INTERVAL_VALUE);
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

        //################################################################

        //Get the boolean (true/false) that was set in the registry (i.e. Run service when the user wants)
        public bool getRunNowBooleanFromRegistry()
        {
            try
            {
                _log.Debug("+getRunNowBooleanFromRegistry()");
                if (isRegistry())
                {
                    using (BASE_REGISTRY_KEY)
                    using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                    {
                        if (isValueNameInRegistry(RUN_NOW))
                        {
                            _log.Debug("-getRunNowBooleanFromRegistry()");
                            string value = (string)key.GetValue(RUN_NOW);
                            if (value == "true")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            setRegistryValueRunNowBoolean();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("-getRunNowBooleanFromRegistry() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                BASE_REGISTRY_KEY.Close();
            }
            _log.Debug("-getRunNowBooleanFromRegistry()");
            return getRunNowBooleanFromRegistry();
        }

        //Set the run now value to false after run (i.e. Run service when the user wants)
        public void setRegistryValueRunNowBoolean()
        {
            try
            {
                _log.Debug("+setRegistryValueRunNowBoolean()");
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.CreateSubKey(REGISTRY_SUBKEY_URL))
                {
                    key.SetValue(RUN_NOW, RUN_NOW_VALUE);
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

        //################################################################

        //Check to see if the registry directory was created
        public bool isRegistry()
        {
            try
            {
                //log.Debug("+isRegistry()");
                // Setting..
                using (BASE_REGISTRY_KEY)
                using (var key = BASE_REGISTRY_KEY.OpenSubKey(REGISTRY_SUBKEY_URL))
                {
                    if (key == null)
                    {
                        //log.Debug("++isRegistry()");
                        CreateRegistryPathAndAllValues();
                        return false;
                    }
                    else
                    {
                        //log.Debug("-isRegistry()");
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
                //_log.Debug("+isValueNameInRegistry()");
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
                        //_log.Debug("-isValueNameInRegistry()");
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

        //Deletes all the Keys and Values of AnalogueDataLift Key
        public void DeleteKey()
        {
            try
            {
                _log.Debug("+DeleteKey()");
                // Setting
                RegistryKey rk = BASE_REGISTRY_KEY;
                RegistryKey sk1 = rk.CreateSubKey(REGISTRY_SUBKEY_URL);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 != null)
                {
                    sk1.DeleteValue(LAST_RUN_DATE);
                    sk1.DeleteValue(REMOTE_DATABASE_NAME);
                    sk1.DeleteValue(LOCAL_CONNECTION_STRING);
                    sk1.DeleteValue(WEBSERVICE_URL);
                    sk1.DeleteValue(POLL_INTERVAL);
                }
            }
            catch (Exception e)
            {
                _log.Debug("-DeleteKey() Error: " + e.Message);
            }
            _log.Debug("-DeleteKey()");
        }

    }
}
