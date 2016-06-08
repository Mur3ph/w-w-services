using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Domain
{
    class ReadingJson
    {
        private static List<Reading> _readings;

        public ReadingJson(List<Reading> readings)
        {
            _readings = readings;
        }

        public List<Reading> Reading
        {
            get
            {
                return _readings;
            }
        }

        public string DatabaseName
        {
            get;
            set;
        }
    }
}
