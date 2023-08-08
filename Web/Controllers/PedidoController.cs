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
using Web.Util;

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
                if (((Usuario)Session["User"]).MetodoPago.Count() <= 0 || ((Usuario)Session["User"]).Direccion.Count() <= 0)
                {
                    return RedirectToAction("UnAuthorized", "Login");
                }

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

            if (((Usuario)Session["User"]).Direccion.Count() <= 0)
            {
                return RedirectToAction("UnAuthorized", "Login");
            }

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
                if (((Usuario)Session["User"]).MetodoPago.Count() <= 0 || ((Usuario)Session["User"]).Direccion.Count() <= 0)
                {
                    return RedirectToAction("UnAuthorized", "Login");
                }

                if (id == null)
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

            if (((Usuario)Session["User"]).MetodoPago.Count() <= 0 || ((Usuario)Session["User"]).Direccion.Count() <= 0)
            {
                return RedirectToAction("UnAuthorized", "Login");
            }

            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            ViewBag.Carrito = Web.Utils.Carrito.Instancia.Items;

            Usuario usuario =  Session["User"] as Usuario;
            ViewBag.ListaPago = new SelectList(usuario.MetodoPago
                                               .Select(m => new
                                               {
                                                   IdMetodoPago = m.IdMetodoPago,
                                                   CustomDescripcion = $"{m.Proveedor} ({m.TipoPago.Descripcion}) | {m.FechaExpiracion.Value.ToString("MM/yy")}"
                                               }).ToList(),
                                "IdMetodoPago", "CustomDescripcion");

            ViewBag.ListaDireccion = new SelectList(usuario.Direccion, "IdDireccion", "Sennas");

            return View();
        }

        [HttpPost]
        public JsonResult IngresarProductoCarrito(int idProducto)
        {
            string mensaje = "";
            var carrito = Web.Utils.Carrito.Instancia;

            try
            {
                mensaje = carrito.AgregarItem(idProducto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }
            return Json(new { mensaje, cantCarrito = carrito.GetCountItems(), cantProducto = carrito.getCountProducto(idProducto) } );
        }

        [HttpPost]
        public JsonResult ActualizarCantidad (int idProducto, int cantidad)
        {
            string mensaje = "";
            var carrito = Web.Utils.Carrito.Instancia;

            try
            {
                mensaje = carrito.SetItemCantidad(idProducto, cantidad);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }
            return Json(new { mensaje, cantCarrito = carrito.GetCountItems() });
        }

        [HttpPost]
        public JsonResult EliminarProducto (int idProducto)
        {
            string mensaje = "";
            var carrito = Web.Utils.Carrito.Instancia;

            try
            {
                mensaje = carrito.EliminarItem(idProducto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }
            return Json(new { mensaje, cantCarrito = carrito.GetCountItems() });
        }

        public PartialViewResult DetalleCarrito()
        {
            return PartialView("_DetalleCarrito", (IEnumerable<CompraDetalle>)Web.Utils.Carrito.Instancia.Items);
        }

        public PartialViewResult DetalleCarritoNoEditable()
        {

            CompraEncabezado compraEncabezado = new CompraEncabezado();
            compraEncabezado.CompraDetalle = Web.Utils.Carrito.Instancia.Items;
            compraEncabezado.SubTotal = (double)Utils.Carrito.Instancia.GetTotal(); 
            compraEncabezado.Impuesto = (double)compraEncabezado.SubTotal * 0.13;
            compraEncabezado.Total = (double)compraEncabezado.SubTotal + (double)compraEncabezado.Impuesto;

            return PartialView("_DetalleCarritoNoEditable", compraEncabezado);
        }

        [HttpPost]
        public ActionResult Save (CompraEncabezado compraEncabezado)
        {
            try
            {
                Pedido oPedido = null;

                if(Web.Utils.Carrito.Instancia.Items.Count() <= 0)
                {
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Carrito", "Seleccione los productos a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Carrito");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        Usuario usuario = Session["User"] as Usuario;
                        compraEncabezado.Usuario = new Usuario() { IdUsuario = usuario.IdUsuario };

                        var detalle = new List<CompraDetalle>();
                        foreach (var compraDetalle in Utils.Carrito.Instancia.Items)
                        {
                            compraDetalle.EstadoEntrega = (bool)false;
                            detalle.Add(compraDetalle);
                        }
                        compraEncabezado.CompraDetalle = detalle;
                        compraEncabezado.SubTotal = (double)Utils.Carrito.Instancia.GetTotal();
                        compraEncabezado.Impuesto = (double)compraEncabezado.SubTotal * 0.13;
                        compraEncabezado.Total = (double)compraEncabezado.SubTotal + (double)compraEncabezado.Impuesto;

                        Pedido pedido = new Pedido() 
                        { 
                            EstadoEntrega = 0,
                            CompraEncabezado = compraEncabezado,
                        };

                        IServicePedido _ServicePedido = new ServicePedido();
                        oPedido = _ServicePedido.Save(pedido);
                    }

                    if(oPedido != null)
                    {
                        Utils.Carrito.Instancia.EliminarCarrito();
                        TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Pedido", "Pedido guardado satisfactoriamente!", SweetAlertMessageType.success);
                        return RedirectToAction($"Details/{oPedido.IdCompraEncabezado}", "Pedido");
                    }

                    return RedirectToAction("Carrito");
                }
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Pedido";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
