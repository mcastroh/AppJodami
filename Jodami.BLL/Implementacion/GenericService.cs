using Jodami.BLL.Interfaces;
using Jodami.DAL.Interfaces;
using System.Linq.Expressions;

namespace Jodami.BLL.Implementacion
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<T> GetById(Expression<Func<T, bool>> filtro)
        {
            return await _repository.GetById(filtro);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filtro = null)
        {
            return await _repository.GetByFilter(filtro);
        }

        public async Task<T> Insert(T entity)
        {
            return await _repository.Insert(entity);
        }

        public async Task<bool> Update(T entity)
        {
            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> filtro)
        {
            return await _repository.Delete(filtro);
        }

    }
}