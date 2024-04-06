using Jodami.DAL.DBContext;
using Jodami.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jodami.DAL.Implementacion
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbJodamiContext _dbContext;
        private bool disposed = false;

        public GenericRepository(DbJodamiContext DbJodamiContext)
        {
            _dbContext = DbJodamiContext;
        }

        protected DbSet<T> _dbSet
        {
            get
            {
                return _dbContext.Set<T>();
            }
        }

        public async Task<T> GetById(Expression<Func<T, bool>> filtro)
        {
            return await _dbSet.FirstOrDefaultAsync(filtro);  
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filtro = null)
        {
            return _dbSet.AsNoTracking().Where(filtro);
        }

        public async Task<T> Insert(T entity)
        {
            _dbSet.Add(entity);
            await Save();
            return entity;
        }

        public async Task<bool> Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await Save();
            return true;
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> filtro)
        {
            T entity = await _dbSet.FirstOrDefaultAsync(filtro);
            _dbSet.Remove(entity);
            await Save();
            return true;             
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
              
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _dbContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}