using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class PedidoController : Controller
    {

        private int numItemsPerPage = 6;

        // GET: Pedido
        [CustomAuthorize((int)Roles.Cliente)]
        public ActionResult Index()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();

            try
            {
                Usuario user = Session["User"] as Usuario;
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidoByCliente(user.IdUsuario);
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

        [CustomAuthorize((int)Roles.Administrador)]
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

        [CustomAuthorize((int)Roles.Vendedor)]
        public ActionResult IndexVendedor()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();

            try
            {
                Usuario user = Session["User"] as Usuario;
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidoByVendedor(user.IdUsuario);
                ViewBag.Vendedor = user.IdUsuario;
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Cliente)]
        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {
            Pedido pedido = null;

            try
            {
                if(id == null)
                {
                    return RedirectToAction("Index");
                }

                IServicePedido _ServicePedido = new ServicePedido();
                pedido = _ServicePedido.GetPedidoByID(Convert.ToInt32(id));

                if (pedido == null)
                {
                    TempData["Message"] = "No existe el pedido solicitado";
                    TempData["Redirect"] = "Pedido";
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }

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
                Usuario user = Session["User"] as Usuario;

                if (tipoUsuario == 1)
                {
                    lista = _ServicePedido.GetPedido();
                }
                else
                {
                    lista = _ServicePedido.GetPedidoByCliente(user.IdUsuario);
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

        [CustomAuthorize((int)Roles.Cliente)]
        public ActionResult Carrito ()
        {
            if(TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            ViewBag.Carrito = Web.Utils.Carrito.Instancia.Items;

            return View();
        }

    }
}
