using Jodami.BLL.Interfaces;
using Jodami.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jodami.BLL.Implementacion
{
    public class EmpresaService : IEmpresaService
    {
        public Task<List<Empresa>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Empresa> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Empresa> GuardarCambios(Empresa empresa, Stream logo = null, string nombreLogo = "")
        {
            throw new NotImplementedException();
        }
    }
}
