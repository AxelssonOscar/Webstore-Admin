using System.Collections.Generic;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories =>
            _context.Categories;
    }
}
