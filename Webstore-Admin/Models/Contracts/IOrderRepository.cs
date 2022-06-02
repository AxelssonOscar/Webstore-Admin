﻿using System.Collections.Generic;
using System.Linq;

namespace Webstore_Admin.Models.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders { get; }
        IQueryable<Order> GetAll { get; }

        string GetDistance(string city);
        public Order GetSingle(int id);

    }
}
