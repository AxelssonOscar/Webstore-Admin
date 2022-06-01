using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(int id);

        Task<IEnumerable<Product>> GetAllAsync();

        IEnumerable<Product> GetAll { get; }

        Task<Product> GetSingleAsync(int id);


    }
}
