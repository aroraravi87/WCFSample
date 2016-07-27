// -----------------------------------------------------------------------
// <copyright file="OrderRepository.cs" company="Logiciells">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


using WcfService.Library.Entities;
using WcfService.Library.Impl.Interface;

namespace WcfService.Library.Impl
{
    public class OrderRepositoryImpl : GenericRepositoryImpl<Order>
    {
        private readonly IDbContext _context;

        public OrderRepositoryImpl(IDbContext context)
            : base(context)
        {
            _context = context;
        }

        //public int UpdateOrderSequence(long orderId)
        //{
        //    var database = _context.GetDatabase();
        //    //return database.ExecuteSqlCommand("update res_order o left join res_order ord on ord.id=o.id set o.id=ord.id-1 where ord.id > {0}", orderId);
        //    retu
        //}

        //public int MarkItemsAsSendToKitchen(long orderId)
        //{
        //    var database = _context.GetDatabase();
        //    return database.ExecuteSqlCommand("update res_order_child set has_send_to_kitchen = 1 where order_id = {0}", orderId);
        //}
    }
}