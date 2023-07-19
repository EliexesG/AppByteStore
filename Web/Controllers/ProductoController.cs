using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {

        private int numItemsPerPage = 6;

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

        [CustomAuthorize((int)Roles.Administrador)]
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
        [CustomAuthorize((int)Roles.Vendedor)]
        public ActionResult IndexVendedor()
        {
            //Creo una lista tipo Producto
            IEnumerable<Producto> lista = null;
            try
            {
                //Instancia 
                Usuario user = Session["User"] as Usuario;
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProductoPorVendedor(user.IdUsuario); //Obtengo todos los datos de la BD y los agrego a la lista 
                TempData["GetBack"] = "IndexVendedor";
                ViewBag.NumItemsPerPage = numItemsPerPage;
                ViewBag.Nombres = _ServiceProducto.GetProductoNombres(user.IdUsuario);
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
        public ActionResult Details(int? id)
        {
            Producto producto = null;

            try
            {
                if(id == null)
                {
                    return RedirectToAction("Index");
                }

                IServiceProducto _ServiceProducto = new ServiceProducto();
                producto = _ServiceProducto.GetProductoByID(Convert.ToInt32(id));

                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.GetBack = TempData["GetBack"];

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
            Usuario user = Session["User"] as Usuario;
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
                    lista = _ServiceProducto.GetProductoPorNombre(filtro, user.IdUsuario);
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
                Usuario user = Session["User"] as Usuario;
                IServiceProducto _ServiceProducto = new ServiceProducto();

                if (tipoUsuario == 1)
                {

                    lista = _ServiceProducto.GetProducto();
                }
                else
                {
                    lista = _ServiceProducto.GetProductoPorVendedor(user.IdUsuario);
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

        public PartialViewResult PreguntasProducto (int IdProducto, int IdUsuarioProducto) {
            
            IEnumerable<Pregunta> lista = null;
            
            try
            {
                IServicePregunta _ServicePregunta = new ServicePregunta();
                lista = _ServicePregunta.GetPreguntaByProducto(IdProducto);
                ViewBag.IdUsuarioProducto = IdUsuarioProducto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PreguntasProducto", lista);
        }

        [HttpPost]
        public JsonResult SavePregunta (string stringPregunta, int IdProducto)
        {
            IServicePregunta _ServicePregunta = new ServicePregunta();
            bool resultado = false;

            try
            {

                Pregunta pregunta = new Pregunta()
                {
                    FechaHora = DateTime.Now,
                    Pregunta1 = stringPregunta
                };

                pregunta = _ServicePregunta.SavePregunta(pregunta, (Session["User"] as Usuario).IdUsuario, IdProducto);

                if (pregunta != null)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return Json(new
            {
                success = resultado
            });
        }

        public PartialViewResult RespuestasPregunta(int IdPregunta)
        {

            IEnumerable<Respuesta> lista = null;

            try
            {
                IServicePregunta _ServicePregunta = new ServicePregunta();
                lista = _ServicePregunta.GetRespuestaByPregunta(IdPregunta);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_RespuestasPregunta", lista);
        }

        public JsonResult SaveRespuesta(string stringRespuesta, int IdPregunta)
        {
            IServicePregunta _ServicePregunta = new ServicePregunta();
            bool resultado = false;

            try
            {

                Respuesta respuesta = new Respuesta()
                {
                    FechaHora = DateTime.Now,
                    Respuesta1 = stringRespuesta
                };

                respuesta = _ServicePregunta.SaveRespuesta(respuesta, (Session["User"] as Usuario).IdUsuario, IdPregunta);

                if (respuesta != null)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return Json(new
            {
                success = resultado
            });
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
