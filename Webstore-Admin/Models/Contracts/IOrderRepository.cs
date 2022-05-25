using System.Collections.Generic;

namespace Webstore_Admin.Models.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders { get; }
    }
}
