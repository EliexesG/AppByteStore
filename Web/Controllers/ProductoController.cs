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
                TempData["GetBack"] = "Index";
                //Para cargar la lista de categorías
                IServiceCategoria _ServiceCategoria = new ServiceCategoria();
                ViewBag.listaCategoria = _ServiceCategoria.GetCategoria();
                ViewBag.Nombres = _ServiceProducto.GetProductoNombres();
                return View(lista); //Retorno la vista con la lista ya cargada


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
                TempData["GetBack"] = "IndexAdmin";
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
                TempData["GetBack"] = "IndexVendedor";
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

        // GET: Pedido/Details/5
        public ActionResult Details(int id)
        {
            Producto producto = null;

            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                producto = _ServiceProducto.GetProductoByID(Convert.ToInt32(id));
                ViewBag.GetBack = TempData["GetBack"];
                /*
                if (producto == null || producto.Stock == 0)
                {
                    TempData["Message"] = "El producto que seleccionaste está agotado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");

                }
                */
                return View(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        //Filtro por Nombre
        public PartialViewResult buscarProductoxNombre(string filtro, int tipoUsuario)
        {
            IEnumerable<Producto> lista = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();
            string partialView = "";

            //Validar si hay texto vacio
            if (string.IsNullOrEmpty(filtro.Trim()) && (tipoUsuario == 1 || tipoUsuario == 2))
            {
                return PaginacionYOrden(1, tipoUsuario, 0);
            }
            else if (string.IsNullOrEmpty(filtro.Trim()) && (tipoUsuario == 3)) {
                lista = _ServiceProducto.GetProducto();
                partialView = "_PartialCatalogoProducto";
            }
            else
            {
                if (tipoUsuario == 1) {
                    lista = _ServiceProducto.GetProductoPorNombre(filtro);
                    partialView = "_PaginacionYOrdenViewProducto";
                }
                else if (tipoUsuario == 2) {
                    lista = _ServiceProducto.GetProductoPorNombre(filtro, idVendedor);
                    partialView = "_PaginacionYOrdenViewProducto";
                }
                else {
                    lista = _ServiceProducto.GetProductoPorNombre(filtro);
                    partialView = "_PartialCatalogoProducto";
                }
            }

            return PartialView(partialView, lista);
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

        public PartialViewResult BuscarProductoxCategoria(int categoria) {

            IEnumerable<Producto> lista = null;

            try {

                IServiceProducto _ServiceProducto = new ServiceProducto();

                if (categoria != -1) {
                    lista = _ServiceProducto.GetProductoByCategoria(categoria);
                }
                else {
                    lista = _ServiceProducto.GetProducto();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PartialCatalogoProducto", lista);
        }

        //Boton nuevo producto
        public ActionResult Create()
        {
            //Para cargar la lista de categorías
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            ViewBag.listaCategoria = _ServiceCategoria.GetCategoria();
            return View();
        }

        //Accion crear, editar
        // POST: Libro/Create-Update
        [HttpPost]
        public ActionResult Save(Producto Producto, HttpPostedFileBase ImageFile, string[] selectedCategoria)
        {
            return View("Create", Producto);



        }

    }



}
