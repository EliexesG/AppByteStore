using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        private IRepositoryProducto repository = new RepositoryProducto();

        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProducto()
        {
            return repository.GetProducto();
        }

        public IEnumerable<Producto> GetProductoByCategoria(int idCategoria)
        {
            return repository.GetProductoByCategoria(idCategoria);
        }

        public Producto GetProductoByID(int id)
        {
            return repository.GetProductoByID(id);
        }

        public IEnumerable<Producto> GetProductoPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProductoPorVendedor(int idProducto)
        {
           return repository.GetProductoPorVendedor(idProducto);
        }

        public Producto GuardarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Producto Save(Producto producto)
        {
            throw new NotImplementedException();
        }
    }
}
