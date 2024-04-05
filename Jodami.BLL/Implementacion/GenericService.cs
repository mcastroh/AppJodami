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
            return await _repository.ConsultarById(filtro); 
        } 

        public async Task<List<T>> GetAll()
        {
            return (await _repository.ConsultarAll()).ToList(); 
        }

        public async Task<bool> Crear(T entidad)
        {
            return await _repository.Crear(entidad);           
        }
         
        public async Task<bool> Editar(T entidad)
        {
            return await _repository.Editar(entidad);           
        }
          
        public async Task<bool> Eliminar(T entidad)
        { 
            return await _repository.Eliminar(entidad);
        }

       
    }
}