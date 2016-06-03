﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ensemble.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.IDBManagerService")]
    public interface IDBManagerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/GetData", ReplyAction="http://tempuri.org/IDBManagerService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/GetData", ReplyAction="http://tempuri.org/IDBManagerService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IDBManagerService/GetDataUsingDataContractResponse")]
        DatabaseService.CompositeType GetDataUsingDataContract(DatabaseService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IDBManagerService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<DatabaseService.CompositeType> GetDataUsingDataContractAsync(DatabaseService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/connect", ReplyAction="http://tempuri.org/IDBManagerService/connectResponse")]
        int connect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/connect", ReplyAction="http://tempuri.org/IDBManagerService/connectResponse")]
        System.Threading.Tasks.Task<int> connectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/disConnect", ReplyAction="http://tempuri.org/IDBManagerService/disConnectResponse")]
        void disConnect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/disConnect", ReplyAction="http://tempuri.org/IDBManagerService/disConnectResponse")]
        System.Threading.Tasks.Task disConnectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/hashPassword", ReplyAction="http://tempuri.org/IDBManagerService/hashPasswordResponse")]
        string hashPassword(string p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/hashPassword", ReplyAction="http://tempuri.org/IDBManagerService/hashPasswordResponse")]
        System.Threading.Tasks.Task<string> hashPasswordAsync(string p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/isUserExist", ReplyAction="http://tempuri.org/IDBManagerService/isUserExistResponse")]
        bool isUserExist(string e);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/isUserExist", ReplyAction="http://tempuri.org/IDBManagerService/isUserExistResponse")]
        System.Threading.Tasks.Task<bool> isUserExistAsync(string e);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/login", ReplyAction="http://tempuri.org/IDBManagerService/loginResponse")]
        string login(string e, string p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBManagerService/login", ReplyAction="http://tempuri.org/IDBManagerService/loginResponse")]
        System.Threading.Tasks.Task<string> loginAsync(string e, string p);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDBManagerServiceChannel : Ensemble.ServiceReference2.IDBManagerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DBManagerServiceClient : System.ServiceModel.ClientBase<Ensemble.ServiceReference2.IDBManagerService>, Ensemble.ServiceReference2.IDBManagerService {
        
        public DBManagerServiceClient() {
        }
        
        public DBManagerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DBManagerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBManagerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBManagerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public DatabaseService.CompositeType GetDataUsingDataContract(DatabaseService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<DatabaseService.CompositeType> GetDataUsingDataContractAsync(DatabaseService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public int connect() {
            return base.Channel.connect();
        }
        
        public System.Threading.Tasks.Task<int> connectAsync() {
            return base.Channel.connectAsync();
        }
        
        public void disConnect() {
            base.Channel.disConnect();
        }
        
        public System.Threading.Tasks.Task disConnectAsync() {
            return base.Channel.disConnectAsync();
        }
        
        public string hashPassword(string p) {
            return base.Channel.hashPassword(p);
        }
        
        public System.Threading.Tasks.Task<string> hashPasswordAsync(string p) {
            return base.Channel.hashPasswordAsync(p);
        }
        
        public bool isUserExist(string e) {
            return base.Channel.isUserExist(e);
        }
        
        public System.Threading.Tasks.Task<bool> isUserExistAsync(string e) {
            return base.Channel.isUserExistAsync(e);
        }
        
        public string login(string e, string p) {
            return base.Channel.login(e, p);
        }
        
        public System.Threading.Tasks.Task<string> loginAsync(string e, string p) {
            return base.Channel.loginAsync(e, p);
        }
    }
}
