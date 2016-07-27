using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService.Library.Impl;
using Entity = WcfService.Library.Entities;

namespace WcfService.Library.DAL.Customer.Interface
{
    public class CustomerService:ICustomerService
    {
        public IQueryable<Entity.Employee> GetCustomers()
        {
            IQueryable<Entity.Employee> CustomersList;

            using (var unitWork = new UnitOfWork())
            {
                CustomersList=unitWork.GetRepository<Entity.Employee>().GetList();
            }
            return CustomersList;
        }
    }
}
