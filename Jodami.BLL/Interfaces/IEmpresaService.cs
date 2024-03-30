using Jodami.Entity;

namespace Jodami.BLL.Interfaces
{
    public interface IEmpresaService
    {         
        Task<List<Empresa>> GetAll();

        Task<Empresa> GetById(int id);

        Task<Empresa> GuardarCambios(Empresa empresa, Stream logo = null, string nombreLogo = "");

    }
}
