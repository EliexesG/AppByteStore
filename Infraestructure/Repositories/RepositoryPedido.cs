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
    public class RepositoryPedido : IRepositoryPedido
    {
        public void DeletePedido(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> GetPedido()
        {
            IEnumerable<Pedido> lista = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todos los Pedidos
                    lista = ctx.Pedido
                        .Include(x => x.CompraEncabezado)
                        .Include(x => x.CompraEncabezado.Usuario)
                        .Include(x => x.CompraEncabezado.CompraDetalle)
                        .Include(x => x.CompraEncabezado.Direccion)
                        .Include(x => x.CompraEncabezado.MetodoPago)
                        .Include(x => x.CompraEncabezado.MetodoPago.TipoPago)
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto))
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto.Usuario))
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

        public IEnumerable<Pedido> GetPedidoByCliente(int idCliente)
        {
            IEnumerable<Pedido> lista = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todos los pedidos
                    lista = ctx.Pedido
                        .Include(x => x.CompraEncabezado)
                        .Include(x => x.CompraEncabezado.Usuario)
                        .Include(x => x.CompraEncabezado.CompraDetalle)
                        .Include(x => x.CompraEncabezado.Direccion)
                        .Include(x => x.CompraEncabezado.MetodoPago)
                        .Include(x => x.CompraEncabezado.MetodoPago.TipoPago)
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto))
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto.Usuario))
                        .Where(pedido => pedido.CompraEncabezado.Usuario.IdUsuario == idCliente)
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

        public Pedido GetPedidoByID(int id)
        {
            Pedido pedido = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todos los pedidos
                    pedido = ctx.Pedido
                        .Include(x => x.CompraEncabezado)
                        .Include(x => x.CompraEncabezado.Usuario)
                        .Include(x => x.CompraEncabezado.CompraDetalle)
                        .Include(x => x.CompraEncabezado.Direccion)
                        .Include(x => x.CompraEncabezado.MetodoPago)
                        .Include(x => x.CompraEncabezado.MetodoPago.TipoPago)
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto))
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto.Usuario))
                        .Where(x => x.IdCompraEncabezado == id)
                        .FirstOrDefault(); ;

                }
                return pedido;
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

        public IEnumerable<Pedido> GetPedidoByVendedor(int idVendedor)
        {
            IEnumerable<Pedido> lista = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todos los Pedidos
                    lista = ctx.Pedido
                        .Include(x => x.CompraEncabezado)
                        .Include(x => x.CompraEncabezado.Usuario)
                        .Include(x => x.CompraEncabezado.CompraDetalle)
                        .Include(x => x.CompraEncabezado.Direccion)
                        .Include(x => x.CompraEncabezado.MetodoPago)
                        .Include(x => x.CompraEncabezado.MetodoPago.TipoPago)
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto))
                        .Include(x => x.CompraEncabezado.CompraDetalle.Select(c => c.Producto.Usuario))
                        .Where(pedido => pedido.CompraEncabezado.CompraDetalle.Any(c => c.Producto.Usuario.IdUsuario == idVendedor))
                        .OrderByDescending(x => x.CompraEncabezado.FechaHora)
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

        public CompraDetalle ActualizarEstadoEntregado(CompraDetalle compraDetalle)
        {

            
            int retorno = 0;
            CompraDetalle oCompraDetalle = null;

            try
            {

                using(ByteStoreContext ctx = new ByteStoreContext())
                {
                    Pedido oPedido = null;
                    ctx.CompraDetalle.Attach(compraDetalle);
                    ctx.Entry(compraDetalle).Property("EstadoEntrega").IsModified = true;
                    retorno = ctx.SaveChanges();

                    if(retorno >= 0)
                    {
                        oPedido = this.GetPedidoByID(compraDetalle.IdCompraEncabezado);
                    }

                    if(oPedido != null)
                    {

                        int totalDetalles = oPedido.CompraEncabezado.CompraDetalle.Count();
                        int totalDetallesEntregados = oPedido.CompraEncabezado.CompraDetalle.Where(cd => cd.EstadoEntrega == true).Count();

                        if(totalDetalles == totalDetallesEntregados)
                        {
                            oPedido.EstadoEntrega = 2;
                        }
                        else if (totalDetallesEntregados > 0)
                        {
                            oPedido.EstadoEntrega = 1;
                        }

                        oPedido.CompraEncabezado.CompraDetalle = null;

                        ctx.Pedido.Attach(oPedido);
                        ctx.Entry(oPedido).Property("EstadoEntrega").IsModified=true;
                        retorno += ctx.SaveChanges();

                        if (retorno > 0) {
                            oCompraDetalle = ctx.CompraDetalle.Where(cd => cd.IdCompraEncabezado == compraDetalle.IdCompraEncabezado && cd.IdProducto == compraDetalle.IdProducto).FirstOrDefault();
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


            return oCompraDetalle;
        }

        public Pedido Save(Pedido pedido)
        {
            int resultado = 0;
            Pedido _Pedido = null;
            IRepositoryProducto repositoryProducto = new RepositoryProducto();

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    using(var transaccion = ctx.Database.BeginTransaction())
                    {
                        foreach(var detalleCompra in pedido.CompraEncabezado.CompraDetalle)
                        {
                            detalleCompra.Producto.Usuario = null;
                            detalleCompra.Producto.Categoria = null;
                            detalleCompra.Producto.FotoProducto = null;
                            ctx.Producto.Attach(detalleCompra.Producto);
                        }

                        pedido.CompraEncabezado.Usuario.Direccion = null;
                        pedido.CompraEncabezado.Usuario.MetodoPago = null;
                        ctx.Usuario.Attach(pedido.CompraEncabezado.Usuario);

                        ctx.MetodoPago.Attach(pedido.CompraEncabezado.MetodoPago);
                        ctx.Direccion.Attach(pedido.CompraEncabezado.Direccion);

                        ctx.Pedido.Add(pedido);
                        resultado = ctx.SaveChanges();
                        transaccion.Commit();
                    }
                }

                if(resultado >= 0)
                {
                    _Pedido = GetPedidoByID(pedido.IdCompraEncabezado);

                    foreach (var detalle in pedido.CompraEncabezado.CompraDetalle)
                    {
                        repositoryProducto.ActualizarStock(detalle.Producto.IdProducto, (int)detalle.Cantidad);
                    }

                }

                return _Pedido;
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
