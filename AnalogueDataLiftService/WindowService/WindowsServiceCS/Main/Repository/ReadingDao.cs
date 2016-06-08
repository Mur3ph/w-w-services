using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using WindowsServiceCS.Main.Domain;
using WindowsServiceCS.Main.Logging;

namespace WindowsServiceCS.Main.Repository
{
    class ReadingDao : IReadingDao
    {
        private static readonly Logger _log = new Logger(new ReadingDao().GetType());
        private static int _numberOfRows = 0;
        private static Reading _readingObj;
        private static List<Reading> _readingsList;
        private static ReadingJson JsonReading; //Naming the Json Object "JsonReading". Has to be called "JsonReading" to match parameter in Wcf Web Service method

        public ReadingDao()
        {
            _readingsList = new List<Reading>();
        }

        //Get Connection string from Registry for this Window Service to connect to database to get data
        //Get all Readings data from database, database name for Wcf Web Service from Registry and transform it to Json
        public string GetReadings(string lastRunDate, string remoteDbName, string connectionString)
        {
            _log.Debug("+GetReadings()");
            SqlConnection connection = null;
            SqlDataReader dataReader = null;
            JavaScriptSerializer jserializer = null;

            string sqlReadings = @"SELECT 
                                        r.CompanyID,
                                        r.GroupID,
                                        r.GroupName,
                                        r.CustTypeID,
                                        r.CustTypeName,
                                        r.DeviceNo,
                                        r.DeviceID,
                                        r.Serial,
                                        r.DateReceived,
                                        r.AuxAnalogue,
                                        r.DeviceLevel,
                                        r.UsableLevel,
                                        r.MeasuredLevel,
                                        r.RecordType,
                                        r.ErrorCondition,
                                        r.ErrorConditionLink,
                                        r.ErrorText,
                                        r.AvgUsage,
                                        r.DaysToEmpty,
                                        r.AccountNumber,
                                        r.LastName,
                                        r.FirstName,
                                        r.Address1,
                                        r.Address2,
                                        r.City,
                                        r.State,
                                        r.ZipCode,
                                        r.Country,
                                        r.HPhone,
                                        r.WPhone,
                                        r.Mobile,
                                        r.Email,
                                        r.DeviceSize,
                                        r.DeviceSizeTotal,
                                        r.DeviceCount,
                                        r.Identifier,
                                        r.SubstanceID,
                                        r.SubstanceName
                                   FROM 
	                                    TBLReadings r
                                   WHERE DateReceived > '" + lastRunDate + "'";

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlReadings, connection);
                    connection.Open();
                    using (dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                _numberOfRows++;
                                _readingObj = new Reading();
                                _readingObj.CompanyID = SafeDao.SafeGetInt(dataReader, 0);
                                _readingObj.GroupID = SafeDao.SafeGetInt(dataReader, 1);
                                _readingObj.GroupName = SafeDao.SafeGetString(dataReader, 2);
                                _readingObj.CustTypeID = SafeDao.SafeGetString(dataReader, 3);
                                _readingObj.CustTypeName = SafeDao.SafeGetString(dataReader, 4);
                                _readingObj.DeviceNo = SafeDao.SafeGetShort(dataReader, 5);
                                _readingObj.DeviceID = SafeDao.SafeGetInt(dataReader, 6);
                                _readingObj.Serial = SafeDao.SafeGetString(dataReader, 7);
                                _readingObj.DateReceived = SafeDao.SafeGetDateTime(dataReader, 8);
                                _readingObj.AuxAnalogue = SafeDao.SafeGetInt(dataReader, 9);
                                _readingObj.DeviceLevel = SafeDao.SafeGetInt(dataReader, 10);
                                _readingObj.UsableLevel = SafeDao.SafeGetInt(dataReader, 11);
                                _readingObj.MeasuredLevel = SafeDao.SafeGetInt(dataReader, 12);
                                _readingObj.RecordType = SafeDao.SafeGetInt(dataReader, 13);
                                _readingObj.ErrorCondition = SafeDao.SafeGetInt(dataReader, 14);
                                _readingObj.ErrorConditionLink = SafeDao.SafeGetInt(dataReader, 15);
                                _readingObj.ErrorText = SafeDao.SafeGetString(dataReader, 16);
                                _readingObj.AvgUsage = SafeDao.SafeGetDouble(dataReader, 17);
                                _readingObj.DaysToEmpty = SafeDao.SafeGetDouble(dataReader, 18);
                                _readingObj.AccountNumber = SafeDao.SafeGetString(dataReader, 19);
                                _readingObj.LastName = SafeDao.SafeGetString(dataReader, 20);
                                _readingObj.FirstName = SafeDao.SafeGetString(dataReader, 21);
                                _readingObj.Address1 = SafeDao.SafeGetString(dataReader, 22);
                                _readingObj.Address2 = SafeDao.SafeGetString(dataReader, 23);
                                _readingObj.City = SafeDao.SafeGetString(dataReader, 24);
                                _readingObj.State = SafeDao.SafeGetString(dataReader, 25);
                                _readingObj.ZipCode = SafeDao.SafeGetString(dataReader, 26);
                                _readingObj.Country = SafeDao.SafeGetString(dataReader, 27);
                                _readingObj.HPhone = SafeDao.SafeGetString(dataReader, 28);
                                _readingObj.WPhone = SafeDao.SafeGetString(dataReader, 29);
                                _readingObj.Mobile = SafeDao.SafeGetString(dataReader, 30);
                                _readingObj.Email = SafeDao.SafeGetString(dataReader, 31);
                                _readingObj.DeviceSize = SafeDao.SafeGetInt(dataReader, 32);
                                _readingObj.DeviceSizeTotal = SafeDao.SafeGetInt(dataReader, 33);
                                _readingObj.DeviceCount = SafeDao.SafeGetInt(dataReader, 34);
                                _readingObj.Identifier = SafeDao.SafeGetString(dataReader, 35);
                                _readingObj.SubstanceID = SafeDao.SafeGetInt(dataReader, 36);
                                _readingObj.SubstanceName = SafeDao.SafeGetString(dataReader, 37);
                                _readingsList.Add(_readingObj);
                            }
                        }
                        dataReader.Close();
                    }
                    connection.Close();
                }
                _log.Debug("-GetReadings() Readings from database: " + _numberOfRows + "rows of data!");
                //Set the composite object with our data..
                JsonReading = new ReadingJson(_readingsList);
                JsonReading.DatabaseName = remoteDbName;

                jserializer = new JavaScriptSerializer();
                jserializer.MaxJsonLength = Int32.MaxValue;
            }
            catch (SqlException ex)
            {
                _log.Error("-GetReadings() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (connection != null) { connection.Close(); }
            }

            _log.Debug("-GetReadings()");
            return jserializer.Serialize(new { JsonReading }).ToString();
        }
    }
}
