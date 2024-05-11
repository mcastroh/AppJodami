using AutoMapper;
using Jodami.AppWeb.Rack;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Jodami.AppWeb.Controllers
{
    public class RackClaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<TipoVia> _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public RackClaseController(IMapper mapper, IGenericService<TipoVia> service, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();

            //List<RackClase> objRackClase = new List<RackClase>();
            //BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
            //objRackClase = BaseGlobal.goRackClasePublicControllerFacade.RaC_List(new RackClase());
            //return View(objRackClase);

        }

        #endregion

        //#region Adicionar => HttpPost

        //[HttpPost]
        //public async Task<IActionResult> Adicionar(string codigo, string descripcion)
        //{
        //    var modelo = new TipoVia()
        //    {
        //        CodigoTipoVia = codigo,
        //        Descripcion = descripcion,               
        //        EsActivo = true,
        //        UsuarioName = _sessionUsuario.Nombre,
        //        FechaRegistro = DateTime.Now
        //    };

        //    var entity = await _service.Insert(modelo);
        //    return RedirectToAction("Index");
        //}

        //#endregion

        //#region Edit => HttpPost 

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Editar(VMTipoVia vmModelo)
        //{
        //    var modelo = await _service.GetById(x => x.IdTipoVia == vmModelo.IdTipoVia);
            
        //    modelo.CodigoTipoVia = vmModelo.CodigoTipoVia;
        //    modelo.Descripcion = vmModelo.Descripcion;           
        //    modelo.EsActivo = vmModelo.EsActivo;
        //    modelo.UsuarioName = _sessionUsuario.Nombre;
        //    modelo.FechaRegistro = DateTime.Now;

        //    bool flgRetorno = await _service.Update(modelo);
        //    return RedirectToAction("Index");
        //}

        //#endregion

        //#region Eliminar => HttpPost

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Eliminar(VMTipoVia vmModelo)
        //{
        //    bool flgRetorno = await _service.Delete(x => x.IdTipoVia == vmModelo.IdTipoVia);
        //    return RedirectToAction("Index");
        //}

        //#endregion

        //#region Método  => Listar PDF   

        //public async Task<IActionResult> ListarPDF()
        //{
        //    var query = _mapper.Map<List<VMTipoVia>>(await _service.GetAll());

        //    string titulo = "Tipos de Vías";
        //    string protocolo = Request.IsHttps ? "Https" : "Http";
        //    string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
        //    string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

        //    return new ViewAsPdf("ListarPDF", query)
        //    {
        //        FileName = $"Tipos de Vias {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
        //        CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
        //    };
        //}

        //#endregion

    }
}
