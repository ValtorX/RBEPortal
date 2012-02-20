using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Core;
using Core.Schema;

namespace RBEPortalServer {
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class GlobalParameterService {
        [DataContract]
        public class ParameterValue {
            [DataMember]
            public string SVal01 { get; set; }
            [DataMember]
            public double? NVal01 { get; set; }
            [DataMember]
            public DateTime? DVal01 { get; set; }
        }

        [OperationContract]
        [WebGet(
            UriTemplate = "?parameterName={parameterName}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        public Response<ParameterValue> Get(string parameterName) {
            var response = new Response<ParameterValue>();
            try {
                //return new ParameterValue {
                //    SVal01 = "123",
                //    NVal01 = 123,
                //};
                using (var context = new RBEPortalContext()) {
                    var param = context.Parameter.RawTryGet(parameterName + ".Global");
                    if (param != null) {
                        response.Status = "200";
                        response.Data = new ParameterValue {
                            SVal01 = param.SVal01,
                            NVal01 = param.NVal01,
                            DVal01 = param.DVal01,
                        };
                    } else
                        response.Status = "401";
                }
            } catch (Exception exception) {
                response.Status = "400";
                response.StatusInfo = exception.ToString();
            }
            return response;
        }

        //// TODO: Implement the collection resource that will contain the SampleItem instances

        //[OperationContract]
        //[WebGet(UriTemplate = "")]
        //public List<SampleItem> GetCollection() {
        //    // TODO: Replace the current implementation to return a collection of SampleItem instances
        //    return new List<SampleItem>() { new SampleItem() { Id = 1, StringValue = "Hello" } };
        //}

        //[OperationContract]
        //[WebInvoke(UriTemplate = "", Method = "POST")]
        //public SampleItem Create(SampleItem instance) {
        //    // TODO: Add the new instance of SampleItem to the collection
        //    throw new NotImplementedException();
        //}

        //[OperationContract]
        //[WebGet(UriTemplate = "{id}")]
        //public SampleItem Get(string id) {
        //    // TODO: Return the instance of SampleItem with the given id
        //    throw new NotImplementedException();
        //}

        //[OperationContract]
        //[WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        //public SampleItem Update(string id, SampleItem instance) {
        //    // TODO: Update the given instance of SampleItem in the collection
        //    throw new NotImplementedException();
        //}

        //[OperationContract]
        //[WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        //public void Delete(string id) {
        //    // TODO: Remove the instance of SampleItem with the given id from the collection
        //    throw new NotImplementedException();
        //}

    }
}
