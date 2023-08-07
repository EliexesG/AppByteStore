using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Security;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {

            IServiceUsuario serviceUsuario = new ServiceUsuario();

            ViewBag.listaRoles = ListaRoles();

            return View();
        }

        [CustomAuthorize((int)Roles.Vendedor, (int)Roles.Cliente)]
        public ActionResult Update()
        {

            IServiceUsuario serviceUsuario = new ServiceUsuario();
            IServiceMetodoPago serviceMetodoPago = new ServiceMetodoPago();

            Usuario oUsuario = serviceUsuario.GetUsuarioByID(((Usuario)Session["User"]).IdUsuario);

            var listaRoles = serviceUsuario.GetRol();

            ViewBag.listaRoles = ListaRoles();

            var listatipoTelefono = new TipoTelefono[] {
                                   new TipoTelefono() { IdTipo = 1, Descripcion = "Casa" },
                                   new TipoTelefono() { IdTipo = 2, Descripcion = "Celular" }};

            ViewBag.listatipoTelefono = new SelectList(listatipoTelefono, "IdTel", "Descripcion");

            ViewBag.password = Infraestructure.Utils.Encrypter.Desencrypt(oUsuario.Contrasenna);

            ViewBag.listaTipoPago = serviceMetodoPago.GetTipoPago();


            return View(oUsuario);
        }

        public PartialViewResult PartialTelefonos(int idUsuario)
        {
            IEnumerable<Telefono> lista = null;

            try
            {
                IServiceTelefono _ServiceTelefono = new ServiceTelefono();
                lista = _ServiceTelefono.GetTelefonoByUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PartialTelefonos", lista);
        }

        [HttpPost]
        public JsonResult SaveTelefono(string numero, int tipo)
        {
            IServiceTelefono _ServiceTelefono = new ServiceTelefono();
            bool resultado = false;

            try
            {
                int idUsuario = ((Usuario)Session["User"]).IdUsuario;

                Telefono oTelefono = new Telefono()
                {
                    Numero = numero,
                    Tipo = tipo == 2 ? false : true,
                    Usuario = new Usuario() { IdUsuario = idUsuario }
                };

                Telefono telefono = _ServiceTelefono.SaveTelefono(oTelefono);

                if (telefono != null)
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

        public PartialViewResult PartialMetodoPago(int idUsuario)
        {
            IEnumerable<MetodoPago> lista = null;
            IServiceMetodoPago serviceMetodoPago = new ServiceMetodoPago();

            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = serviceMetodoPago.GetMetodoPagoByUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PartialMetodoPago", lista);
        }

        [HttpPost]
        public JsonResult SaveMetodoPago (string proveedor, string numeroTarjeta, string fechaExpiracion, string codigo, int idTipoPago)
        {
            IServiceMetodoPago _ServiceMetodoPago = new ServiceMetodoPago();
            bool resultado = false;

            try
            {
                int idUsuario = ((Usuario)Session["User"]).IdUsuario;
                string[] separacionFecha = fechaExpiracion.Split('/');

                MetodoPago oMetodoPago = new MetodoPago()
                {
                    Proveedor = proveedor,
                    NumeroTarjeta = Encoding.UTF8.GetBytes(numeroTarjeta),
                    FechaExpiracion = new DateTime(Convert.ToInt32($"20{separacionFecha[1]}"), Convert.ToInt32(separacionFecha[0]), 1),
                    Codigo = codigo,
                    TipoPago = new TipoPago() { IdTipoPago = idTipoPago },
                    Usuario = new Usuario() {  IdUsuario = idUsuario }
                };

                MetodoPago metodoPago = _ServiceMetodoPago.SaveMetodoPago(oMetodoPago);

                if (metodoPago != null)
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

        public async Task<PartialViewResult> PartialDireccion(int idUsuario)
        {
            IEnumerable<Direccion> lista = null;
            IServiceUbicacion serviceUbicacion = new ServiceUbicacion();

            try
            {
                List<string[]> listaNombres = new List<string[]>();
                IServiceDireccion _ServiceDireccion = new ServiceDireccion();
                lista = _ServiceDireccion.GetDireccionByUsuario(idUsuario);

                foreach (var direccion in lista)
                {
                    string[] nombres = new string[3];
                    nombres[0] = await serviceUbicacion.ObtenerNombreProvincia((int)direccion.Provincia);
                    nombres[1] = await serviceUbicacion.ObtenerNombreCanton((int)direccion.Provincia, (int)direccion.Canton);
                    nombres[2] = await serviceUbicacion.ObtenerNombreDistrito((int)direccion.Provincia, (int)direccion.Canton, (int)direccion.Distrito);
                    listaNombres.Add(nombres);
                }

                ViewBag.ListaNombres = listaNombres;
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return PartialView("_PartialDireccion", lista);
        }

        [HttpPost]
        public JsonResult SaveDireccion(int provincia, int canton, int distrito, string telefono, string codigoPostal, string sennas)
        {
            IServiceDireccion _ServiceDireccion = new ServiceDireccion();
            bool resultado = false;

            try
            {
                int idUsuario = ((Usuario)Session["User"]).IdUsuario;

                Direccion oDireccion = new Direccion()
                {
                    Provincia = provincia,
                    Canton = canton,
                    Distrito = distrito,
                    Telefono = telefono,
                    CodigoPostal = codigoPostal,
                    Sennas = sennas,
                    Usuario = new Usuario() { IdUsuario = idUsuario }
                };

                Direccion direccion = _ServiceDireccion.SaveDireccion(oDireccion);

                if (direccion != null)
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

        public async Task<JsonResult> ObtenerProvincias()
        {

            IServiceUbicacion serviceUbicacion = new ServiceUbicacion();
            ApiResult resultado = null;

            try
            {
                resultado = await serviceUbicacion.ObtenerProvincia();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> ObtenerCantonesxProvincia(int idProvincia)
        {

            IServiceUbicacion serviceUbicacion = new ServiceUbicacion();
            ApiResult resultado = null;

            try
            {
                resultado = await serviceUbicacion.ObtenerCantonxProvincia(idProvincia);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> ObtenerDistritosxCanton(int idProvincia, int idCanton)
        {

            IServiceUbicacion serviceUbicacion = new ServiceUbicacion();
            ApiResult resultado = null;

            try
            {
                resultado = await serviceUbicacion.ObtenerDistritoxCanton(idProvincia, idCanton);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult SaveUsuario(Usuario pUsuario, string[] selectedRol, string password)
        {


            ModelState.Remove("NombreProveedor");
            ModelState.Remove("Contrasenna");
            ModelState.Remove("Rol");
            ModelState.Remove("PromedioEvaluaciones");

            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Usuario oUsuario = null;
            pUsuario.Estado = true;
            pUsuario.Contrasenna = Encoding.UTF8.GetBytes(password);

            try
            {

                Usuario usuarioVerificacionRol = _ServiceUsuario.GetUsuarioByID(pUsuario.IdUsuario);
                bool esVendedor = usuarioVerificacionRol.Rol.Where(u => u.IdRol == 2).FirstOrDefault() != null;

                if (usuarioVerificacionRol != null)
                {
                    if(esVendedor && !selectedRol.Contains("2") && _ServiceProducto.GetProductoPorVendedor(usuarioVerificacionRol.IdUsuario).Count() > 0)
                    {
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                            "El usuario posee Rol Vendedor, no se puede eliminar", Util.SweetAlertMessageType.warning);
                        return RedirectToAction($"Update/{pUsuario.IdUsuario}", "Usuario");
                    }
                }
                else
                {
                    pUsuario.PromedioEvaluaciones = 5;
                }

                if (ModelState.IsValid)
                {

                    oUsuario = _ServiceUsuario.Guardar(pUsuario, selectedRol);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //Recurso a cargar en la vista
                    ViewBag.listaRoles = ListaRoles();

                    //Debe funcionar para crear y modificar
                    return View("Create", pUsuario);
                }
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

        public MultiSelectList ListaRoles(ICollection<Rol> Roles = null)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Rol> lista = _ServiceUsuario.GetRol();
            //Seleccionar los Roles para modificar
            int[] SelectRol = null;
            if (Roles != null)
            {
                SelectRol = Roles.Select(r => r.IdRol).ToArray();
            }

            return new MultiSelectList(lista, "IdRol", "Descripcion", SelectRol);
        }
    }



}