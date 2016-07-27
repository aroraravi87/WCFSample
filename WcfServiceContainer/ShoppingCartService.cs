using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.Library;
using WcfService.Library.DAL.Customer.Interface;
using Entity=WcfService.Library.Entities;
using WcfService.Unity.UnityBase;
using WcfServiceContainer.Models;

namespace WcfServiceContainer
{
    public class ShoppingCartService : IShoppingCartService,IDisposable
    {


        public ShoppingCartService()
        {
            CustomUnityContainerExtension.InitializeContainer();
        }

        public ProductModel GetXMlData(string id)
        {
            ProductModel xml = new ProductModel() { ProductName = "Test Product", ProductID = Convert.ToInt32(id), ProductPrice = "4000.00" };
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

        public List<ProductModel> GetJsonData(string id)
        {
            if (HandleHttpOptionsRequest())
            {
                List<ProductModel> productList = new List<ProductModel>();
                for (int i = 1; i <= 26; i++)
                {
                    ProductModel objProduct = new ProductModel();
                    Random objRand = new Random();
                    objProduct.ProductID = i;
                    objProduct.ProductName = "product" + i;
                    objProduct.ProductPrice = "$" + (i * objRand.Next(100, 1000));
                    objProduct.ProductUrl = "http://localhost:8091/" + i + ".jpg";
                    objProduct.ProductImage = "" + i + ".jpg";
                    productList.Add(objProduct);
                }
                return productList;
            }
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Entity.Employee> GetCustomerDetails()
        {
            if (HandleHttpOptionsRequest())
            {
                var result = CustomUnityContainer.Resolve<ICustomerService>().GetCustomers();
                if (result != null && result.Count() > 0)
                    return result.ToList();
            }
            return null;
        }

        public void Dispose()
        {
           
        }
    }
}
