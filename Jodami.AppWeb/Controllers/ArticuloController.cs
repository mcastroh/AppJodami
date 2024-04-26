using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.AppWeb.Utilidades.Infraestructura;
using Jodami.AppWeb.Utilidades.Servicios;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IGenericService<Articulo> _srvArticulo;
        private readonly IGenericService<GrupoArticulo> _srvGrupoArticulo;
        private readonly IGenericService<SubGrupoArticulo> _srvSubGrupoArticulo;
        private readonly IGenericService<UnidadMedida> _srvUnidadMedida;
        private readonly IGenericService<TipoArticulo> _srvTipoArticulo;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public ArticuloController(
            IMapper mapper,
            IGenericService<Articulo> srvArticulo,
            IGenericService<GrupoArticulo> srvGrupoArticulo,
            IGenericService<SubGrupoArticulo> srvSubGrupoArticulo,
            IGenericService<UnidadMedida> srvUnidadMedida,
            IGenericService<TipoArticulo> srvTipoArticulo,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _srvArticulo = srvArticulo;
            _srvGrupoArticulo = srvGrupoArticulo;
            _srvSubGrupoArticulo = srvSubGrupoArticulo;
            _srvUnidadMedida = srvUnidadMedida;
            _srvTipoArticulo = srvTipoArticulo;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = await ETL_Articulos();
            return View(query);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(VMArticulos modelo)
        {
            var articulo = new Articulo()
            {
                CodigoArticulo = "2401-1234",
                Descripcion = modelo.Descripcion,
                IdSubGrupoArticulo = modelo.IdSubGrupoArticulo,
                IdUnidadInventario = modelo.IdUnidadInventario,
                IdUnidadCompra = modelo.IdUnidadCompra,
                IdUnidadVenta = modelo.IdUnidadVenta,
                IdTipoArticulo = modelo.IdTipoArticulo,
                IdTipoDetraccion = modelo.IdTipoDetraccion,
                IdTipoValorizacion = modelo.IdTipoValorizacion,
                IdTipoExistencia = modelo.IdTipoExistencia,
                StockMinimo = modelo.StockMinimo,
                StockMaximo = modelo.StockMaximo,
                StockSeguridad = modelo.StockSeguridad,
                Observaciones = modelo.Observaciones,
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now 
            };

            var entity = await _srvArticulo.Insert(articulo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(VMArticulos modelo)
        {
            var articulo = await _srvArticulo.GetById(x => x.IdArticulo == modelo.IdArticulo);

            articulo.CodigoArticulo = modelo.CodigoArticulo;
            articulo.Descripcion = modelo.Descripcion;
            articulo.IdSubGrupoArticulo = modelo.IdSubGrupoArticulo;
            articulo.IdUnidadInventario = modelo.IdUnidadInventario;
            articulo.IdUnidadCompra = modelo.IdUnidadCompra;
            articulo.IdUnidadVenta = modelo.IdUnidadVenta;
            articulo.IdTipoArticulo = modelo.IdTipoArticulo;
            articulo.IdTipoDetraccion = modelo.IdTipoDetraccion;
            articulo.IdTipoValorizacion = modelo.IdTipoValorizacion;
            articulo.IdTipoExistencia = modelo.IdTipoExistencia;
            articulo.StockMinimo = modelo.StockMinimo;
            articulo.StockMaximo = modelo.StockMaximo;
            articulo.StockSeguridad = modelo.StockSeguridad;
            articulo.Observaciones = modelo.Observaciones;
            articulo.EsActivo = true;
            articulo.UsuarioName = _sessionUsuario.Nombre;
            articulo.FechaRegistro = DateTime.Now; 

            bool flgRetorno = await _srvArticulo.Update(articulo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMArticulos modelo)
        {
            bool flgRetorno = await _srvArticulo.Delete(x => x.IdArticulo == modelo.IdArticulo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = await ETL_Articulos();

            string titulo = "Artículos";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Articulos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Artículos

        public async Task<List<VMArticulos>> ETL_Articulos()
        {
            var tipoArticulos = _mapper.Map<List<VMTipoArticulo>>(await _srvTipoArticulo.GetAll());
            var grupos = _mapper.Map<List<VMGrupoArticulo>>(await _srvGrupoArticulo.GetAll());
            var subGrupos = _mapper.Map<List<VMSubGrupoArticulo>>(await _srvSubGrupoArticulo.GetAll());
            var unidades = _mapper.Map<List<VMUnidadMedida>>(await _srvUnidadMedida.GetAll());
            var articulos = _mapper.Map<List<VMArticulos>>(await _srvArticulo.GetAll());

            foreach (var item in articulos)
            {
                item.Nav_SubGrupoArticulo = subGrupos.FirstOrDefault(x => x.IdSubGrupoArticulo == item.IdSubGrupoArticulo);
                item.Nav_GrupoArticulo = grupos.FirstOrDefault(x => x.IdGrupoArticulo == item.Nav_SubGrupoArticulo.IdGrupoArticulo);
                item.Nav_UnidadMedidaInventario = unidades.FirstOrDefault(x => x.IdUnidad == item.IdUnidadInventario.Value);
                item.Nav_TipoArticulo = tipoArticulos.FirstOrDefault(x => x.IdTipoArticulo == item.IdTipoArticulo.Value);
            }

            ViewBag.NewArticulo = new VMArticulos()
            {
                IdTipoArticulo = tipoArticulos.FirstOrDefault(x => x.Descripcion == "Artículo").IdTipoArticulo,
                List_TipoArticulo = tipoArticulos.ToList(),
                List_GrupoArticulo = grupos.ToList(),
                List_SubGrupoArticulo = subGrupos.ToList(),
                List_UnidadMedidaInventario = unidades.ToList()
            };

            ViewBag.ListTipoArticulo = tipoArticulos.ToList();
            ViewBag.ListGrupoArticulo = grupos.ToList();
            ViewBag.ListSubGrupoArticulo = subGrupos.ToList();
            ViewBag.ListUnidadMedidaInventario = unidades.ToList();

            return articulos;
        }

        #endregion



        #region GET JsonResult => Get Sub Grupos de Artículos 

        public async Task<JsonResult> GetSubGruposByGrupoId(int grupoId)
        {
            var subGrupos =  _mapper.Map<List<VMSubGrupoArticulo>>(await _srvSubGrupoArticulo.GetByFilter( x=> x.IdGrupoArticulo == grupoId));
            return Json(subGrupos);
        }

        #endregion

        #region GET JsonResult => Siguiente Corelativo Codigo Articulo

        public async Task<JsonResult> GetSiguienteCodigoArticulo(int subGrupoId)
        {
            string newCodigoArticulo = string.Empty;            
            var articulos = _mapper.Map<List<VMArticulos>>(await _srvArticulo.GetByFilter(x => x.IdSubGrupoArticulo == subGrupoId));
            var codigoSubGrupo = _mapper.Map<VMSubGrupoArticulo>(await _srvSubGrupoArticulo.GetById(x => x.IdSubGrupoArticulo == subGrupoId)).Codigo;           

            if (articulos.Count != 0)
            {
                var articulo = articulos.OrderByDescending(x=> x.CodigoArticulo).FirstOrDefault();
                string[] arraySelect = articulo.CodigoArticulo.Split('-');
                newCodigoArticulo = arraySelect[0] + "-" + (int.Parse(arraySelect[1]) + 1).ToString("0000");
            }
            else
            {
                newCodigoArticulo = codigoSubGrupo + "-0001";
            } 

            return Json(newCodigoArticulo);
        }

        #endregion



    }
}