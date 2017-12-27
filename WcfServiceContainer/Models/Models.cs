using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace WcfServiceContainer.Models
{
    [DataContract]
    public partial class ProductModel
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string ProductPrice { get; set; }
        [DataMember]
        public string ProductUrl { get; set; }
        [DataMember]
        public string ProductImage { get; set; }
    }


    [MessageContract]
    public class AuthRequest
    {
        [MessageHeader]
        public string AuthId { get; set; }

    }

    [MessageContract]
    public class AuthReponse
    {
        [MessageBodyMember]
        public Author AuthObject { get; set; }

    }

    [DataContract]
    public class Author
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string ContactNumber { get; set; }
       
    }
}
