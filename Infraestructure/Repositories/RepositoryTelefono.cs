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
    public class RepositoryTelefono : IRepositoryTelefono
    {
        public int DeleteTelefonoByID(int id)
        {
            Telefono oTelefono = this.GetTelefonoByID(id);
            int retorno = 0;

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    ctx.Telefono.Attach(oTelefono);
                    ctx.Telefono.Remove(oTelefono);
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

            return retorno;

        }

        public Telefono GetTelefonoByID(int id)
        {
            Telefono telefono = null;

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    telefono = ctx.Telefono.Where(d => d.IdTelefono == id).FirstOrDefault();
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

            return telefono;
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

        public Telefono SaveTelefono(Telefono pTelefono)
        {
            Telefono oTelefono = null;
            int retorno = 0;

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    ctx.Usuario.Attach(pTelefono.Usuario);
                    ctx.Telefono.Add(pTelefono);
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
                oTelefono = this.GetTelefonoByID((int)pTelefono.IdTelefono);
            }

            return oTelefono;
        }
    }
}
