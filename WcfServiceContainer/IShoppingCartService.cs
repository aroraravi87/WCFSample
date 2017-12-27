using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WCFDataTransferObjects.Customers;
using WcfServiceContainer.Models;
using Entity = WcfService.Library.EDMX;

namespace WcfServiceContainer
{
    //// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    //[ServiceContract]
    //public interface IShoppingCartService
    //{
    //    [OperationContract]
    //    string GetData(int value);

    //    [OperationContract]
    //    CompositeType GetDataUsingDataContract(CompositeType composite);

    //    // TODO: Add your service operations here
    //}

    //// Use a data contract as illustrated in the sample below to add composite types to service operations.
    //// You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceContainer.ContractType".
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}

    [ServiceContract]
    public interface IShoppingCartService
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Xml,
                    BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "xml/{id}")]
        ProductModel GetXMlData(string id);


        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "json/{id}")]
        List<ProductModel> GetJsonData(string id);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Wrapped,
                                UriTemplate = "json")]
        IList<CustomerDTO> GetCustomerDetails();

        [OperationContract]
        [FaultContract(typeof(string))]
        AuthReponse GetAuthorInfo(AuthRequest objRequest);

    }

}
