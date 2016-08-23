using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFDataTransferObjects.Customers;
using Entity=WcfService.Library.EDMX;

namespace WcfService.Library.DAL.Customer.Interface
{
    public interface ICustomerService
    {
        IList<CustomerDTO> GetCustomers();
    }
}
