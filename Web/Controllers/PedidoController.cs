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

        private int numItemsPerPage = 6;
        private int idCliente = 3;
        private int idVendedor = 2;

        // GET: Pedido
        public ActionResult Index()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();

            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidoByCliente(idCliente);
                ViewBag.NumItemsPerPage = numItemsPerPage;
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
                lista = _ServicePedido.GetPedido();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult IndexVendedor()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();

            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidoByVendedor(idVendedor);
                ViewBag.Vendedor = idVendedor;
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

        public PartialViewResult PaginacionYOrden(int pag, int tipoUsuario, int orden = 0)
        {

            //Creo una lista tipo Producto
            IEnumerable<Pedido> lista = null;
            try
            {
                //Instancia 
                IServicePedido _ServicePedido = new ServicePedido();

                if(tipoUsuario == 1)
                {
                    lista = _ServicePedido.GetPedido();
                }
                else
                {
                    lista = _ServicePedido.GetPedidoByCliente(idCliente);
                }
                
                //
                int startIndex = (pag * numItemsPerPage) - numItemsPerPage;
                int count = (lista.Count() - startIndex) < numItemsPerPage ? (lista.Count() - startIndex) : numItemsPerPage;
                lista = ((List<Pedido>)lista).GetRange(startIndex, count);

                if (orden == 0)
                {
                    lista = lista.OrderByDescending(pedido => pedido.CompraEncabezado.FechaHora);
                }
                else
                {
                    lista = lista.OrderBy(pedido => pedido.CompraEncabezado.FechaHora);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PaginacionYOrdenViewPedido", lista);
        }

    }
}
