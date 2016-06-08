//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnalogueWcfService.ServiceDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLReading
    {
        public int UnitRequestID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> GroupID { get; set; }
        public string GroupName { get; set; }
        public string CustTypeID { get; set; }
        public string CustTypeName { get; set; }
        public Nullable<short> DeviceNo { get; set; }
        public Nullable<int> DeviceID { get; set; }
        public string DeviceRFAddress { get; set; }
        public string Serial { get; set; }
        public Nullable<System.DateTime> DateReceived { get; set; }
        public Nullable<System.DateTime> NextDialUpDate { get; set; }
        public Nullable<byte> ReceiveError { get; set; }
        public Nullable<byte> bool0 { get; set; }
        public Nullable<byte> bool1 { get; set; }
        public Nullable<byte> bool2 { get; set; }
        public Nullable<byte> bool3 { get; set; }
        public Nullable<byte> EMD { get; set; }
        public Nullable<byte> NDEM { get; set; }
        public Nullable<byte> ThreshBrokenInt { get; set; }
        public Nullable<int> Battery { get; set; }
        public Nullable<byte> teach { get; set; }
        public Nullable<int> AuxAnalogue { get; set; }
        public Nullable<int> DeviceLevel { get; set; }
        public Nullable<int> UsableLevel { get; set; }
        public Nullable<int> UnusableLevel { get; set; }
        public Nullable<int> MeasuredLevel { get; set; }
        public Nullable<byte> dataflag { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public Nullable<System.DateTime> DateCollected { get; set; }
        public Nullable<byte> CollectedByOilManagerSW { get; set; }
        public Nullable<int> RecordType { get; set; }
        public Nullable<int> ErrorCondition { get; set; }
        public Nullable<int> ErrorConditionLink { get; set; }
        public string ErrorText { get; set; }
        public Nullable<short> ReadingStatus { get; set; }
        public string ReadingReaction { get; set; }
        public string AlertReaction { get; set; }
        public Nullable<int> AlertStatus { get; set; }
        public Nullable<double> AvgUsage { get; set; }
        public Nullable<double> NumDaysInAvg { get; set; }
        public Nullable<int> NumMeasInAvg { get; set; }
        public Nullable<double> DaysToEmpty { get; set; }
        public Nullable<int> PriorLevel { get; set; }
        public Nullable<int> PriorMeasuredLevel { get; set; }
        public string NotifiedPerson { get; set; }
        public string AccountNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string HPhone { get; set; }
        public string WPhone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<int> DeviceSize { get; set; }
        public Nullable<int> DeviceSizeTotal { get; set; }
        public Nullable<int> DeviceCount { get; set; }
        public Nullable<int> DeviceSubType { get; set; }
        public Nullable<int> HighLimit { get; set; }
        public Nullable<int> LowLimit { get; set; }
        public Nullable<int> Differential { get; set; }
        public string Identifier { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string Loc_Address1 { get; set; }
        public string Loc_City { get; set; }
        public string Loc_State { get; set; }
        public Nullable<int> ModemType { get; set; }
        public Nullable<int> DwellingFloors { get; set; }
        public Nullable<int> DwellingUnits { get; set; }
        public Nullable<int> DwellingRooms { get; set; }
        public Nullable<double> DwellingArea { get; set; }
        public Nullable<int> SubstanceID { get; set; }
        public string SubstanceName { get; set; }
        public Nullable<int> EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public Nullable<int> ValueBeforeDiff { get; set; }
    }
}