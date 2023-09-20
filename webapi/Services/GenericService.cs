using webapi.Interfaces;
using webapi.Models.Generics;

namespace webapi.Services
{
    public class GenericService<T> : IGenericService<T> where T : class, ICharacterAssociable<T>
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();

        public Task<T> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task AddAsync(T entity) => _repository.AddAsync(entity);

        public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
