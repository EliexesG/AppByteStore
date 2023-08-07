using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryDireccion : IRepositoryDireccion
    {
        public Direccion GetDireccionByID(int id)
        {
            Direccion direccion = null;

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    direccion = ctx.Direccion.Where(d => d.IdDireccion == id).FirstOrDefault();
                }
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

            return direccion;
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

        public Direccion SaveDireccion(Direccion pDireccion)
        {
            Direccion oDireccion = null;
            int retorno = 0;

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    ctx.Usuario.Attach(pDireccion.Usuario);
                    ctx.Direccion.Add(pDireccion);
                    retorno = ctx.SaveChanges();
                }
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

            if (retorno >= 0)
            {
                oDireccion = this.GetDireccionByID((int)pDireccion.IdDireccion);
            }

            return oDireccion;
        }
    }
}
