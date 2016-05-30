using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceContainer.Models
{
    [DataContract]
    public class Product
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
}
