using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> Search(string name, string city, string address);

        Task<IEnumerable<Customer>> GetSingle(int customerId);

    }
}