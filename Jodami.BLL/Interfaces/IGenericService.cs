using System.Linq.Expressions;

namespace Jodami.BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetById(Expression<Func<T, bool>> filtro);

        Task<List<T>> GetAll();

        Task<bool> Crear(T entidad);

        Task<bool> Editar(T entidad);

        Task<bool> Eliminar(T entidad);

    }
}