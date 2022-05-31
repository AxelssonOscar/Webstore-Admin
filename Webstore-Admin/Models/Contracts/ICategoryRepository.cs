using System.Collections.Generic;

namespace Webstore_Admin.Models.Contracts
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories { get; }
    }
}
