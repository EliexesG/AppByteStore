using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infraestructure.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public Usuario GetUsuarioByID(int id)
        {
            Usuario usuario = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    usuario = ctx.Usuario.
                              Include(u => u.Rol).
                              Where(p => p.IdUsuario == id).
                              FirstOrDefault<Usuario>();
                }
                return usuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Rol GetRolByID(int id)
        {
            Rol oRol = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRol = ctx.Rol.Find(id);
                }
                return oRol;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Usuario.
                            Include(u => u.Rol).
                            ToList();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Usuario> GetUsuarioByRol(int IdRol)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Usuario
                            .Include(u => u.Rol)
                            .Where(usuario => usuario.Rol.Any(rol => rol.IdRol == IdRol))
                            .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario Login(string Correo, string contrasenna)
        {
            Usuario usuario = null;

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    usuario = ctx.Usuario
                               .Include("Rol")
                               .Include(u => u.MetodoPago)
                               .Include(u => u.MetodoPago.Select(m => m.TipoPago))
                               .Include(u => u.Direccion)
                               .Where(u => u.CorreoElectronico.Equals(Correo))
                               .ToList()
                               .Where(u => Encrypter.Desencrypt(u.Contrasenna).Equals(contrasenna) && u.Estado == true)
                               .FirstOrDefault();
                }

                return usuario;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<TipoPago> GetTipoPago()
        {
            IEnumerable<TipoPago> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.TipoPago.ToList();

                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Rol> GetRol()
        {
            IEnumerable<Rol> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Rol.Where(r => r.IdRol != 1).ToList();

                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Usuario> GetUsuarioByEstado(bool estado)
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Usuario
                            .Include("Rol")
                            .Where(usuario => usuario.Estado == estado)
                            .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario)
        {
            IEnumerable<MetodoPago> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.MetodoPago
                            .Include(m => m.Usuario)
                            .Where(m => m.Usuario.IdUsuario == idUsuario)
                            .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario)
        {
            IEnumerable<Direccion> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Direccion
                            .Include(d => d.Usuario)
                            .Where(d => d.Usuario.IdUsuario == idUsuario)
                            .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario)
        {
            IEnumerable<Telefono> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Telefono
                            .Include(d => d.Usuario)
                            .Where(d => d.Usuario.IdUsuario == idUsuario)
                            .ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario Guardar(Usuario pUsuario, string[] selectedRol)
        {
            IRepositoryUsuario _RepositoryUsuario = new RepositoryUsuario();

            Usuario oUsuario = null;
            int retorno = 0;

            using (ByteStoreContext ctx = new ByteStoreContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                //Para Insertar 
                if (oUsuario == null)
                {


                    //Insertar
                    //Logica para agregar los roles al usuario
                    if (selectedRol != null)
                    {

                        pUsuario.Rol = new List<Rol>();
                        foreach (var rol in selectedRol)
                        {
                            var AddRol = _RepositoryUsuario.GetRolByID(int.Parse(rol));
                            ctx.Rol.Attach(AddRol);
                            pUsuario.Rol.Add(AddRol);


                        }
                    }
                    ctx.Usuario.Attach(pUsuario);

                    ctx.Usuario.Add(pUsuario);
                    retorno = ctx.SaveChanges();
                }
            
            }
            if (retorno >= 0)
                oUsuario = GetUsuarioByID((int)pUsuario.IdUsuario);

            return oUsuario;

        }
    }
}
