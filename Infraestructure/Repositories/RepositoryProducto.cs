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

        public IEnumerable<Producto> GetFotosPorProducto(int idProducto)
        {
            IEnumerable<Producto> oFotoProducto = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener Productos por Vendedor (Usuario) y su información
                    oFotoProducto = ctx.Producto
                        .Include("FotoProducto")
                        .Where(l => l.IdProducto == idProducto)
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
        public Producto GuardarProducto(Producto producto)
        {
            throw new NotImplementedException();
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
                    //Para Modificar
                    ctx.Usuario.Attach(producto.Usuario);

                    ctx.Categoria.Attach(producto.Categoria);


                    ctx.Producto.Add(producto);
                    ctx.Entry(producto).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)producto.IdProducto);
            oProducto = GetProductoByCategoria((int)producto.IdProducto);

            return oProducto;
        }
    }
}
