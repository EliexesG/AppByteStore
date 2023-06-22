using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();

            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidoByCliente(3);
                ViewData.Add("GetBack", "Index");
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult IndexAdmin()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();

            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidoByVendedor(2);
                ViewData.Add("GetBack", "IndexAdmin");
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int id)
        {
            Pedido pedido = null;

            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                pedido = _ServicePedido.GetPedidoByID(id);
                return View(pedido);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
