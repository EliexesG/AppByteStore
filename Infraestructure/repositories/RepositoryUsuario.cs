using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                     Include("Rol"). //obtiene el usuario incluyendo el rol
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

        public IEnumerable<Usuario> GetUsuarios(string Correo, string contrasenna)
        {
            IEnumerable<Usuario> usuarios = null;

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuarios = ctx.Usuario.Where
                        (u => u.CorreoElectronico.Equals(Correo) && u.Contrasenna.Equals(contrasenna));


                }

                return usuarios;
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


        public Usuario Guardar(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
