using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


            var listaRoles = new object[] {
                                   new { IdRol = 2, Descripcion = "Vendedor" },
                                   new { IdRol = 3, Descripcion = "Cliente" } };

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


        // POST: Usuario/Create
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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
