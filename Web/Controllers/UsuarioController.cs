using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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

            var listaRoles = serviceUsuario.GetRol();

            ViewBag.listaRoles = new SelectList(listaRoles, "IdRol", "Descripcion");

            var listatipoTelefono = new object[] {
                                   new { IdTel = 1, Descripcion = "Casa" },
                                   new { IdTel = 2, Descripcion = "Celular" },
                                   new { IdTel = 3, Descripcion = "Otro" } };

            ViewBag.listatipoTelefono = new SelectList(listatipoTelefono, "IdTel", "Descripcion");


            //Para cargar la lista de MetodoPago
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            ViewBag.listaMetodoPago = _ServiceUsuario.GetTipoPago();


            return View();
        }

        //vista parcial de telefonos, direccion y metodos de pago
        public PartialViewResult PartialTelefonos()
        {

            return PartialView("_PartialTelefonos");
        }

        public PartialViewResult PartialMetodosPago()
        {

            //Para cargar la lista de MetodoPago
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            ViewBag.listaMetodoPago = _ServiceUsuario.GetTipoPago();

            return PartialView("_PartialMetodosPago");
        }

        public PartialViewResult PartialDireccion()
        {

            return PartialView("_PartialDireccion");
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
    }
}
