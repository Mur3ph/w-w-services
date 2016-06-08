using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Domain
{
    class Reading
    {
        public Reading() { }

        public int? CompanyID { get; set; }
        public int? GroupID { get; set; }
        public string GroupName { get; set; }
        public string CustTypeID { get; set; }
        public string CustTypeName { get; set; }
        public short? DeviceNo { get; set; }
        public int? DeviceID { get; set; }
        public string Serial { get; set; }
        public DateTime? DateReceived { get; set; }
        public int? AuxAnalogue { get; set; }
        public int? DeviceLevel { get; set; }
        public int? UsableLevel { get; set; }
        public int? MeasuredLevel { get; set; }
        public int? RecordType { get; set; }
        public int? ErrorCondition { get; set; }
        public int? ErrorConditionLink { get; set; }
        public string ErrorText { get; set; }
        public double? AvgUsage { get; set; }
        public double? DaysToEmpty { get; set; }
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
        public int? DeviceSize { get; set; }
        public int? DeviceSizeTotal { get; set; }
        public int? DeviceCount { get; set; }
        public string Identifier { get; set; }
        public int? SubstanceID { get; set; }
        public string SubstanceName { get; set; }
    }
}
