using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetOrders =>
            _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product);

        public IQueryable<Order> GetAll =>
            _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product);

        public Order GetSingle(int id)
        {
            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefault(o => o.Id == id);
        }
    }
}
