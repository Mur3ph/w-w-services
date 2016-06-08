using AnalogueWcfService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AnalogueWcfService
{
    [ServiceContract]
    public interface IAnalogueService
    {
        [OperationContract(Name = "sendReadingData")]
        [WebInvoke(UriTemplate = "sendReadingData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool sendReadingData(AnalogReadingJson JsonReading);

        [OperationContract(Name = "sendDeviceData")]
        [WebInvoke(UriTemplate = "sendDeviceData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool sendDeviceData(AnalogDeviceJson JsonDevice);
    }
}
