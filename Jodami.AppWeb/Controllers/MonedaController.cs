using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class MonedaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMonedaService _service;

        #region Constructor 

        public MonedaController(IMapper mapper, IMonedaService service)
        {
            _mapper = mapper;
            _service = service;
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = _mapper.Map<List<VMMoneda>>(await _service.GetAll());               
            return View(query);
        }

        #endregion

        #region Adicionar => HttpPost
         
        [HttpPost]
        public async Task<IActionResult> Adicionar(string descripcion, string simbolo, string idSunat)
        {   
            var modelo = new Moneda()
            {
                Descripcion = descripcion,
                Simbolo = simbolo,
                IdSunat = idSunat,
                EsActivo = true,                
                UsuarioName = "Admin",
                FechaRegistro = DateTime.Now
            };
           
            var entidad = await _service.Crear(modelo); 
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(VMMoneda vmModelo)
        {
            var modelo = _mapper.Map<Moneda>(vmModelo);
            var Moneda = await _service.Editar(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMMoneda vmModelo)
        {            
            var Moneda = await _service.Eliminar(vmModelo.IdMoneda);

            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = _mapper.Map<List<VMMoneda>>(await _service.GetAll());

            string titulo = "Listado de Monedas";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Monedas {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,               
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            }; 
        }

        #endregion

    }
}
