using System.Collections.Generic;
using System.Linq;

namespace Webstore_Admin.Models.Contracts
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll { get; }
        string GetDistance(string city);
        Order GetSingle(int id);
    }
}
