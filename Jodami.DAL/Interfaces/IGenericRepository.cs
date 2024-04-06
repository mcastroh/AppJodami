using System.Linq.Expressions;

namespace Jodami.DAL.Interfaces
{
    public interface IGenericRepository<T>: IDisposable where T : class
    {
        Task<T> GetById(Expression<Func<T, bool>> filtro);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filtro = null);

        Task<T> Insert(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(Expression<Func<T, bool>> filtro); 

    }
}
