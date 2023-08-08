using ApplicationCore.Services;
using Infraestructure.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //public ActionResult Index(Usuario usuario)
        public ActionResult Login(Login login)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {

                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.Login(login.CorreoElectronico, login.Contrasenna);

                    if (oUsuario != null)
                    {
                        Session["User"] = oUsuario;

                        Log.Info($"Accede{oUsuario.Nombre} {oUsuario.PrimerApellido} {oUsuario.SegundoApellido} " +
                            $"con los roles {oUsuario.Rol.FirstOrDefault().IdRol}");

                        TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Login",
                            "Usuario autenticado correctamente", Util.SweetAlertMessageType.success);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio de secion{login.CorreoElectronico}");
                        TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Login",
                            "Usuario no válido", Util.SweetAlertMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View("Index");
        }

        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                Usuario usuario = Session["User"] as Usuario;
                Log.Warn($"No autorizado {usuario.CorreoElectronico}");
            }
            return View();
        }
        public ActionResult Logout()
        {
            try
            {
                Session.Remove("User");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
