using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(int id);
    }
}
