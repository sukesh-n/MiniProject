using QuizzApp.Models;

namespace QuizzApp.Interfaces
{
    public interface ICategoryRepository : IRepository<int,Category>
    {
        public Task<List<int>> GetCategoryId(string MainCategory, string SubCategory);
    }
}
