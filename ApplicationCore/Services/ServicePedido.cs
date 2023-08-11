using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePedido : IServicePedido
    {

        private IRepositoryPedido _repositoryPedido;

        public ServicePedido()
        {
            _repositoryPedido = new RepositoryPedido();
        }

        public IEnumerable<Pedido> GetPedido()
        {
            return _repositoryPedido.GetPedido();
        }

        public void DeletePedido(int id)
        {
            _repositoryPedido.DeletePedido(id);
        }

        public IEnumerable<Pedido> GetPedidoByCliente(int idCliente)
        {
            return _repositoryPedido.GetPedidoByCliente(idCliente).OrderByDescending(p => p.CompraEncabezado.FechaHora).ToList();
        }

        public Pedido GetPedidoByID(int id)
        {
            return _repositoryPedido.GetPedidoByID(id);
        }

        public IEnumerable<Pedido> GetPedidoByVendedor(int idVendedor)
        {
            return _repositoryPedido.GetPedidoByVendedor(idVendedor);
        }

        public CompraDetalle ActualizarEstadoEntregado(CompraDetalle compraDetalle)
        {
            return _repositoryPedido.ActualizarEstadoEntregado(compraDetalle);
        }

        public Pedido Save(Pedido pedido)
        {
            return _repositoryPedido.Save(pedido);
        }

       public IEnumerable<CompraEncabezado> GetComprasRegistradasEnElDia()
        {
           return _repositoryPedido.GetComprasRegistradasEnElDia();
        }

        public IEnumerable<object> GetTopProductosVendidosByMes()
        {
            return _repositoryPedido.GetTopProductosVendidosByMes();
        }

        public object GetProductoMasVendidoVendedor(int idVendedor)
        {
            return _repositoryPedido.GetProductoMasVendidoVendedor(idVendedor);
        }

        public object GetClienteMasFiel(int idVendedor)
        {
            return _repositoryPedido.GetClienteMasFiel(idVendedor);
        }
    }
}
