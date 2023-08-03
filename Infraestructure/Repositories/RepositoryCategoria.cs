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
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public IEnumerable<Categoria> GetCategoria()
        {
            try
            {

                IEnumerable<Categoria> lista = null;
                using (ByteStoreBDEntities ctx = new ByteStoreBDEntities())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Categoria.ToList<Categoria>();
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

        public Categoria GetCategoriaByID(int id)
        {
            Categoria oCategoria = null;
            try
            {

                using (ByteStoreBDEntities ctx = new ByteStoreBDEntities())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCategoria = ctx.Categoria.Find(id);
                }

                return oCategoria;
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
    }
}
