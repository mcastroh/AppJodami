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
    public class CentroCostos_N2_VariedadController : Controller
    {
        #region Variables 

        private readonly Usuario _sessionUsuario;
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<CentroCosto> _srvCentroCosto;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor 

        public CentroCostos_N2_VariedadController(DbJodamiContext contexto, IGenericService<CentroCosto> srvCentroCosto, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _contexto = contexto;
            _srvCentroCosto = srvCentroCosto;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion

        #region Centro Costos => Fundos

        #region Index  

        [HttpGet]
        public async Task<IActionResult> FundoIndex()
        {
            int keyFundo = await Get_NivelCentroCosto(KeysNames.NIVEL_CENTRO_COSTO_FUNDO);
            var modelo = _mapper.Map<List<VMCentroCostos>>(await _contexto.CentroCosto.AsNoTracking().Where(x => x.IdNivelCentroCosto == keyFundo).OrderBy(x=> x.CodigoCentroCosto).ToListAsync());

            var dto = new DtoCentroCostos()
            {
                CentroCostoNavigation = modelo,
                CentroCostoFundo = new VMCentroCostos()
            };

            return View(dto);
        }

        #endregion            

        #region Add 

        [HttpPost]
        public async Task<IActionResult> FundoAdd(VMCentroCostos vmModelo)
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

            var entity = await _srvCentroCosto.Insert(modelo);
            return RedirectToAction("FundoIndex");
        }

        #endregion

        #region Edit 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundoEdit(VMCentroCostos vmModelo)
        {
            var modelo = await _srvCentroCosto.GetById(x => x.IdCentroCosto == vmModelo.IdCentroCosto);

            modelo.Descripcion = vmModelo.Descripcion;
            modelo.CodigoCentroCosto = vmModelo.CodigoCentroCosto;
            modelo.EsNivelEspecifico = vmModelo.EsNivelEspecifico;
            modelo.EsActivo = vmModelo.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _srvCentroCosto.Update(modelo);
            return RedirectToAction("FundoIndex");
        }

        #endregion

        #region Eliminar 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundoDelete(VMCentroCostos vmModelo)
        {
            bool flgRetorno = await _srvCentroCosto.Delete(x => x.IdCentroCosto == vmModelo.IdCentroCosto);
            return RedirectToAction("FundoIndex");
        }

        #endregion

        #endregion


        #region Centro Costos => Cultivos

        #region Index

        [HttpGet]
        public async Task<IActionResult> CultivoIndex(int idCentroCostoPadre)
        {
            var fundo = _mapper.Map<VMCentroCostos>(await _contexto.CentroCosto.AsNoTracking().FirstOrDefaultAsync(x => x.IdCentroCosto == idCentroCostoPadre));

            int keyFundo = await Get_NivelCentroCosto(KeysNames.NIVEL_CENTRO_COSTO_CULTIVO);
            var modelo = _mapper.Map<List<VMCentroCostos>>(await _contexto.CentroCosto.AsNoTracking().Where(x => x.IdNivelCentroCosto == keyFundo && x.IdCentroCostoPadre.Value == idCentroCostoPadre).OrderBy(x => x.CodigoCentroCosto).ToListAsync());
            
            var dto = new DtoCentroCostos()
            {
                CentroCostoFundo = fundo,
                CentroCostoNavigation = modelo,
                CentroCostoCultivo = new VMCentroCostos()
            };

            return View(dto);
        }

        #region Add 

        [HttpPost]
        public async Task<IActionResult> CultivoAdd(VMCentroCostos vmModelo)
        {
            int keyCultivo = await Get_NivelCentroCosto(KeysNames.NIVEL_CENTRO_COSTO_CULTIVO);

            var modelo = new CentroCosto()
            {
                IdNivelCentroCosto = keyCultivo,
                CodigoCentroCosto = vmModelo.CodigoCentroCosto,
                Descripcion = vmModelo.Descripcion,
                IdCentroCostoPadre = vmModelo.IdCentroCostoPadre,
                EsNivelEspecifico = vmModelo.EsNivelEspecifico,
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _srvCentroCosto.Insert(modelo);
            return RedirectToAction("CultivoIndex", new { idCentroCostoPadre  = vmModelo.IdCentroCostoPadre });
        }

        #endregion

        #region Edit 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CultivoEdit(VMCentroCostos vmModelo)
        {
            var modelo = await _srvCentroCosto.GetById(x => x.IdCentroCosto == vmModelo.IdCentroCosto);

            modelo.Descripcion = vmModelo.Descripcion;
            modelo.CodigoCentroCosto = vmModelo.CodigoCentroCosto;
            modelo.EsNivelEspecifico = vmModelo.EsNivelEspecifico;
            modelo.EsActivo = vmModelo.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _srvCentroCosto.Update(modelo);
            return RedirectToAction("CultivoIndex");
        }

        #endregion

        #region Eliminar 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CultivoDelete(VMCentroCostos vmModelo)
        {
            bool flgRetorno = await _srvCentroCosto.Delete(x => x.IdCentroCosto == vmModelo.IdCentroCosto);
            return RedirectToAction("CultivoIndex");
        }

        #endregion

        #endregion


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


    }
}