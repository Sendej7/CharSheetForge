using webapi.Models.Generics;

namespace webapi.Interfaces
{
    public interface IGenericRepository<T> where T : class, ICharacterAssociable<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
