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
            var query = _mapper.Map<List<VMMoneda>>(await _service.GetAll());
            //var query = await Obtener();     
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




        #region Task => Obtener Datos

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


         

    }
}
