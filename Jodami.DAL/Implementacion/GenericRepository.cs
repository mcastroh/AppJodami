using Jodami.DAL.DBContext;
using Jodami.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jodami.DAL.Implementacion
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbJodamiContext _dBContext;

        public GenericRepository(DbJodamiContext DbJodamiContext)
        {
            _dBContext = DbJodamiContext;
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                T entidad = await _dBContext.Set<T>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Crear(T entidad)
        {
            try
            {
                _dBContext.Set<T>().Add(entidad);
                await _dBContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(T entidad)
        {
            try
            {
                _dBContext.Set<T>().Update(entidad);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(T entidad)
        {
            try
            {
                _dBContext.Set<T>().Remove(entidad);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro)
        {
            IQueryable<T> queryEntidad = filtro == null ? _dBContext.Set<T>() : _dBContext.Set<T>().Where(filtro);
            return queryEntidad;
        }

    }
}