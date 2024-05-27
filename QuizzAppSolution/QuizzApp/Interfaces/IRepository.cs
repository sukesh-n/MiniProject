using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface IRepository<K,T>where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int Key);
        Task<T> GetAsync(K Key);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
