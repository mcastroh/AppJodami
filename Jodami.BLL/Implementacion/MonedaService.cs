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
 
        #region Entidad => GetById

        public async Task<Moneda> GetById(int id)
        {
            try
            {
                return await _repository.ConsultarById(x => x.IdMoneda == id);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Entidad => GetAll

        public async Task<List<Moneda>> GetAll()
        {
            try
            {
                return (await _repository.ConsultarAll()).ToList();
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
                var dato = await _repository.ConsultarById(x=> x.IdMoneda == entidad.IdMoneda);
                
                dato.Descripcion = entidad.Descripcion;
                dato.Simbolo = entidad.Simbolo;
                dato.IdSunat = entidad.IdSunat;
                dato.EsActivo = entidad.EsActivo;
                dato.Orden = entidad.Orden;
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
                var dato = await _repository.ConsultarById(x => x.IdMoneda == id);

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