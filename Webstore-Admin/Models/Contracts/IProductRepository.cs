using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetSingleAsync(int id);

        IEnumerable<Product> GetAll { get; }


    }
}
