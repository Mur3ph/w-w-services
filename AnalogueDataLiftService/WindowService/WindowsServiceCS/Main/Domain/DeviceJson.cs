using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WindowsServiceCS.Main.Domain
{
    class DeviceJson
    {
        private static List<Device> _devices;

        public DeviceJson(List<Device> devices)
        {
            _devices = devices;
        }

        public List<Device> Device
        {
            get
            {
                return _devices;
            }
        }

        public string DatabaseName
        {
            get;
            set;
        }
    }
}
