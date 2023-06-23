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

        // GET: Producto
        //Para el mantenimiento de productos
        public ActionResult IndexAdmin()
        {
            //Creo una lista tipo Producto
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProductoPorVendedor(2); //Obtengo todos los datos de la BD y los agrego a la lista 
                ViewBag.NumItemsPerPage = numItemsPerPage;
                ViewBag.Nombres = _ServiceProducto.GetProductoNombres(2);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista); //Retorno la vista con la lista ya cargada
        }

        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto(); //Obtengo todos los datos de la BD y los agrego a la lista 

                //Para cargar la lista de categorías
                //Nota:Hacer repositorios de categorias
                
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
        public PartialViewResult buscarProductoxNombre(string filtro)
        {
            IEnumerable<Producto> lista = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();

            //Validar si hay texto vacio
            if (string.IsNullOrEmpty(filtro.Trim()))
            {
                return PaginacionYOrden(1, 0);

            }
            else
            {
                lista = _ServiceProducto.GetProductoPorNombre(filtro, 2);
                return PartialView("_PaginacionView", lista);

            }

        }

        //Para la paginación
        public PartialViewResult PaginacionYOrden(int pag, int orden = 0)
        {

            //Creo una lista tipo Producto
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProductoPorVendedor(2);
                //
                int startIndex = (pag * numItemsPerPage) - numItemsPerPage;
                int count = (lista.Count() - startIndex) < numItemsPerPage ? (lista.Count() - startIndex) : numItemsPerPage;
                lista = ((List<Producto>)lista).GetRange(startIndex, count);

                if(orden == 0)
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

            return PartialView("_PaginacionView", lista);
        }


        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
       

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
