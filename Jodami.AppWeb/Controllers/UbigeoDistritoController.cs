using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class UbigeoDistritoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Departamento> _srvDpto;
        private readonly IGenericService<Provincia> _srvProv;
        private readonly IGenericService<Distrito> _srvDist;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public UbigeoDistritoController(IMapper mapper, IGenericService<Departamento> srvDpto, IGenericService<Provincia> srvProv, IGenericService<Distrito> srvDist, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _srvDpto = srvDpto;
            _srvProv = srvProv;
            _srvDist = srvDist;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index(int idDepartamento, int idProvincia)
        {
            var departamento = _mapper.Map<VMUbigeoDepartamento>(await _srvDpto.GetById(x => x.IdDepartamento == idDepartamento));
            var provincia = _mapper.Map<VMUbigeoProvincia>(await _srvProv.GetById(x => x.IdProvincia == idProvincia));
            var distritos = _mapper.Map<List<VMUbigeoDistrito>>(await _srvDist.GetByFilter(x => x.IdProvincia == idProvincia));

            foreach (var item in distritos)
            {
                item.UbiDptoKey = departamento.IdDepartamento;
                item.UbiDptoCodigo = departamento.CodigoDepartamento;
                item.UbiDptoName = departamento.DepartamentoName;
                item.UbiProvKey = provincia.IdProvincia;
                item.UbiProvCodigo = provincia.CodigoProvincia;
                item.UbiProvName = provincia.ProvinciaName;
            }

            ViewBag.Departamento = departamento;
            ViewBag.Provincia = provincia;

            return View(distritos);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(VMUbigeoDistrito datos)
        {
            var modelo = new Distrito()
            {
                CodigoDistrito = datos.UbiProvCodigo + datos.CodigoDistrito,
                DistritoName = datos.DistritoName,
                IdProvincia = datos.UbiProvKey,           
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _srvDist.Insert(modelo);

            return RedirectToAction(nameof(Index), new { idDepartamento = datos.UbiDptoKey, idProvincia =datos.UbiProvKey });
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string keyDist, VMUbigeoDistrito datos)
        {
            var modelo = await _srvDist.GetById(x => x.IdDistrito == datos.IdDistrito);

            modelo.CodigoDistrito = datos.UbiProvCodigo + keyDist;
            modelo.DistritoName = datos.DistritoName;
            modelo.IdProvincia = datos.UbiProvKey;           
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _srvDist.Update(modelo);
            return RedirectToAction(nameof(Index), new { idDepartamento = datos.UbiDptoKey, idProvincia = datos.UbiProvKey });

        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMUbigeoDistrito datos)
        {
            bool flgRetorno = await _srvDist.Delete(x => x.IdDistrito == datos.IdDistrito);
            return RedirectToAction(nameof(Index), new { idDepartamento = datos.UbiDptoKey, idProvincia = datos.UbiProvKey });
        }

        #endregion

        #region Método  => Listar PDF     

        public async Task<IActionResult> ListarPDF(int idDepartamento, int idProvincia)
        {
            var departamento = _mapper.Map<VMUbigeoDepartamento>(await _srvDpto.GetById(x => x.IdDepartamento == idDepartamento));
            var provincia = _mapper.Map<VMUbigeoProvincia>(await _srvProv.GetById(x => x.IdProvincia == idProvincia));
            var distritos = _mapper.Map<List<VMUbigeoDistrito>>(await _srvDist.GetByFilter(x => x.IdProvincia == idProvincia));

            foreach (var item in distritos)
            {
                item.UbiDptoKey = departamento.IdDepartamento;
                item.UbiDptoCodigo = departamento.CodigoDepartamento;
                item.UbiDptoName = departamento.DepartamentoName;
                item.UbiProvKey = provincia.IdProvincia;
                item.UbiProvCodigo = provincia.CodigoProvincia;
                item.UbiProvName = provincia.ProvinciaName;
            }  

            string titulo = "Distritos por Provincias";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", distritos)
            {
                FileName = $"Distritos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}