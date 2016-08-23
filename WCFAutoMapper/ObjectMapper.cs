using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WCFDataTransferObjects.Customers;
using WcfService.Library.EDMX;

namespace WCFAutoMapper
{
    public class ObjectMapper : IObjectMapper
    {
        public void CreateMap()
        {
            MapObjectForCustomers();
        }

        private void MapObjectForCustomers()
        {
            Mapper.CreateMap<CustomerDTO,Customer>();
            Mapper.CreateMap<Customer, CustomerDTO>();

        }
       
    }
}
