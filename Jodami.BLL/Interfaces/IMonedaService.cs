using Jodami.Entity;

namespace Jodami.BLL.Interfaces
{
    public interface IMonedaService
    {       
        Task<List<Moneda>> GetAll();

        Task<Moneda> Crear(Moneda entidad);

        Task<Moneda> Editar(Moneda entidad);

        Task<bool> Eliminar(int id);

    }
}
