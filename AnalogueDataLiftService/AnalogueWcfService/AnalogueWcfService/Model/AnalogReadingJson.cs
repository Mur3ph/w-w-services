﻿using AnalogueWcfService.ServiceDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AnalogueWcfService.Model
{
    [DataContract]
    public class AnalogReadingJson
    {
        [DataMember]
        public List<AnalogReading> Reading { get; set; }
        [DataMember]
        public string DatabaseName { get; set; }
    }
}