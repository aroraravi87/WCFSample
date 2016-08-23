using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService.Library.Impl;
using WCFDataTransferObjects.Customers;
using Entity = WcfService.Library.EDMX;

namespace WcfService.Library.DAL.Customer.Interface
{
    public class CustomerService : ICustomerService
    {
        public IList<CustomerDTO> GetCustomers()
        {
            var unitWork = new UnitOfWork();
            return unitWork.GetRepository<Entity.Customer>().GetList()
                    .Select(x=>new CustomerDTO()
                    {
                        CompanyName = x.CompanyName,
                        Address = x.Address,
                        ContactName =x.ContactName,
                        Region = x.Region,
                        CustomerID = x.CustomerID,
                        Country = x.Country
                    }).ToList();
        }
    }
}
