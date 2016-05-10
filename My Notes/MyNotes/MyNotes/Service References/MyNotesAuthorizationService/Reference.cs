﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyNotes.MyNotesAuthorizationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyNotesAuthorizationService.IAuthorization")]
    public interface IAuthorization {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthorization/SignIn", ReplyAction="http://tempuri.org/IAuthorization/SignInResponse")]
        int SignIn(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthorization/SignIn", ReplyAction="http://tempuri.org/IAuthorization/SignInResponse")]
        System.Threading.Tasks.Task<int> SignInAsync(string email, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthorizationChannel : MyNotes.MyNotesAuthorizationService.IAuthorization, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthorizationClient : System.ServiceModel.ClientBase<MyNotes.MyNotesAuthorizationService.IAuthorization>, MyNotes.MyNotesAuthorizationService.IAuthorization {
        
        public AuthorizationClient() {
        }
        
        public AuthorizationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthorizationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthorizationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthorizationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int SignIn(string email, string password) {
            return base.Channel.SignIn(email, password);
        }
        
        public System.Threading.Tasks.Task<int> SignInAsync(string email, string password) {
            return base.Channel.SignInAsync(email, password);
        }
    }
}