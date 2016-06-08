
using AnalogueWcfService.Model;
using AnalogueWcfService.ServiceDB;
using AnalogueWindowService.Main.Log;
using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace AnalogueWcfService
{
    public class AnalogueService : IAnalogueService
    {
        private static readonly Logger _log = new Logger(new AnalogueService().GetType());

        [WebInvoke(UriTemplate = "sendReadingData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public bool sendReadingData(AnalogReadingJson JsonReading)
        {
            _log.Info("+sendReadingData()");
            SqlConnection connection = null;
            string SQLInsert;
            int numberofRowsInserted = 0;
            try
            {
                using (connection = new SqlConnection(concatenateConnectionString(JsonReading.DatabaseName, getWebConfigConnectionString())))
                {
                    connection.Open();
                    foreach (var reading in JsonReading.Reading)
                    {
                        //Count the number of rows that are inserted into table
                        numberofRowsInserted++;

                        var dataReceived = ((DateTime)reading.DateReceived).ToString("yyyy-MM-dd HH:mm:ss tt");

                        _log.Info("sendReadingData() After Parsing date: (yyyy-MM-dd HH:mm:ss tt) " + dataReceived);
                        SQLInsert = insertIntoAnalogueReadingsTable(reading, dataReceived);

                        using (SqlCommand cmd = new SqlCommand(SQLInsert, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
                _log.Info("sendReadingData() Number of Analogue Readings Inserted: " + numberofRowsInserted);
            }
            catch (SqlException e)
            {
                _log.Error("-sendDeviceData() - Message: " + e.Message + "\nInnerException: " + e.InnerException);
                return false;
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
            _log.Info("-sendReadingData()");
            return true;
        }

        [WebInvoke(UriTemplate = "sendDeviceData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public bool sendDeviceData(AnalogDeviceJson JsonDevice)
        {
            _log.Info("+sendDeviceData()");
            SqlConnection connection = null;
            string SQLUpdateOrInsert="";
            int numberofRowsInserted = 0;
            SqlCommand cmd = null;
            try
            {
                using (connection = new SqlConnection(concatenateConnectionString(JsonDevice.DatabaseName, getWebConfigConnectionString())))
                {
                    connection.Open();
                    foreach (var device in JsonDevice.Device)
                    {
                        //Count the number of rows that are inserted into table
                        numberofRowsInserted++;

                        //SELECT statement to check if the Device exists in the database. (In case a new Device was added on the client machine)
                        int doesDeviceExist = isDeviceExists(device, cmd, connection);

                        //Change format of date before inserting/updating..
                        var dateUpdated = ((DateTime)device.DateUpdated).ToString("yyyy-MM-dd HH:mm:ss tt");
                        var dataRegistered = ((DateTime)device.DateRegistered).ToString("yyyy-MM-dd HH:mm:ss tt");
                        _log.Info("sendDeviceData() After Parsing date: (yyyy-MM-dd HH:mm:ss tt) Date Updated --> " + dateUpdated + " & Date Registered --> " + dataRegistered);
                        
                        //If the device exists in the database, Update..
                        if (doesDeviceExist > 0)
                        {
                            _log.Info("-sendDeviceData() Update statement executed.");
                            //If the Device already exists in the table, UPDATE Device data in TBLDevices with changes made on Client machine
                            SQLUpdateOrInsert = updateDeviceTable(device, dateUpdated, dataRegistered);
                        }
                        else
                        {
                            _log.Info("-sendDeviceData() Insert statement executed.");
                            //If the Device does NOT exist in the table, INSERT new Device into TBLDevices
                            SQLUpdateOrInsert = insertIntoDeviceTable(device, dateUpdated, dataRegistered);
                        }
                        using (cmd = new SqlCommand(SQLUpdateOrInsert, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
                _log.Info("sendDeviceData() Number of Analogue Devices Updated: " + numberofRowsInserted);
            }
            catch (SqlException e)
            {
                _log.Error("Exception: -sendDeviceData() - Message: " + e.Message + "\nInnerException: " + e.InnerException);
                return false;
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
            _log.Info("-sendDeviceData() ");
            return true;
        }

        private static string getWebConfigConnectionString()
        {
            try
            {
                _log.Info("+webConfigConnectionString()" + System.Configuration.ConfigurationManager.ConnectionStrings["ServiceDBEntities"].ConnectionString.ToString());
                return System.Configuration.ConfigurationManager.ConnectionStrings["ServiceDBEntities"].ConnectionString.ToString();
            }
            catch (Exception e)
            {
                _log.Error("-webConfigConnectionString() Message: " + e.Message + "\nInnerException: " + e.InnerException);
                throw e;
            }
        }

        private static string cutAndConcatenateDatabaseDataSourceFromConnectionString(string input)
        {
            _log.Info("+cutAndConcatenateDatabaseDataSourceFromConnectionString()" + input.Split(new string[] { "source=" }, StringSplitOptions.None)[1].Split(';')[0].Trim().ToString());
            return input.Split(new string[] { "source=" }, StringSplitOptions.None)[1]
                .Split(';')[0]
                .Trim().ToString();
        }

        private static string cutDatabaseUserIdFromConnectionString(string input)
        {
            _log.Info("+cutDatabaseUserIdFromConnectionString()" + input.Split(new string[] { "id=" }, StringSplitOptions.None)[1].Split(';')[0].Trim().ToString());
            return input.Split(new string[] { "id=" }, StringSplitOptions.None)[1]
                .Split(';')[0]
                .Trim().ToString();
        }

        private static string cutDatabaseUserPasswordFromConnectionString(string input)
        {
            _log.Info("+cutDatabaseUserPasswordFromConnectionString()" + input.Split(new string[] { "password=" }, StringSplitOptions.None)[1].Split(';')[0].Trim().ToString());
            return input.Split(new string[] { "password=" }, StringSplitOptions.None)[1]
                .Split(';')[0]
                .Trim().ToString();
        }

        private static string concatenateConnectionString(string databaseName, string input)
        {
            _log.Info("+concatenateConnectionString()" + "data source=" + cutAndConcatenateDatabaseDataSourceFromConnectionString(input) + "; initial catalog=" + databaseName + "; integrated security=False; User ID=" + cutDatabaseUserIdFromConnectionString(input) + "; Password=" + cutDatabaseUserPasswordFromConnectionString(input) + ";");
            return @"data source=" + cutAndConcatenateDatabaseDataSourceFromConnectionString(input) + "; initial catalog=" + databaseName + "; integrated security=False; User ID=" + cutDatabaseUserIdFromConnectionString(input) + "; Password=" + cutDatabaseUserPasswordFromConnectionString(input) + ";";
        }

        //Insert Analogue readings into the new table
        private string insertIntoAnalogueReadingsTable(AnalogReading reading, string dataReceived)
        {
            _log.Info("insertIntoAnalogueReadingsTable() After Parsing date: (yyyy-MM-dd HH:mm:ss tt) " + dataReceived);
            return @" INSERT INTO AnalogReadings(CompanyID, GroupID, GroupName, CustTypeID, CustTypeName, DeviceNo, DeviceID, Serial, " +
                                                               "DateReceived, AuxAnalogue, DeviceLevel, UsableLevel, MeasuredLevel, RecordType, ErrorCondition, ErrorConditionLink, " +
                                                               "ErrorText, AvgUsage, DaysToEmpty, AccountNumber, LastName, FirstName, Address1, Address2, " +
                                                               "City, State, ZipCode, Country, HPhone, WPhone, Mobile, Email, " +
                                                               "DeviceSize, DeviceSizeTotal, DeviceCount, Identifier, SubstanceID, SubstanceName" + ")" +
                    " VALUES(" + reading.CompanyID + "," + reading.GroupID + ",'" + reading.GroupName + "','" + reading.CustTypeID + "','" +
                            reading.CustTypeName + "'," + reading.DeviceNo + "," + reading.DeviceID + ",'" + reading.Serial + "','" +
                            dataReceived + "'," + reading.AuxAnalogue + "," + reading.DeviceLevel + "," + reading.UsableLevel + "," +
                            reading.MeasuredLevel + "," + reading.RecordType + "," + reading.ErrorCondition + "," + reading.ErrorConditionLink + ",'" +
                            reading.ErrorText + "'," + reading.AvgUsage + "," + reading.DaysToEmpty + ",'" + reading.AccountNumber + "','" +
                            reading.LastName + "','" + reading.FirstName + "','" + reading.Address1 + "','" + reading.Address2 + "','" +
                            reading.City + "','" + reading.State + "','" + reading.ZipCode + "','" + reading.Country + "','" +
                            reading.HPhone + "','" + reading.WPhone + "','" + reading.Mobile + "','" + reading.Email + "'," +
                            reading.DeviceSize + "," + reading.DeviceSizeTotal + "," + reading.DeviceCount + ",'" + reading.Identifier + "'," +
                            reading.SubstanceID + ",'" + reading.SubstanceName + "')";
        }

        //SELECT statement to check if the Device exists in the database. (In case a new Device was added on the client machine)
        private static int isDeviceExists(TBLDevice device, SqlCommand cmd, SqlConnection connection)
        {
            _log.Info("+isDeviceExists() Select statement executed.");
            int doesDeviceExist = 0;
            string SQLSelect = @" SELECT COUNT(*) " +
                                " FROM TBLDevices " +
                                " WHERE SerialNumber = '" + device.SerialNumber + "'" +
                                " AND DeviceNo = " + device.DeviceNo;
            using (cmd = new SqlCommand(SQLSelect, connection))
            {
                doesDeviceExist = (int)cmd.ExecuteScalar();
            }
            _log.Info("-isDeviceExists() Select statement executed.");
            return doesDeviceExist;
        }

        //If the Device already exists in the table, UPDATE Device data in TBLDevices with changes made on Client machine
        private static string updateDeviceTable(TBLDevice device, string dateUpdated, string dataRegistered)
        {
            _log.Info("+updateDeviceTable() Update statement executed.");
            return @" UPDATE TBLDevices 
                      SET CompanyID=" + device.CompanyID + ", DeviceID=" + device.DeviceID + ", DateRegistered='" + dataRegistered + "', DateUpdated='" + dateUpdated +
                            "', DeviceName='" + device.DeviceName + "', DeviceRFAddress='" + device.DeviceRFAddress + "', DeviceSize=" + device.DeviceSize + ", DeviceSizeTotal=" + device.DeviceSizeTotal +
                            ", DeviceSubType=" + device.DeviceSubType + ", HighLimit=" + device.HighLimit + ", LowLimit=" + device.LowLimit + ", DeviceHighThreshold=" + device.DeviceHighThreshold +
                            ", OverrideHighThreshhold=" + device.OverrideHighThreshhold + ", DeviceLowThreshold=" + device.DeviceLowThreshold + ", OverrideLowThreshold=" + device.OverrideLowThreshold + ", Differential=" + device.Differential +
                            ", DiffPolarity=" + device.DiffPolarity + ", OverrideDifferential=" + device.OverrideDifferential + ", DeviceDrivenInt=" + device.DeviceDrivenInt + ", NoDataInt=" + device.NoDataInt +
                            ", ThresholdForCollection=" + device.ThresholdForCollection + ", ErrorRepeatDelay=" + device.ErrorRepeatDelay + ", OverrideErrorRepeatDelay=" + device.OverrideErrorRepeatDelay + ", DisableInterruptForThisDevice=" + device.DisableInterruptForThisDevice +
                            ", NoDataInterruptSamples=" + device.NoDataInterruptSamples + ", Height=" + device.Height + ", Length=" + device.Length + ", Width=" + device.Width +
                            ", BottomOutlet=" + device.BottomOutlet + ", CapacityOfAdjustment=" + device.CapacityOfAdjustment + ", DeviceCount=" + device.DeviceCount + ", SensorOffset=" + device.SensorOffset +
                            ", UpdateDevice=" + device.UpdateDevice + ", RegisterDevice=" + device.RegisterDevice + ", MarkedForDeletion=" + device.MarkedForDeletion + ", DeviceLinkTableID=" + device.DeviceLinkTableID +
                            ", Identifier='" + device.Identifier + "', AlertTypeH=" + device.AlertTypeH + ", AlertTypeL=" + device.AlertTypeL + ", AlertTypeD=" + device.AlertTypeD +
                            ", AlertTypeI=" + device.AlertTypeI + ", AlertTypeN=" + device.AlertTypeN + ", AlertDesc='" + device.AlertDesc + "', UnitMeasurement=" + device.UnitMeasurement +
                            ", DTEStatus=" + device.DTEStatus + ", DeviceApplication=" + device.DeviceApplication + ", OverrideDDEM=" + device.OverrideDDEM + ", DwellingFloors=" + device.DwellingFloors +
                            ", DwellingUnits=" + device.DwellingUnits + ", DwellingRooms=" + device.DwellingRooms + ", DwellingArea=" + device.DwellingArea + ", SubstanceID=" + device.SubstanceID +
                            ", EquipmentID=" + device.EquipmentID +
                    "  WHERE SerialNumber='" + device.SerialNumber +
                    "' AND DeviceNo=" + device.DeviceNo;
        }

        //If the Device does NOT exist in the table, INSERT new Device into TBLDevices
        private string insertIntoDeviceTable(TBLDevice device, string dateUpdated, string dataRegistered)
        {
            _log.Info("+insertIntoDeviceTable() Insert statement executed.");
            return @" INSERT INTO TBLDevices(  SerialNumber, DeviceNo, CompanyID, DeviceID, DateRegistered, DateUpdated, DeviceName,
                                                DeviceRFAddress, DeviceSize, DeviceSizeTotal, DeviceSubType, HighLimit, LowLimit,
                                                DeviceHighThreshold, OverrideHighThreshhold, DeviceLowThreshold, OverrideLowThreshold, Differential, DiffPolarity,
                                                OverrideDifferential, DeviceDrivenInt, NoDataInt, ThresholdForCollection, ErrorRepeatDelay, OverrideErrorRepeatDelay,
                                                DisableInterruptForThisDevice, NoDataInterruptSamples, Height, Length, Width, BottomOutlet,
                                                CapacityOfAdjustment, DeviceCount, SensorOffset, UpdateDevice, RegisterDevice, MarkedForDeletion,
                                                DeviceLinkTableID, Identifier, AlertTypeH, AlertTypeL, AlertTypeD, AlertTypeI,
                                                AlertTypeN, AlertDesc, UnitMeasurement, DTEStatus, DeviceApplication, OverrideDDEM,
                                                DwellingFloors, DwellingUnits, DwellingRooms, DwellingArea, SubstanceID, EquipmentID) " +
                    " VALUES('" + device.SerialNumber + "', " + device.DeviceNo + ", " + device.CompanyID + ", " + device.DeviceID + ", '" +
                                    dataRegistered + "', '" + dateUpdated + "', '" + device.DeviceName + "', '" + device.DeviceRFAddress + "', " +
                                    device.DeviceSize + ", " + device.DeviceSizeTotal + ", " + device.DeviceSubType + ", " + device.HighLimit + ", " +
                                    device.LowLimit + ", " + device.DeviceHighThreshold + ", " + device.OverrideHighThreshhold + ", " + device.DeviceLowThreshold + ", " +
                                    device.OverrideLowThreshold + ", " + device.Differential + ", " + device.DiffPolarity + ", " + device.OverrideDifferential + ", " +
                                    device.DeviceDrivenInt + ", " + device.NoDataInt + ", " + device.ThresholdForCollection + ", " + device.ErrorRepeatDelay + ", " +
                                    device.OverrideErrorRepeatDelay + ", " + device.DisableInterruptForThisDevice + ", " + device.NoDataInterruptSamples + ", " + device.Height + ", " +
                                    device.Length + ", " + device.Width + ", " + device.BottomOutlet + ", " + device.CapacityOfAdjustment + ", " +
                                    device.DeviceCount + ", " + device.SensorOffset + ", " + device.UpdateDevice + ", " + device.RegisterDevice + ", " +
                                    device.MarkedForDeletion + ", " + device.DeviceLinkTableID + ", '" + device.Identifier + "', " + device.AlertTypeH + ", " +
                                    device.AlertTypeL + ", " + device.AlertTypeD + ", " + device.AlertTypeI + ", " + device.AlertTypeN + ", '" +
                                    device.AlertDesc + "', " + device.UnitMeasurement + ", " + device.DTEStatus + ", " + device.DeviceApplication + ", " +
                                    device.OverrideDDEM + ", " + device.DwellingFloors + ", " + device.DwellingUnits + ", " + device.DwellingRooms + ", " +
                                    device.DwellingArea + ", " + device.SubstanceID + ", " + device.EquipmentID + ")";
        }
    }
}
