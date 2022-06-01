using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface IDiscountRepository
    {
        public IQueryable<Discount> GetAll { get; }

        Task<Discount> GetSingleAsync(int id);
        Task<Discount> AddAsync(Discount discount, IEnumerable<int> selectedIds);
        Task<Discount> UpdateAsync(Discount discount);

        Task<Tuple<Discount, List<Product>>> GetSpecific(int discountId);
    }
}
