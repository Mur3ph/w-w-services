﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnalogueWcfService.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IAnalogueService")]
    public interface IAnalogueService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogueService/getJsonFromWindowService", ReplyAction="http://tempuri.org/IAnalogueService/getJsonFromWindowServiceResponse")]
        void getJsonFromWindowService();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogueService/getJsonFromWindowService", ReplyAction="http://tempuri.org/IAnalogueService/getJsonFromWindowServiceResponse")]
        System.Threading.Tasks.Task getJsonFromWindowServiceAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogueService/insertIntoDatabase", ReplyAction="http://tempuri.org/IAnalogueService/insertIntoDatabaseResponse")]
        void insertIntoDatabase();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogueService/insertIntoDatabase", ReplyAction="http://tempuri.org/IAnalogueService/insertIntoDatabaseResponse")]
        System.Threading.Tasks.Task insertIntoDatabaseAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAnalogueServiceChannel : AnalogueWcfService.ServiceReference1.IAnalogueService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AnalogueServiceClient : System.ServiceModel.ClientBase<AnalogueWcfService.ServiceReference1.IAnalogueService>, AnalogueWcfService.ServiceReference1.IAnalogueService {
        
        public AnalogueServiceClient() {
        }
        
        public AnalogueServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AnalogueServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnalogueServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnalogueServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void getJsonFromWindowService() {
            base.Channel.getJsonFromWindowService();
        }
        
        public System.Threading.Tasks.Task getJsonFromWindowServiceAsync() {
            return base.Channel.getJsonFromWindowServiceAsync();
        }
        
        public void insertIntoDatabase() {
            base.Channel.insertIntoDatabase();
        }
        
        public System.Threading.Tasks.Task insertIntoDatabaseAsync() {
            return base.Channel.insertIntoDatabaseAsync();
        }
    }
}
