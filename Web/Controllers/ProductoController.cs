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
    public class ProductoController : Controller
    {

        private int numItemsPerPage = 6;
        private int idVendedor = 2;

        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto(); //Obtengo todos los datos de la BD y los agrego a la lista 
                return View(lista); //Retorno la vista con la lista ya cargada
                //Para cargar la lista de categorías
                //Nota:Hacer repositorios de categorias
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
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto(); //Obtengo todos los datos de la BD y los agrego a la lista 
                return View(lista); //Retorno la vista con la lista ya cargada
                //Para cargar la lista de categorías
                //Nota:Hacer repositorios de categorias
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Producto
        //Para el mantenimiento de productos
        public ActionResult IndexVendedor()
        {
            //Creo una lista tipo Producto
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProductoPorVendedor(idVendedor); //Obtengo todos los datos de la BD y los agrego a la lista 
                ViewBag.NumItemsPerPage = numItemsPerPage;
                ViewBag.Nombres = _ServiceProducto.GetProductoNombres(idVendedor);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista); //Retorno la vista con la lista ya cargada
        }

        //Filtro por Nombre
        public PartialViewResult buscarProductoxNombre(string filtro, int tipoUsuario)
        {
            IEnumerable<Producto> lista = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();

            //Validar si hay texto vacio
            if (string.IsNullOrEmpty(filtro.Trim()))
            {
                return PaginacionYOrden(1, tipoUsuario, 0);

            }
            else
            {
                lista = _ServiceProducto.GetProductoPorNombre(filtro, idVendedor);
                return PartialView("_PaginacionYOrdenViewProducto", lista);

            }

        }

        //Para la paginación
        public PartialViewResult PaginacionYOrden(int pag, int tipoUsuario, int orden = 0)
        {

            //Creo una lista tipo Producto
            IEnumerable<Producto> lista = null;

            try
            {

                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();

                if (tipoUsuario == 1)
                {

                    lista = _ServiceProducto.GetProducto();
                }
                else
                {
                    lista = _ServiceProducto.GetProductoPorVendedor(idVendedor);
                }

                int startIndex = (pag * numItemsPerPage) - numItemsPerPage;
                int count = (lista.Count() - startIndex) < numItemsPerPage ? (lista.Count() - startIndex) : numItemsPerPage;
                lista = ((List<Producto>)lista).GetRange(startIndex, count);

                if (orden == 0)
                {
                    lista = lista.OrderByDescending(producto => producto.Precio);
                }
                else
                {
                    lista = lista.OrderBy(producto => producto.Precio);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PaginacionYOrdenViewProducto", lista);
        }
    }
}
