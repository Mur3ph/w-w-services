using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Domain
{
    class Device
    {
        public Device() { }

        public string SerialNumber { get; set; }
        public int DeviceNo { get; set; }
        public int? CompanyID { get; set; }
        public int? DeviceID { get; set; }
        public DateTime? DateRegistered { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string DeviceName { get; set; }
        public string DeviceRFAddress { get; set; }
        public int? DeviceSize { get; set; }
        public int? DeviceSizeTotal { get; set; }
        public int? DeviceSubType { get; set; }
        public int? HighLimit { get; set; }
        public int? LowLimit { get; set; }
        public int? DeviceHighThreshold { get; set; }
        public byte? OverrideHighThreshhold { get; set; }
        public int? DeviceLowThreshold { get; set; }
        public byte? OverrideLowThreshold { get; set; }
        public int? Differential { get; set; }
        public short? DiffPolarity { get; set; }
        public byte? OverrideDifferential { get; set; }
        public byte? DeviceDrivenInt { get; set; }
        public byte? NoDataInt { get; set; }
        public byte? ThresholdForCollection { get; set; }
        public int? ErrorRepeatDelay { get; set; }
        public byte? OverrideErrorRepeatDelay { get; set; }
        public byte? DisableInterruptForThisDevice { get; set; }
        public short? NoDataInterruptSamples { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? BottomOutlet { get; set; }
        public double? CapacityOfAdjustment { get; set; }
        public int? DeviceCount { get; set; }
        public double? SensorOffset { get; set; }
        public byte? UpdateDevice { get; set; }
        public byte? RegisterDevice { get; set; }
        public byte? MarkedForDeletion { get; set; }
        public int? DeviceLinkTableID { get; set; }
        public string Identifier { get; set; }
        public int? AlertTypeH { get; set; }
        public int? AlertTypeL { get; set; }
        public int? AlertTypeD { get; set; }
        public int? AlertTypeI { get; set; }
        public int? AlertTypeN { get; set; }
        public string AlertDesc { get; set; }
        public int? UnitMeasurement { get; set; }
        public short? DTEStatus { get; set; }
        public short? DeviceApplication { get; set; }
        public byte? OverrideDDEM { get; set; }
        public int? DwellingFloors { get; set; }
        public int? DwellingUnits { get; set; }
        public int? DwellingRooms { get; set; }
        public double? DwellingArea { get; set; }
        public int? SubstanceID { get; set; }
        public int? EquipmentID { get; set; }
    }
}
