﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace V5.Portal.SmsClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SmsClient.ISms")]
    public interface ISms {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISms/SmsSend", ReplyAction="http://tempuri.org/ISms/SmsSendResponse")]
        string SmsSend(string[] MobileTels, string SmsContent, string Type);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISmsChannel : V5.Portal.SmsClient.ISms, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SmsClient : System.ServiceModel.ClientBase<V5.Portal.SmsClient.ISms>, V5.Portal.SmsClient.ISms {
        
        public SmsClient() {
        }
        
        public SmsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SmsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string SmsSend(string[] MobileTels, string SmsContent, string Type) {
            return base.Channel.SmsSend(MobileTels, SmsContent, Type);
        }
    }
}