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

        public Pedido Save(Pedido pedido)
        {
            throw new NotImplementedException();
        }
    }
}
