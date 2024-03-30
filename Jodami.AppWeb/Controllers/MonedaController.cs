using AutoMapper;
using AutoMapper.Internal;
using Azure;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.AppWeb.Utilidades.Response;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var query = await Obtener();     
            return View(query.ListaObjeto);
        }

        #endregion


        #region HttpGet => Obtener  

        [HttpGet]
        public async Task<GenericResponse<VMMoneda>> Obtener()
        {
            var gResponse = new GenericResponse<VMMoneda>();

            try
            {
                List<VMMoneda> query = _mapper.Map<List<VMMoneda>>(await _service.GetAll());
                gResponse.Estado = true;
                gResponse.ListaObjeto = query;               
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message; 
            }

            return gResponse;
        }

        #endregion

        #region HttpGet => Index  

        //[HttpPost]
        //public async Task<IActionResult> Guardar([FromForm]string modelo)
        //{
        //    var gResponse = new GenericResponse<VMMoneda>();

        //    try
        //    {
        //        var vmEntity = JsonConvert.DeserializeObject<VMMoneda>(modelo);
        //        var entityCrud = _mapper.Map<Moneda>(vmEntity);
        //        var entitySave = await _service.GuardarCambios(entityCrud);                
                
        //        var vmRetorna = _mapper.Map<VMMoneda>(entitySave);
        //        gResponse.Estado = true;
        //        gResponse.Objeto = vmRetorna;
        //    }
        //    catch (Exception ex)
        //    {
        //        gResponse.Estado = false;
        //        gResponse.Mensaje = ex.Message;
        //        return StatusCode(StatusCodes.Status500InternalServerError, gResponse);
        //    }

        //    return StatusCode(StatusCodes.Status200OK, gResponse);
        //}

        #endregion

    }
}
