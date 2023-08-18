using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ReportesController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult ReportesAdmin()
        {

            return View();

        }

        [CustomAuthorize((int)Roles.Vendedor)]
        public ActionResult ReportesVendedor()
        {

            return View();

        }

        [HttpGet]
        public JsonResult GetCantComprasRegistradasEnElDia()
        {
            int cant = 0;
            IServicePedido _ServicePedido = new ServicePedido();

            cant = _ServicePedido.GetCantComprasRegistradasEnElDia();

            return Json(new
            {
                CantComprasRegistradasEnElDia = cant
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTopProductosVendidosByMes()
        {
            IEnumerable<object> lista = null;
            IServicePedido _ServicePedido = new ServicePedido();

            lista = _ServicePedido.GetTopProductosVendidosByMes();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVendedoresMejorEvaluados()
        {
            IEnumerable<Usuario> lista = null;
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            lista = _ServiceUsuario.GetVendedoresMejorEvaluados();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVendedoresPeorEvaluados()
        {
            IEnumerable<Usuario> lista = null;
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            lista = _ServiceUsuario.GetVendedoresPeorEvaluados();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductoMasVendidoVendedor(int idVendedor)
        {
            object productoMasVendido = null;
            IServicePedido _ServicePedido = new ServicePedido();

            productoMasVendido = _ServicePedido.GetProductoMasVendidoVendedor(idVendedor);

            return Json(new
            {
                productoMasVendido
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetClienteMasFiel(int idVendedor)
        {
            object clienteMasFiel = null;
            IServicePedido _ServicePedido = new ServicePedido();

            clienteMasFiel = _ServicePedido.GetClienteMasFiel(idVendedor);

            return Json(new
            {
                clienteMasFiel
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CantEvaluacionesPorEscalaVendedor(int idVendedor)
        {
            IEnumerable<object> EvaluacionesPorEscala = null;
            IServiceEvaluacion _ServiceEvaluaciones = new ServiceEvaluacion();

            EvaluacionesPorEscala = _ServiceEvaluaciones.CantEvaluacionesPorEscalaVendedor(idVendedor);

            return Json(new
            {
                EvaluacionesPorEscala
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
