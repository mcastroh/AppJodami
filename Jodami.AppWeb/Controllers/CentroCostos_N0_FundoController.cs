using AutoMapper;
using Jodami.AppWeb.Models.Dto;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.AppWeb.Utilidades.Infraestructura;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class CentroCostos_N0_FundoController : Controller
    {
        #region Variables 

        private readonly Usuario _sessionUsuario;
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<CentroCosto> _srvDB;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor 

        public CentroCostos_N0_FundoController(DbJodamiContext contexto, IGenericService<CentroCosto> srvDB, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _contexto = contexto;
            _srvDB = srvDB;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion
                 
        #region Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var dto = await ETL_CentroCostos();
            return View(dto);
        }

        #endregion            

        #region Add 

        [HttpPost]
        public async Task<IActionResult> Add(VMCentroCostos vmModelo)
        { 
            int keyFundo = await Get_NivelCentroCosto(KeysNames.NIVEL_CENTRO_COSTO_FUNDO);

            var modelo = new CentroCosto()
            {
                IdNivelCentroCosto = keyFundo,
                CodigoCentroCosto = vmModelo.CodigoCentroCosto,
                Descripcion = vmModelo.Descripcion,
                EsNivelEspecifico = vmModelo.EsNivelEspecifico, 
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _srvDB.Insert(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VMCentroCostos vmModelo)
        {
            var modelo = await _srvDB.GetById(x => x.IdCentroCosto == vmModelo.IdCentroCosto);

            modelo.Descripcion = vmModelo.Descripcion;
            modelo.CodigoCentroCosto = vmModelo.CodigoCentroCosto;
            modelo.EsNivelEspecifico = vmModelo.EsNivelEspecifico;
            modelo.EsActivo = vmModelo.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _srvDB.Update(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VMCentroCostos vmModelo)
        {
            bool flgRetorno = await _srvDB.Delete(x => x.IdCentroCosto == vmModelo.IdCentroCosto);
            return RedirectToAction("Index");
        }

        #endregion
                 
        #region GET => Nivel Centro de Costo

        public async Task<int> Get_NivelCentroCosto(string nameNivel)
        {
            int keyFundo = (await _contexto.NivelCentroCosto.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion.ToUpper() == nameNivel)).IdNivelCentroCosto;
            return keyFundo;
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF(string nivel)
        {
            int keyFundo = await Get_NivelCentroCosto(KeysNames.NIVEL_CENTRO_COSTO_FUNDO);
            var modelo = _mapper.Map<List<VMCentroCostos>>(await _contexto.CentroCosto.AsNoTracking().Where(x => x.IdNivelCentroCosto == keyFundo).OrderBy(x => x.CodigoCentroCosto).ToListAsync());

            string titulo = $"Centro de Costos Nivel - {nivel} ";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", modelo)
            {
                FileName = $"Centro de Costos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>   Centro de Costos Nivel 0

        public async Task<DtoCentroCostos> ETL_CentroCostos()
        {
            var lstUnidadGasto = await _contexto.UnidadGasto.ToListAsync();
            var lstSistemaConduccion = await _contexto.SistemaConduccion.ToListAsync();
            var lstCultivo = await _contexto.Cultivo.ToListAsync();

            int keyFundo = await Get_NivelCentroCosto(KeysNames.NIVEL_CENTRO_COSTO_FUNDO);
            var modelo = _mapper.Map<List<VMCentroCostos>>(await _contexto.CentroCosto
                                .AsNoTracking()
                                .Include(x => x.IdCultivoNavigation)
                                .Include(x => x.IdNivelCentroCostoNavigation)
                                .Include(x => x.IdSistemaConduccionNavigation)
                                .Where(x => x.IdNivelCentroCosto == keyFundo)
                                .OrderBy(x => x.CodigoCentroCosto)
                                .ToListAsync());

            var dto = new DtoCentroCostos()
            {
                LstCentroCostos = modelo,
                CentroCostoFundo = new VMCentroCostos()
                {
                    LstUnidadGasto = lstUnidadGasto,
                    LstSistemaConduccion = lstSistemaConduccion,
                    LstCultivo = lstCultivo
                }
            };

            return dto;
        }

        #endregion           

    }
}