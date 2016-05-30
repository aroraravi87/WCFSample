using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceContainer.Models;

namespace WcfServiceContainer
{
    public class ShoppingCartService : IShoppingCartService
    {
        public Product GetXMlData(string id)
        {
            Product xml = new Product() { ProductName = "Test Product", ProductID = Convert.ToInt32(id), ProductPrice = "4000.00" };
            return xml; 
        }

        private bool HandleHttpOptionsRequest()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "GET")
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                return true;
            }

            return false;
        }

        public List<Product> GetJsonData(string id)
        {
            if (HandleHttpOptionsRequest())
            {
                List<Product> productList = new List<Product>();
                for (int i = 1; i <= 26; i++)
                {
                    Product objProduct = new Product();
                    Random objRand = new Random();
                    objProduct.ProductID = i;
                    objProduct.ProductName = "product" + i;
                    objProduct.ProductPrice = "$" + (i * objRand.Next(100, 1000));
                    objProduct.ProductUrl = "http://localhost:50679/Images/" + i + ".jpg";
                    objProduct.ProductImage = "~/Images/" + i + ".jpg";
                    productList.Add(objProduct);
                }
                return productList;
            }
            return null;
        }
    }
}
