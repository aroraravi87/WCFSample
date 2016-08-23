using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.Library;
using WcfService.Library.DAL.Customer.Interface;
using Entity = WcfService.Library.EDMX;
using WcfService.Unity.UnityBase;
using WcfServiceContainer.Models;
using System.Data.Entity.Validation;
using WCFDataTransferObjects;
using WCFDataTransferObjects.Customers;

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
        public IList<CustomerDTO> GetCustomerDetails()
        {

            try
            {
                if (HandleHttpOptionsRequest())
                {
                    IList<CustomerDTO> CustomerList = CustomUnityContainer.Resolve<ICustomerService>().GetCustomers();
                    return CustomerList.Count > 0 ? CustomerList : null;

                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (InvalidOperationException ex)
            {
                //throw ex;
            }
            catch (Exception ex)
            {
            }


                return null;
        }

        public void Dispose()
        {
           
        }
    }
}
