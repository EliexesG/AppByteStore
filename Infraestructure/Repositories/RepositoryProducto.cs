using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;


namespace Infraestructure.Repositories
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProducto()
        {
            IEnumerable<Producto> lista = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Producto.Include("Usuario").
                        Include("FotoProducto")
                        .Include("Categoria")
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

        public IEnumerable<Producto> GetProductoByCategoria(int idCategoria)
        {
            IEnumerable<Producto> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener los libros que pertenecen a una categoría
                    lista = ctx.Producto
                        .Include("Usuario")
                        .Include("FotoProducto")
                        .Include("Categoria")
                        .Where(o => o.Categoria.IdCategoria == idCategoria).ToList();

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

        public Producto GetProductoByID(int id)
        {
            {
                Producto oProducto = null;
                try
                {
                    using (ByteStoreContext ctx = new ByteStoreContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        //Obtener Producto por ID incluyendo el autor y todas sus categorías
                        oProducto = ctx.Producto.Find(id);
                        oProducto = ctx.Producto
                            .Include("Usuario")
                            .Include("Categoria")
                            .Include("FotoProducto")
                            .Where(l => l.IdProducto == id)
                            .FirstOrDefault();

                    }
                    return oProducto;
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

        public IEnumerable<Producto> GetProductoPorNombre(string nombre)
        {
            try
            {
                IEnumerable<Producto> oProducto = null;
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener Producto por nombre
                    oProducto = ctx.Producto
                        .Include("Usuario")
                        .Include("FotoProducto")
                        .Include("Categoria")
                        .ToList()
                        .FindAll(x => x.Nombre.ToLower()
                        .Contains(nombre.ToLower()));

                }
                return oProducto;
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


        public IEnumerable<Producto> GetProductoPorVendedor(int idVendedor)
        {
            IEnumerable<Producto> oProducto = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener Productos por Vendedor (Usuario) y su información
                    oProducto = ctx.Producto
                        .Include("Usuario")
                        .Include("FotoProducto")
                        .Include("Categoria")
                        .Where(l => l.Usuario.IdUsuario == idVendedor)
                        .ToList();

                }
                return oProducto;
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

        public IEnumerable<FotoProducto> GetFotosPorProducto(int idProducto)
        {
            IEnumerable<FotoProducto> oFotoProducto = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener Productos por Vendedor (Usuario) y su información
                    oFotoProducto = ctx.FotoProducto
                                    .Include("Producto")
                                    .Where(foto => foto.Producto.IdProducto == idProducto)
                                    .ToList();

                }
                return oFotoProducto;
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

        public Producto Save(Producto producto)
        {
            Producto oProducto = null;
            int retorno = 0;

            using (ByteStoreContext ctx = new ByteStoreContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                oProducto = GetProductoByID(producto.IdProducto);

                //Para Insertar 
                if (oProducto == null)
                {
                    ctx.Usuario.Attach(producto.Usuario);
                    ctx.Categoria.Attach(producto.Categoria);
                    ctx.Producto.Add(producto);
                    retorno = ctx.SaveChanges();
                }

                else
                {
                    try
                    {
                        // Crear una lista de entidades "FotoProducto" a eliminar
                        List<FotoProducto> imagenesEliminar = this.GetFotosPorProducto(producto.IdProducto)
                            .Select(foto => new FotoProducto() { IdFotoProducto = foto.IdFotoProducto })
                            .ToList();

                        // Eliminar las entidades "FotoProducto" de la base de datos
                        foreach (var imagen in imagenesEliminar)
                        {
                            ctx.Entry(imagen).State = EntityState.Deleted;
                        }

                        // Agregar las nuevas entidades "FotoProducto" a la entidad "Producto"
                        foreach (var imagen in producto.FotoProducto)
                        {
                            ctx.Entry(imagen).State = EntityState.Added;
                        }

                        // Para modificar
                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        // Recargar la entidad "Producto" desde la base de datos
                        foreach (var entry in ex.Entries)
                        {
                            entry.Reload();
                        }

                        // Volver a intentar guardar los cambios en la base de datos
                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)producto.IdProducto);

            return oProducto;
        }

        public Producto ActualizarStock (int idProducto, int cantRebajada)
        {
            Producto oProducto = null;
            int retorno = 0;

            try
            {

                oProducto = this.GetProductoByID((int)idProducto);

                if(oProducto != null)
                {
                    oProducto.Stock -= cantRebajada;

                    using (ByteStoreContext ctx = new ByteStoreContext())
                    {

                        ctx.Producto.Attach(oProducto);
                        ctx.Entry(oProducto).Property("Stock").IsModified = true;   

                        retorno = ctx.SaveChanges();

                        if (retorno <= 0)
                        {
                            oProducto = null;
                        }
                    }
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

            return oProducto;
        }
    }
}
