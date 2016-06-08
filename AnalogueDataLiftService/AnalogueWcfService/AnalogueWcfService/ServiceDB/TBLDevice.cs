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
    
    public partial class TBLDevice
    {
        public string SerialNumber { get; set; }
        public int DeviceNo { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> DeviceID { get; set; }
        public Nullable<System.DateTime> DateRegistered { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string DeviceName { get; set; }
        public string DeviceRFAddress { get; set; }
        public Nullable<int> DeviceSize { get; set; }
        public Nullable<int> DeviceSizeTotal { get; set; }
        public Nullable<int> DeviceSubType { get; set; }
        public Nullable<int> HighLimit { get; set; }
        public Nullable<int> LowLimit { get; set; }
        public Nullable<int> DeviceHighThreshold { get; set; }
        public Nullable<byte> OverrideHighThreshhold { get; set; }
        public Nullable<int> DeviceLowThreshold { get; set; }
        public Nullable<byte> OverrideLowThreshold { get; set; }
        public Nullable<int> Differential { get; set; }
        public Nullable<short> DiffPolarity { get; set; }
        public Nullable<byte> OverrideDifferential { get; set; }
        public Nullable<byte> DeviceDrivenInt { get; set; }
        public Nullable<byte> NoDataInt { get; set; }
        public Nullable<byte> ThresholdForCollection { get; set; }
        public Nullable<int> ErrorRepeatDelay { get; set; }
        public Nullable<byte> OverrideErrorRepeatDelay { get; set; }
        public Nullable<byte> DisableInterruptForThisDevice { get; set; }
        public Nullable<short> NoDataInterruptSamples { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> BottomOutlet { get; set; }
        public Nullable<double> CapacityOfAdjustment { get; set; }
        public Nullable<int> DeviceCount { get; set; }
        public Nullable<double> SensorOffset { get; set; }
        public Nullable<byte> UpdateDevice { get; set; }
        public Nullable<byte> RegisterDevice { get; set; }
        public Nullable<byte> MarkedForDeletion { get; set; }
        public Nullable<int> DeviceLinkTableID { get; set; }
        public string Identifier { get; set; }
        public Nullable<int> AlertTypeH { get; set; }
        public Nullable<int> AlertTypeL { get; set; }
        public Nullable<int> AlertTypeD { get; set; }
        public Nullable<int> AlertTypeI { get; set; }
        public Nullable<int> AlertTypeN { get; set; }
        public string AlertDesc { get; set; }
        public Nullable<int> UnitMeasurement { get; set; }
        public Nullable<short> DTEStatus { get; set; }
        public Nullable<short> DeviceApplication { get; set; }
        public Nullable<byte> OverrideDDEM { get; set; }
        public Nullable<int> DwellingFloors { get; set; }
        public Nullable<int> DwellingUnits { get; set; }
        public Nullable<int> DwellingRooms { get; set; }
        public Nullable<double> DwellingArea { get; set; }
        public Nullable<int> SubstanceID { get; set; }
        public Nullable<int> EquipmentID { get; set; }
        public Nullable<bool> Waste { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }
    }
}