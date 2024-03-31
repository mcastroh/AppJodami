using System.Linq.Expressions;

namespace Jodami.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> ConsultarById(Expression<Func<T, bool>> filtro);

        Task<IQueryable<T>> ConsultarAll(Expression<Func<T, bool>> filtro = null);

        Task<T> Crear(T entidad);

        Task<bool> Editar(T entidad);

        Task<bool> Eliminar(T entidad); 

    }
}
