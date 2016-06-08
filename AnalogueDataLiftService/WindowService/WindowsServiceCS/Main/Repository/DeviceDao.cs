using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WindowsServiceCS.Main.Domain;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using WindowsServiceCS.Main.Logging;

namespace WindowsServiceCS.Main.Repository
{
    class DeviceDao : IDeviceDao
    {
        private static readonly Logger _log = new Logger(new DeviceDao().GetType());
        private static int _numberOfRows = 0;
        private static Device _deviceObj;
        private static List<Device> _devices;
        private static DeviceJson JsonDevice; //Naming the Json Object "JsonDevice". Has to be called "JsonDevice" to match parameter in Wcf Web Service method.

        public DeviceDao()
        {
            _devices = new List<Device>();
        }

        //Get Connection string from Registry for this Window Service to connect to database to get data
        //Get all Device data from database, database name for Wcf Web Service from Registry and transform it to Json 
        public string GetDevices(string lastRunDate, string remoteDbName, string connectionString)
        {
            _log.Debug("+GetDevices()");
            SqlConnection connection = null;
            SqlDataReader dataReader = null;
            JavaScriptSerializer jserializer = null;

            string sqlDevices = @"SELECT
	                                   d.SerialNumber,
                                       d.DeviceNo,
                                       d.CompanyID,
                                       d.DeviceID,
                                       d.DateRegistered,
                                       d.DateUpdated,
                                       d.DeviceName,
                                       d.DeviceRFAddress,
                                       d.DeviceSize,
                                       d.DeviceSizeTotal,
                                       d.DeviceSubType,
                                       d.HighLimit,
                                       d.LowLimit,
                                       d.DeviceHighThreshold,
                                       d.OverrideHighThreshhold,
                                       d.DeviceLowThreshold,
                                       d.OverrideLowThreshold,
                                       d.Differential,
                                       d.DiffPolarity,
                                       d.OverrideDifferential,
                                       d.DeviceDrivenInt,
                                       d.NoDataInt,
                                       d.ThresholdForCollection,
                                       d.ErrorRepeatDelay,
                                       d.OverrideErrorRepeatDelay,
                                       d.DisableInterruptForThisDevice,
                                       d.NoDataInterruptSamples,
                                       d.Height,
                                       d.Length,
                                       d.Width,
                                       d.BottomOutlet,
                                       d.CapacityOfAdjustment,
                                       d.DeviceCount,
                                       d.SensorOffset,
                                       d.UpdateDevice,
                                       d.RegisterDevice,
                                       d.MarkedForDeletion,
                                       d.DeviceLinkTableID,
                                       d.Identifier,
                                       d.AlertTypeH,
                                       d.AlertTypeL,
                                       d.AlertTypeD,
                                       d.AlertTypeI,
                                       d.AlertTypeN,
                                       d.AlertDesc,
                                       d.UnitMeasurement,
                                       d.DTEStatus,
                                       d.DeviceApplication,
                                       d.OverrideDDEM,
                                       d.DwellingFloors,
                                       d.DwellingUnits,
                                       d.DwellingRooms,
                                       d.DwellingArea,
                                       d.SubstanceID,
                                       d.EquipmentID
                                  FROM 
	                                    TBLDevices d
                                  WHERE d.DateUpdated > '" + lastRunDate + "'";
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlDevices, connection);
                    connection.Open();
                    using (dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                _numberOfRows++;
                                _deviceObj = new Device();
                                _deviceObj.SerialNumber = SafeDao.SafeGetString(dataReader, 0);
                                _deviceObj.DeviceNo = SafeDao.SafeGetInt(dataReader, 1);
                                _deviceObj.CompanyID = SafeDao.SafeGetInt(dataReader, 2);
                                _deviceObj.DeviceID = SafeDao.SafeGetInt(dataReader, 3);
                                _deviceObj.DateRegistered = SafeDao.SafeGetDateTime(dataReader, 4);
                                _deviceObj.DateUpdated = SafeDao.SafeGetDateTime(dataReader, 5);
                                _deviceObj.DeviceName = SafeDao.SafeGetString(dataReader, 6);
                                _deviceObj.DeviceRFAddress = SafeDao.SafeGetString(dataReader, 7);
                                _deviceObj.DeviceSize = SafeDao.SafeGetInt(dataReader, 8);
                                _deviceObj.DeviceSizeTotal = SafeDao.SafeGetInt(dataReader, 9);
                                _deviceObj.DeviceSubType = SafeDao.SafeGetInt(dataReader, 10);
                                _deviceObj.HighLimit = SafeDao.SafeGetInt(dataReader, 11);
                                _deviceObj.LowLimit = SafeDao.SafeGetInt(dataReader, 12);
                                _deviceObj.DeviceHighThreshold = SafeDao.SafeGetInt(dataReader, 13);
                                _deviceObj.OverrideHighThreshhold = SafeDao.SafeGetByte(dataReader, 14);
                                _deviceObj.DeviceLowThreshold = SafeDao.SafeGetInt(dataReader, 15);
                                _deviceObj.OverrideLowThreshold = SafeDao.SafeGetByte(dataReader, 16);
                                _deviceObj.Differential = SafeDao.SafeGetInt(dataReader, 17);
                                _deviceObj.DiffPolarity = SafeDao.SafeGetShort(dataReader, 18);
                                _deviceObj.OverrideDifferential = SafeDao.SafeGetByte(dataReader, 19);
                                _deviceObj.DeviceDrivenInt = SafeDao.SafeGetByte(dataReader, 20);
                                _deviceObj.NoDataInt = SafeDao.SafeGetByte(dataReader, 21);
                                _deviceObj.ThresholdForCollection = SafeDao.SafeGetByte(dataReader, 22);
                                _deviceObj.ErrorRepeatDelay = SafeDao.SafeGetInt(dataReader, 23);
                                _deviceObj.OverrideErrorRepeatDelay = SafeDao.SafeGetByte(dataReader, 24);
                                _deviceObj.DisableInterruptForThisDevice = SafeDao.SafeGetByte(dataReader, 25);
                                _deviceObj.NoDataInterruptSamples = SafeDao.SafeGetShort(dataReader, 26);
                                _deviceObj.Height = SafeDao.SafeGetDouble(dataReader, 27);
                                _deviceObj.Length = SafeDao.SafeGetDouble(dataReader, 28);
                                _deviceObj.Width = SafeDao.SafeGetDouble(dataReader, 29);
                                _deviceObj.BottomOutlet = SafeDao.SafeGetDouble(dataReader, 30);
                                _deviceObj.CapacityOfAdjustment = SafeDao.SafeGetDouble(dataReader, 31);
                                _deviceObj.DeviceCount = SafeDao.SafeGetInt(dataReader, 32);
                                _deviceObj.SensorOffset = SafeDao.SafeGetDouble(dataReader, 33);
                                _deviceObj.UpdateDevice = SafeDao.SafeGetByte(dataReader, 34);
                                _deviceObj.RegisterDevice = SafeDao.SafeGetByte(dataReader, 35);
                                _deviceObj.MarkedForDeletion = SafeDao.SafeGetByte(dataReader, 36);
                                _deviceObj.DeviceLinkTableID = SafeDao.SafeGetInt(dataReader, 37);
                                _deviceObj.Identifier = SafeDao.SafeGetString(dataReader, 38);
                                _deviceObj.AlertTypeH = SafeDao.SafeGetInt(dataReader, 39);
                                _deviceObj.AlertTypeL = SafeDao.SafeGetInt(dataReader, 40);
                                _deviceObj.AlertTypeD = SafeDao.SafeGetInt(dataReader, 41);
                                _deviceObj.AlertTypeI = SafeDao.SafeGetInt(dataReader, 42);
                                _deviceObj.AlertTypeN = SafeDao.SafeGetInt(dataReader, 43);
                                _deviceObj.AlertDesc = SafeDao.SafeGetString(dataReader, 44);
                                _deviceObj.UnitMeasurement = SafeDao.SafeGetInt(dataReader, 45);
                                _deviceObj.DTEStatus = SafeDao.SafeGetShort(dataReader, 46);
                                _deviceObj.DeviceApplication = SafeDao.SafeGetShort(dataReader, 47);
                                _deviceObj.OverrideDDEM = SafeDao.SafeGetByte(dataReader, 48);
                                _deviceObj.DwellingFloors = SafeDao.SafeGetInt(dataReader, 49);
                                _deviceObj.DwellingUnits = SafeDao.SafeGetInt(dataReader, 50);
                                _deviceObj.DwellingRooms = SafeDao.SafeGetInt(dataReader, 51);
                                _deviceObj.DwellingArea = SafeDao.SafeGetDouble(dataReader, 52);
                                _deviceObj.SubstanceID = SafeDao.SafeGetInt(dataReader, 53);
                                _deviceObj.EquipmentID = SafeDao.SafeGetInt(dataReader, 54);
                                _devices.Add(_deviceObj);
                            }
                        }
                        dataReader.Close();
                    }
                    connection.Close();
                }
                _log.Debug("-GetDevices() Devices from database: " + _numberOfRows + "rows of data!");
                //Set the composite object with our data..
                JsonDevice = new DeviceJson(_devices);
                JsonDevice.DatabaseName = remoteDbName;

                //Set Json object to max possible capasity
                jserializer = new JavaScriptSerializer();
                jserializer.MaxJsonLength = Int32.MaxValue;
            }
            catch (SqlException ex)
            {
                _log.Error("-GetDevices() Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (connection != null) { connection.Close(); }
            }
            _log.Debug("-GetDevices()");
            return jserializer.Serialize(new { JsonDevice }).ToString();
        }
    }
}
