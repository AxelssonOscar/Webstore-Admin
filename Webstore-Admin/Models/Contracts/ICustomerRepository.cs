using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Search(string name, string city, string address);
        IQueryable<Customer> GetSingle(int? customerId);
        Task<Tuple<Customer, string>> GetSingleAndDistanceAsync(int id);

    }
}