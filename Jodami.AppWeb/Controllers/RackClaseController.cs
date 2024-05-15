using AutoMapper;
using Jodami.AppWeb.Rack;
using Jodami.AppWeb.Utilidades.Infraestructura;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.DotNet.MSIdentity.Shared;

namespace Jodami.AppWeb.Controllers
{
    public class RackClaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<TipoVia> _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        private readonly List<RackClase> _rackClase;

        #region Constructor 

        public RackClaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _rackClase = new List<RackClase>
            {
                new RackClase {Id = "10", Name = "J256"},
                new RackClase {Id = "20", Name = "T255"},
                new RackClase {Id = "30", Name = "Z129"},
                new RackClase {Id = "40", Name = "R745"}
            };
             
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(_rackClase); 
        }

        #endregion


        #region GET: Nuevo

        public ActionResult Nuevo()
        {
            ViewData["Accion"] = KeysNames.FormularioAccion.Nuevo;
            var objRackClaseViewModel = new RackClase();
            return View("Partial_RackClase", objRackClaseViewModel);
        }
        #endregion

        #region GET: Actualizar

        public ActionResult Actualizar(string pIdClaseRack)
        {
            ViewData["Accion"] = KeysNames.FormularioAccion.Actualizar;
            //RackClaseViewModel objRackClaseViewModel = new RackClaseViewModel();
            //BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
            //objRackClaseViewModel.RackClase = BaseGlobal.goRackClasePublicControllerFacade.RaC_Obt(pIdClaseRack);

            var objRackClaseViewModel = new RackClase();

            return View("Partial_RackClase", objRackClaseViewModel);
        }
        #endregion GET: Actualizar

        public ActionResult ViewPartial_RackClase(string pIdClaseRack)
        {
            var objRackClaseViewModel = new RackClase();
            //BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
            //objRackClaseViewModel.RackClase = BaseGlobal.goRackClasePublicControllerFacade.RaC_Obt(pIdClaseRack);


            return View(objRackClaseViewModel);
        }

        #region GET: Eliminar

        public JsonResult Eliminar(string pIdClaseRack)
        {
            //var jsonResponse = new JsonResponse();
            string newCodigoArticulo = string.Empty;
            //try
            //{
            //    BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
            //    JsonResponse.Success = BaseGlobal.goRackClasePublicControllerFacade.RaC_Elim(pIdClaseRack);
            //}
            //catch (Exception ex)
            //{
            //    var sqq = ex.Message.ToString();
            //    var qq = ex.Source.ToString();
            //    var qsq = ex.ToString();
            //    //throw ex;
            //    JsonResponse.Success = false;
            //}

            //return Json(JsonResponse, JsonRequestBehavior.AllowGet);

            return Json(newCodigoArticulo);

        }

        #endregion GET: Eliminar

        //#region GET: ExportarReporteRackClase
        //public ActionResult ExportarReporteRackClase()
        //{
        //    List<RackClase> objListarRackClase = new List<RackClase>();
        //    //BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
        //    //objListarRackClase = BaseGlobal.goRackClasePublicControllerFacade.RaC_List(new RackClase());

        //    //ReportDocument rd = new ReportDocument();
        //    //rd.Load(Path.Combine(Server.MapPath("~/Areas/RACKS/Reportes"), "Rpt_RackClase.rpt"));
        //    //rd.SetDataSource(objListarRackClase);

        //    //Response.Buffer = false;
        //    //Response.ClearContent();
        //    //Response.ClearHeaders();

        //    //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    //stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", "Rpt_RackClase.pdf");

        //}


        //public ActionResult ReporteRackClase()
        //{
        //    ViewBag.IdClaseRack = "04";
        //    return View();
        //}

        //#endregion GET: ExportarReporteEspecificacion

        #region POST: Nuevo

        [HttpPost]
        public ActionResult Nuevo(RackClase pRackClaseViewModel)
        {
            //GrabarDatos(pRackClaseViewModel, KeysNames.FormularioAccion.Nuevo);
            return RedirectToAction("Index");
        }
        #endregion

        #region POST: Actualizar

        [HttpPost]
        public ActionResult Actualizar(RackClase pRackClaseViewModel)
        {
            //GrabarDatos(pRackClaseViewModel, KeysNames.FormularioAccion.Actualizar);
            return RedirectToAction("Index");
        }

        #endregion

        #region GrabarDatos: 

        private void GrabarDatos(RackClase  pRackClaseViewModel, KeysNames.FormularioAccion pFormularioAccion)
        {
            bool bResultado = true;

            RackClase objRackClase = new RackClase();
            //objRackClase = pRackClaseViewModel.RackClase;

            //objRackClase.AuditUser = SessionApplication.Usuario.VCODUSU;
            //objRackClase.AuditFch = DateTime.Now;

            //if (pFormularioAccion == KeysNames.FormularioAccion.Nuevo)
            //{
            //    BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
            //    bResultado = BaseGlobal.goRackClasePublicControllerFacade.RaC_Inst(objRackClase);
            //}
            //else
            //    BaseGlobal.goRackClasePublicControllerFacade = ControllerFactory.Instancia.createRackClaseFacade();
            //{
            //    bResultado = BaseGlobal.goRackClasePublicControllerFacade.RaC_Act(objRackClase);
            //}
        }

        #endregion

       




    }
}
