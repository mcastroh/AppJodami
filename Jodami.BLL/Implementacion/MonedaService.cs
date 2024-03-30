using Jodami.BLL.Interfaces;
using Jodami.DAL.Interfaces;
using Jodami.Entity;

namespace Jodami.BLL.Implementacion
{
    public class MonedaService : IMonedaService
    {
        private readonly IGenericRepository<Moneda> _repository;

        #region => Constructor

        public MonedaService(IGenericRepository<Moneda> genericRepository)
        {
            _repository = genericRepository;
        }

        #endregion 

        #region Entidad => GetAll

        public async Task<List<Moneda>> GetAll()
        {
            try
            {
                return (await _repository.Consultar()).ToList();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD => Crear

        public async Task<Moneda> Crear(Moneda entidad)
        {
            try
            {
                var dato = await _repository.Crear(entidad);

                if (dato.IdMoneda == 0)
                {
                    throw new TaskCanceledException("Moneda no pudo ser creada.");
                }

                return dato; 
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD => Editar

        public async Task<Moneda> Editar(Moneda entidad)
        {
            try
            {
                var dato = (await _repository.Consultar(x=> x.IdMoneda == entidad.IdMoneda)).FirstOrDefault();
                
                dato.Descripcion = entidad.Descripcion;
                dato.Simbolo = entidad.Simbolo;
                dato.IdSunat = entidad.IdSunat;
                dato.UsuarioName = "Admin";
                dato.FechaRegistro = DateTime.Now;  

                bool flg = await _repository.Editar(dato); 
                
                if (!flg)  
                    throw new TaskCanceledException("Moneda no pudo ser editada.");

                return dato;                
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD => Eliminar

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var dato = (await _repository.Consultar(x => x.IdMoneda == id)).FirstOrDefault();

                if (dato == null)
                    throw new TaskCanceledException("Moneda no existe.");                                

                return await _repository.Eliminar(dato); 
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}