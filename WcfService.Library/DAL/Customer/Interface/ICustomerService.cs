using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity=WcfService.Library.Entities;

namespace WcfService.Library.DAL.Customer.Interface
{
    public interface ICustomerService
    {
        IQueryable<Entity.Employee> GetCustomers();
    }
}
