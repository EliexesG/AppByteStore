﻿using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
                lista = _ServiceProducto.GetProducto().OrderByDescending(p => p.Precio).ToList();

                //Obtengo todos los datos de la BD y los agrego a la lista 
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

                if (((Usuario)Session["User"]).Direccion.Count() <= 0)
                {
                    return RedirectToAction("UnAuthorized", "Login");
                }

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
                if (id == null)
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
            else if (string.IsNullOrEmpty(filtro.Trim()) && (tipoUsuario == 3))
            {
                lista = _ServiceProducto.GetProducto().OrderByDescending(p => p.Precio).ToList(); ;
                partialView = "_PartialCatalogoProducto";
            }
            else
            {
                if (tipoUsuario == 1)
                {
                    lista = _ServiceProducto.GetProductoPorNombre(filtro);
                    partialView = "_PaginacionYOrdenViewProducto";
                }
                else if (tipoUsuario == 2)
                {
                    lista = _ServiceProducto.GetProductoPorNombre(filtro, user.IdUsuario);
                    partialView = "_PaginacionYOrdenViewProducto";
                }
                else
                {
                    lista = _ServiceProducto.GetProductoPorNombre(filtro).OrderByDescending(p => p.Precio).ToList();
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

        public PartialViewResult BuscarProductoxCategoria(int categoria)
        {

            IEnumerable<Producto> lista = null;

            try
            {

                IServiceProducto _ServiceProducto = new ServiceProducto();

                if (categoria != -1)
                {
                    lista = _ServiceProducto.GetProductoByCategoria(categoria).OrderByDescending(p => p.Precio).ToList();
                }
                else
                {
                    lista = _ServiceProducto.GetProducto().OrderByDescending(p => p.Precio).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PartialCatalogoProducto", lista);
        }

        public PartialViewResult OrdenarxPrecio(int tipoOrden)
        {
            IEnumerable<Producto> lista = null;

            try
            {

                IServiceProducto _ServiceProducto = new ServiceProducto();

                if (tipoOrden == 0)
                {
                    lista = _ServiceProducto.GetProducto().OrderByDescending(p => p.Precio).ToList();
                }
                else
                {
                    lista = _ServiceProducto.GetProducto().OrderBy(p => p.Precio).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PartialCatalogoProducto", lista);
        }

        public PartialViewResult PreguntasProducto(int IdProducto, int IdUsuarioProducto)
        {

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
        public JsonResult SavePregunta(string stringPregunta, int IdProducto)
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

        [HttpPost]
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


        [CustomAuthorize((int)Roles.Vendedor, (int)Roles.Administrador)]
        public ActionResult Create()
        {

            if (((Usuario)Session["User"]).Direccion.Count() <= 0)
            {
                return RedirectToAction("UnAuthorized", "Login");
            }

            //Para cargar la lista de categorías
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            ViewBag.listaCategoria = new SelectList(_ServiceCategoria.GetCategoria(), "IdCategoria", "Descripcion");

            var listaEstados = new object[] {
                                   new { Estado = 0, Descripcion = "Nuevo" },
                                   new { Estado = 1, Descripcion = "Usado (Como Nuevo)" },
                                   new { Estado = 2, Descripcion = "Usado (Buen Estado)" },
                                   new { Estado = 3, Descripcion = "Usado (Aceptable)" } };

            ViewBag.listaEstados = new SelectList(listaEstados, "Estado", "Descripcion");

            return View();
        }

        [CustomAuthorize((int)Roles.Vendedor, (int)Roles.Administrador)]
        public ActionResult Update(int? id)
        {

            if (((Usuario)Session["User"]).Direccion.Count() <= 0)
            {
                return RedirectToAction("UnAuthorized", "Login");
            }

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            //Para cargar la lista de categorías
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            ViewBag.listaCategoria = new SelectList(_ServiceCategoria.GetCategoria(), "IdCategoria", "Descripcion");

            var listaEstados = new object[] {
                                   new { Estado = 0, Descripcion = "Nuevo" },
                                   new { Estado = 1, Descripcion = "Usado (Como Nuevo)" },
                                   new { Estado = 2, Descripcion = "Usado (Buen Estado)" },
                                   new { Estado = 3, Descripcion = "Usado (Aceptable)" } };

            ViewBag.listaEstados = new SelectList(listaEstados, "Estado", "Descripcion");


            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = _ServiceProducto.GetProductoByID((int)id);

            return View(producto);
        }

        //Accion crear, editar
        // POST: Producto/Create-Update
        [HttpPost]
        public ActionResult Save(Producto producto, IEnumerable<HttpPostedFileBase> ImageFiles)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto oProducto = null;
            bool esEditar = producto.IdProducto != 0;

            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (producto.FotoProducto.Count() <= 0 && ImageFiles.ElementAt(0) != null)
                {
                    if (ImageFiles != null && ImageFiles.Count() >= 1)
                    {
                        foreach (var imagen in ImageFiles)
                        {
                            using (var memoryStream = new MemoryStream())
                            {

                                imagen.InputStream.CopyTo(memoryStream);
                                producto.FotoProducto.Add(new FotoProducto() { Foto = memoryStream.ToArray() });
                            }
                        }
                    }

                }
                else if (producto.IdProducto != 0)
                {
                    producto.FotoProducto = _ServiceProducto.GetFotosPorProducto(producto.IdProducto).Select(foto => new FotoProducto() { Foto = foto.Foto }).ToList();
                }

                if (ModelState.IsValid)
                {
                    producto.Usuario = new Usuario() { IdUsuario = (Session["User"] as Usuario).IdUsuario };


                    oProducto = _ServiceProducto.Save(producto);

                    if (oProducto != null)
                    {
                        if (esEditar)
                        {
                            TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Actualizado",
                            "Producto Actualizado Correctamente", Util.SweetAlertMessageType.success);
                        }
                        else
                        {
                            TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Creado",
                            "Producto Creado Correctamente", Util.SweetAlertMessageType.success);
                        }

                    }
                    else
                    {
                        TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Error",
                           "Verifique de Nuevo", Util.SweetAlertMessageType.error);
                    }
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //Recurso a cargar en la vista

                    //Debe funcionar para crear y modificar
                    ViewBag.IdCategoria = producto.Categoria.IdCategoria;
                    return View("Create", producto);
                }

                return RedirectToAction("Details/" + oProducto.IdProducto, "Producto");

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexVendedor";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}




