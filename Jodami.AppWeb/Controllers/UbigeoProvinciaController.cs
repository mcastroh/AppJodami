using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class UbigeoProvinciaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Departamento> _srvDpto;
        private readonly IGenericService<Provincia> _srvProv;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public UbigeoProvinciaController(IMapper mapper, IGenericService<Departamento> srvDpto, IGenericService<Provincia> srvProv, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _srvDpto = srvDpto;
            _srvProv = srvProv;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var dpto = _mapper.Map<VMUbigeoDepartamento>(await _srvDpto.GetById(x => x.IdDepartamento == id));
            var provincias = _mapper.Map<List<VMUbigeoProvincia>>(await _srvProv.GetByFilter(x => x.IdDepartamento == id));

            foreach (var item in provincias)
            {               
                item.UbiDptoKey = dpto.IdDepartamento;
                item.UbiDptoCodigo = dpto.CodigoDepartamento;
                item.UbiDptoName = dpto.DepartamentoName;
            }

            ViewBag.Departamento = dpto;           

            return View(provincias);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(VMUbigeoProvincia datos)
        {
            var modelo = new Provincia()
            {
                CodigoProvincia = datos.UbiDptoCodigo + datos.CodigoProvincia,
                ProvinciaName = datos.ProvinciaName,
                IdDepartamento = datos.UbiDptoKey,             
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _srvProv.Insert(modelo);

            return RedirectToAction(nameof(Index), new { id = datos.UbiDptoKey });
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string keyProv, VMUbigeoProvincia datos)
        {
            var modelo = await _srvProv.GetById(x => x.IdProvincia == datos.IdProvincia);
            modelo.CodigoProvincia = datos.UbiDptoCodigo + keyProv;
            modelo.ProvinciaName = datos.ProvinciaName;
            modelo.IdDepartamento = datos.UbiDptoKey;         
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;
            bool flgRetorno = await _srvProv.Update(modelo);
            return RedirectToAction(nameof(Index), new { id = datos.UbiDptoKey });
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMUbigeoProvincia datos)
        {
            bool flgRetorno = await _srvProv.Delete(x => x.IdProvincia == datos.IdProvincia);
            return RedirectToAction(nameof(Index), new { id = datos.UbiDptoKey });
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF(int id)
        {
            var dpto = _mapper.Map<VMUbigeoDepartamento>(await _srvDpto.GetById(x => x.IdDepartamento == id));
            var provincias = _mapper.Map<List<VMUbigeoProvincia>>(await _srvProv.GetByFilter(x => x.IdDepartamento == id));

            foreach (var item in provincias)
            {
                item.UbiDptoKey = dpto.IdDepartamento;
                item.UbiDptoCodigo = dpto.CodigoDepartamento;
                item.UbiDptoName = dpto.DepartamentoName;
            }

            string titulo = "Provincias por Departamento";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", provincias)
            {
                FileName = $"Provincias {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}