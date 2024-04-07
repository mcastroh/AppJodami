using AutoMapper;
using Jodami.AppWeb.Models.Dto;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Vehiculos> _service;
        private readonly IGenericService<TipoFlete> _srvTipoFlete;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public VehiculosController(IMapper mapper, IGenericService<Vehiculos> service, IGenericService<TipoFlete> srvTipoFlete, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
            _srvTipoFlete = srvTipoFlete;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = new DtoVehiculos();
            dto.listaTipoFlete = new List<VMTipoFlete>();
            dto.listaVehiculos = new List<VMVehiculos>();

            var vehiculos = _mapper.Map<List<VMVehiculos>>(await _service.GetAll());
            var tipoFletes = _mapper.Map<List<VMTipoFlete>>(await _srvTipoFlete.GetAll());

            foreach (var item in vehiculos)
            {
                item.TiposDeFlete = tipoFletes;
            }

            dto.objVehiculos = new VMVehiculos() { TiposDeFlete = tipoFletes };
            dto.listaVehiculos = vehiculos;
            dto.listaTipoFlete = tipoFletes;            

            return View(dto);
        }

        #endregion

        #region Adicionar => HttpPost      

        [HttpPost]
        public async Task<IActionResult> Adicionar(DtoVehiculos dto)
        {
            var entity = new Vehiculos()
            {
                Nombre = string.IsNullOrEmpty(dto.objVehiculos.Nombre) ? string.Empty : dto.objVehiculos.Nombre,
                Marca = dto.objVehiculos.Marca,
                Modelo = dto.objVehiculos.Modelo,
                Color = dto.objVehiculos.Color, 
                Placa = dto.objVehiculos.Placa,
                Certificado = dto.objVehiculos.Certificado,
                PesoKg = dto.objVehiculos.PesoKg.HasValue ? dto.objVehiculos.PesoKg.Value : 0,
                EsDeEmpresa = dto.objVehiculos.EsDeEmpresa,
                IdFlete = dto.objVehiculos.IdFlete.HasValue ? dto.objVehiculos.IdFlete.Value : null,
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var dato = await _service.Insert(entity);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(VMVehiculos dto)
        {
            var modelo = await _service.GetById(x => x.IdVehiculo == dto.IdVehiculo);

            modelo.Nombre = string.IsNullOrEmpty(dto.Nombre) ? string.Empty : dto.Nombre;
            modelo.Marca = dto.Marca;
            modelo.Modelo = dto.Modelo;
            modelo.Color = dto.Color;
            modelo.Placa = dto.Placa;
            modelo.Certificado = dto.Certificado;
            modelo.PesoKg = dto.PesoKg.HasValue ? dto.PesoKg.Value : 0;
            modelo.EsDeEmpresa = dto.EsDeEmpresa;
            modelo.IdFlete = dto.IdFlete.HasValue ? dto.IdFlete.Value : null;
            modelo.EsActivo = dto.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _service.Update(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMVehiculos dto)
        {
            bool flgRetorno = await _service.Delete(x => x.IdVehiculo == dto.IdVehiculo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = _mapper.Map<List<VMVehiculos>>(await _service.GetAll());

            string titulo = "Vehículos";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Vehiculos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}
